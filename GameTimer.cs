using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public Text timerText;
    public Button buttonA;
    public Button buttonB;

    public float timenum = 10f;
    private bool isTiming = false;
    private float elapsedTime = 0f;

    public int gamelevel = 1;

    public int finishedTime = 0;
    public int isRight = 0;
    public long idcode = 101;

    void Start()
    {
        // 添加按钮点击事件监听器
        buttonA.onClick.AddListener(StartTimer);
        buttonB.onClick.AddListener(StopTimer);
    }

    void Update()
    {
        // 如果正在计时，更新计时器显示
        if (isTiming)
        {
            timerText.gameObject.SetActive(true);
            elapsedTime += Time.deltaTime;
            UpdateTimerText();
        }
    }

    void StartTimer()
    {
        // 开始计时
        isTiming = true;
        elapsedTime = 0f;
    }

    void StopTimer()
    {
        // 停止计时
        isTiming = false;

        // 判断是否超时
        bool isTimeout = elapsedTime > timenum;

        // 显示结果
        if (isTimeout)
        {
            timerText.text = "超时";
        }
        else
        {
            timerText.text = "准时";

            // 游戏记录数据
            finishedTime = Mathf.FloorToInt(elapsedTime);
            isRight = isTimeout ? 0 : 1;

            // 将游戏记录数据发送到服务器或者本地记录
            // 获取 DataSender 实例并调用 SaveLevelDetails 方法
            DataSender dataSender = FindObjectOfType<DataSender>();
            if (dataSender != null)
            {
                //dataSender.SaveLevelDetails(gamelevel, finishedTime, isRight, idcode);
                dataSender.SaveLevelDetails(gamelevel, finishedTime, isRight, SavePlayer.Ins.PlayerId);
            }
            else
            {
                Debug.LogError("DataSender not found!");
            }
        }
    }

    void UpdateTimerText()
    {
        // 将秒数转换为小时：分钟：秒钟格式
        int hours = Mathf.FloorToInt(elapsedTime / 3600f);
        int minutes = Mathf.FloorToInt((elapsedTime % 3600f) / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);

        // 更新Text显示
        timerText.text = string.Format("{0:D2}: {1:D2}: {2:D2}", hours, minutes, seconds);
    }
}
