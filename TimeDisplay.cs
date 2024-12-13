using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TimeDisplay : MonoBehaviour
{
    private Text timeText;

    private void Start()
    {
        // 获取Text组件
        timeText = GetComponent<Text>();

        if (timeText == null)
        {
            Debug.LogError("Text component not found!");
        }
    }

    private void Update()
    {
        // 获取当前时间
        DateTime currentTime = DateTime.Now;

        // 格式化为xx:xx:xx
        string formattedTime = currentTime.ToString("HH: mm: ss");

        // 更新Text显示
        if (timeText != null)
        {
            timeText.text = formattedTime;
        }
    }
}