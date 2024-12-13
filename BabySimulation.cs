using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BabySimulation : MonoBehaviour
{
    public Text heartRateText; // Used to display heart rate
    public Text respiratoryRateText; // Used to display respiratory rate
    public Text timerText; // Used to display time

    public AudioSource breathAudioSource; // Sound source
    public GameObject successDialog; // Success dialog
    public GameObject reassessDialog; // Reassess dialog

    public float delayToShowDialog = 5f; // Delay to show the dialog

    private Animator babyAnimator;
    private float lastSqueezeTime;
    private int validBreathsInLast6Seconds;
    private int failedAttempts;
    private int heartRate = 80;
    private int respiratoryRate = 20;
    private float timer = 0f;
    private bool levelCompleted = false;  // 添加一个标志以确保只发送一次数据

    private const int maxHeartRate = 130;

    public GameObject failureIcon; // Failure icon
    public AudioSource failureAudioSource; // Failure sound source
    public float minSqueezeDurationForValidBreath = 0.4f;
    public float failureIconDisplayDuration = 1f;

    public int gamelevel = 2;
    public int finishedTime = 0;
    public int isRight = 0;
    public long idcode = 201;

    void Start()
    {
        babyAnimator = GetComponent<Animator>();
        InvokeRepeating("EvaluateBreaths", 6f, 6f);

        if (breathAudioSource == null || successDialog == null || reassessDialog == null || failureIcon == null || failureAudioSource == null)
        {
            Debug.LogError("One or more required components not assigned!");
        }
    }

    void Update()
    {
        UpdateTimer();
        UpdateUI();

        if (Input.GetMouseButtonDown(0))
        {
            StartSqueeze();
        }

        if (Input.GetMouseButtonUp(0))
        {
            EndSqueeze();
        }

        if (timer > delayToShowDialog)
        {
            if (heartRate > 60)
            {
                ShowSuccessDialog();
            }
            else
            {
                ShowReassessDialog();
            }
                        // 设置标志为true，表示关卡已完成
            levelCompleted = true;
        }
    }

    void ShowSuccessDialog()
    {
        if (successDialog != null)
        {
            successDialog.SetActive(true);
        }

        isRight = 1;
        SaveLevelDetails();
    }

    void ShowReassessDialog()
    {
        if (reassessDialog != null)
        {
            reassessDialog.SetActive(true);
        }

        isRight = 0;
        SaveLevelDetails();
    }

    void StartSqueeze()
    {
        lastSqueezeTime = Time.time;
        babyAnimator.SetTrigger("Squeeze");
        PlayBreathSound();
    }

    void EndSqueeze()
    {
        float squeezeDuration = Time.time - lastSqueezeTime;

        if (squeezeDuration >= minSqueezeDurationForValidBreath)
        {
            int increaseAmount = Random.Range(4, 9);
            heartRate += increaseAmount;

            heartRate = Mathf.Min(heartRate, maxHeartRate);

            validBreathsInLast6Seconds++;
        }
        else
        {
            PlayFailureSound();
            ShowFailureIcon();
            StartCoroutine(HideFailureIconAfterDelay());
        }

        babyAnimator.SetTrigger("Inflate");
        StopBreathSound();
    }

    void PlayBreathSound()
    {
        if (breathAudioSource != null)
        {
            breathAudioSource.Play();
        }
    }

    void StopBreathSound()
    {
        if (breathAudioSource != null)
        {
            breathAudioSource.Stop();
        }
    }

    void PlayFailureSound()
    {
        if (failureAudioSource != null)
        {
            failureAudioSource.Play();
        }
    }

    void ShowFailureIcon()
    {
        if (failureIcon != null)
        {
            failureIcon.SetActive(true);
        }
    }

    IEnumerator HideFailureIconAfterDelay()
    {
        yield return new WaitForSeconds(failureIconDisplayDuration);

        if (failureIcon != null)
        {
            failureIcon.SetActive(false);
        }
    }

    void EvaluateBreaths()
    {
        if (validBreathsInLast6Seconds >= 4)
        {
            Debug.Log("Breaths are valid. No penalty.");
            validBreathsInLast6Seconds = 0;
        }
        else
        {
            int decreaseAmount = Mathf.Max((4 - validBreathsInLast6Seconds) * Random.Range(2, 6) * failedAttempts, 30);
            heartRate -= decreaseAmount;
            heartRate = Mathf.Max(heartRate, 30);

            Debug.Log("Breaths are not valid. Heart rate decreased by: " + decreaseAmount);
            failedAttempts++;
            validBreathsInLast6Seconds = 0;
        }
    }

    void UpdateUI()
    {
        if (heartRateText != null)
        {
            heartRateText.text = heartRate.ToString();
        }

        if (respiratoryRateText != null)
        {
            respiratoryRateText.text = respiratoryRate.ToString();
        }
    }
    //存储游戏完成情况
    void SaveLevelDetails()
    {
        // 只有在关卡完成并且数据还没有被发送时才发送数据
        if (levelCompleted)
        {
            finishedTime = (failedAttempts == 0) ? 0 : 1;
            DataSender.Instance.SaveLevelDetails(gamelevel, finishedTime, isRight, SavePlayer.Ins.PlayerId);
        }
    }

    void UpdateTimer()
    {
        timer += Time.deltaTime;

        if (timerText != null)
        {
            int hours = Mathf.FloorToInt(timer / 3600);
            int minutes = Mathf.FloorToInt((timer % 3600) / 60);
            int seconds = Mathf.FloorToInt(timer % 60);
            timerText.text = hours.ToString("D2") + ": " + minutes.ToString("D2") + ": " + seconds.ToString("D2");
        }
    }
}
