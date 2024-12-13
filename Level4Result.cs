using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4Result : MonoBehaviour
{
    public int gamelevel = 4;
    public int finishedTime = 0;
    public int isRight = 0;
    public long idcode = 401;

    // 记录是否点击过错误按钮
    private bool clickedWrongButton = false;

    // 该方法在点击错误按钮时调用
    public void ClickWrongButton()
    {
        clickedWrongButton = true;
    }

    // 该方法在完成关卡时调用
    public void LevelComplete()
    {
        // 判断是否点击过错误按钮
        if (clickedWrongButton)
        {
            // 点击过错误按钮，finishedTime为1，isRight为0
            finishedTime = 1;
            isRight = 0;
        }
        else
        {
            // 未点击过错误按钮，finishedTime为0，isRight为1
            finishedTime = 0;
            isRight = 1;
        }

        // 使用 DataSender 实例保存游戏记录数据
        DataSender.Instance.SaveLevelDetails(gamelevel, finishedTime, isRight, idcode);
    }


}
