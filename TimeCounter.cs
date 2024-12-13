using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimeCounter : MonoBehaviour
{
    public Text timeText;
    private float currentTime = 0f;
    private float timeSpeed = 6f;

    void Start()
    {
        StartCoroutine(CountTime());
    }

    IEnumerator CountTime()
    {
        while (currentTime < 60f)
        {
            currentTime += Time.deltaTime * timeSpeed;
            UpdateTimeText();
            yield return null;
        }

        // 确保文本显示为60秒
        currentTime = 60f;
        UpdateTimeText();
    }

    void UpdateTimeText()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);

        // 格式化为 时钟：分钟：秒
        string formattedTime = string.Format("{0:00}: {1:00}: {2:00}", 00, minutes, seconds);

        // 更新UI上的时间文本
        timeText.text = formattedTime;
    }
}


