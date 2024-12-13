
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    [System.Serializable]
    public struct TimeNodeSettings
    {
        public Button button; // 对应的按钮
        public Text displayText; // 显示时间的 UI 文本
        public Color textColor; // 文本颜色
        public int seconds; // 对应的秒数
    }

    public TimeNodeSettings[] timeNodeSettings; // 时间节点设置数组
    private int currentTimeIndex = -1; // 当前时间节点索引，初始化为 -1

    private void Start()
    {
        // 为每个按钮添加点击事件
        foreach (var settings in timeNodeSettings)
        {
            if (settings.button != null)
            {
                settings.button.onClick.AddListener(() => FastForwardToNode(settings));
            }
        }

        // 初始化显示时间
        UpdateTimeDisplay();
    }

    void FastForwardToNode(TimeNodeSettings settings)
    {
        // 更新当前时间节点索引
        currentTimeIndex = System.Array.IndexOf(timeNodeSettings, settings);

        // 更新显示时间
        UpdateTimeDisplay();
    }

    void UpdateTimeDisplay()
    {
        // 遍历时间节点设置数组，更新显示时间
        for (int i = 0; i < timeNodeSettings.Length; i++)
        {
            if (i == currentTimeIndex)
            {
                // 当前时间节点显示为对应颜色
                timeNodeSettings[i].displayText.text = "<color=#" + ColorUtility.ToHtmlStringRGB(timeNodeSettings[i].textColor) + ">" + GetTimeString(timeNodeSettings[i].seconds) + "</color>";
            }
            else
            {
                // 其他时间节点显示为默认颜色
                timeNodeSettings[i].displayText.text = GetTimeString(timeNodeSettings[i].seconds);
            }
        }
    }

    string GetTimeString(int seconds)
    {
        // 根据秒数生成时间字符串
        int hours = seconds / 3600;
        int minutes = (seconds % 3600) / 60;
        int remainingSeconds = seconds % 60;

        return string.Format("{0:D2}:{1:D2}:{2:D2}", hours, minutes, remainingSeconds);
    }
}
