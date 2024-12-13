using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GameResultDisplay : MonoBehaviour
{
    public Text resultText;
    public GameTimer gameTimer;
    public BabySimulation babySimulation;
    public GameTimer gameTimer3;
    public Level4Result level4Result;

    private string playerName = "默认名字";

    IEnumerator Start()
    {
        // 尝试从服务器获取玩家名字
        yield return StartCoroutine(GetPlayerNameFromServer());

        DataSender.Instance.CreateUser("playerIp");

 
    }

    IEnumerator GetPlayerNameFromServer()
    {
        string serverEndpoint = "http://172.21.175.226/playerName";

        UnityWebRequest request = UnityWebRequest.Get(serverEndpoint);
        request.SetRequestHeader("Authorization", "Basic " + System.Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes("administrator:Doctor12")));

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string playerNameFromServer = request.downloadHandler.text;
            PlayerPrefs.SetString("PlayerName", playerNameFromServer);
        }
        else
        {
            Debug.LogError($"Failed to get player name from server. Error: {request.error}");
        }
    }

    void Update()
    {
        // 从各个脚本中获取游戏记录数据
        int level1 = gameTimer.gamelevel;
        int finishedTime1 = gameTimer.finishedTime;
        int isRight1 = gameTimer.isRight;
        long idCode1 = gameTimer.idcode;

        int level2 = babySimulation.gamelevel;
        int finishedTime2 = babySimulation.finishedTime;
        int isRight2 = babySimulation.isRight;
        long idCode2 = babySimulation.idcode;

        int level3 = gameTimer3.gamelevel;
        int finishedTime3 = gameTimer3.finishedTime;
        int isRight3 = gameTimer3.isRight;
        long idCode3 = gameTimer3.idcode;

        int level4 = level4Result.gamelevel;
        int finishedTime4 = level4Result.finishedTime;
        int isRight4 = level4Result.isRight;
        long idCode4 = level4Result.idcode;

        // 根据游戏结果生成相应的文字内
        string resultMessage = $"{playerName}医生，你好！" + "\n恭喜你成功完成了复苏！\n现在新生儿可以安置在病房\n\n";

        // 关卡1结果
        resultMessage += "关卡1：";
        resultMessage += GetLevelResultMessage(finishedTime1, isRight1);

        // 关卡2结果
        resultMessage += "\n关卡2：";
        resultMessage += GetLevelResultMessage2(isRight2);

        // 关卡3结果
        resultMessage += "\n关卡3：";
        resultMessage += GetLevelResultMessage(finishedTime3, isRight3);

        // 关卡4结果
        resultMessage += "\n关卡4：";
        resultMessage += GetLevelResultMessage4(isRight4);

        // 给出总评价
        resultMessage += "\n\n总评价：";
        resultMessage += GetTotalEvaluation(isRight1, isRight2, isRight3, isRight4);

        resultText.text = resultMessage;


    }

    // 获取关卡的结果消息
    string GetLevelResultMessage(int finishedTime, int isRight)
    {
        string levelResultMessage;

        if (isRight == 1)
        {
            levelResultMessage = string.Format("本关卡完成用时 {0} 秒，准时！", finishedTime);
        }
        else
        {
            levelResultMessage = string.Format("本关卡完成用时 {0} 秒，超时！", finishedTime);
        }

        return levelResultMessage;
    }

    // 获取2关卡的结果消息
    string GetLevelResultMessage2(int isRight)
    {
        string levelResultMessage;

        if (isRight == 1)
        {
            levelResultMessage = string.Format("本关卡按压达到规定心率，成功！");
        }
        else
        {
            levelResultMessage = string.Format("本关卡按压未达到规定心率，失败！");
        }

        return levelResultMessage;
    }

    // 获取4关卡的结果消息
    string GetLevelResultMessage4(int isRight)
    {
        string levelResultMessage;

        if (isRight == 1)
        {
            levelResultMessage = string.Format("本关卡没有点选错误，成功！");
        }
        else
        {
            levelResultMessage = string.Format("本关卡曾点选错误，失败！");
        }

        return levelResultMessage;
    }

    // 获取总评价
    string GetTotalEvaluation(params int[] isRightArray)
    {
        int totalErrors = 0;

        foreach (int isRight in isRightArray)
        {
            if (isRight == 0)
            {
                totalErrors++;
            }
        }

        if (totalErrors == 0)
        {
            return "优秀的宝宝救星！";
        }
        else if (totalErrors <= 2)
        {
            return "勉强合格的实习医生！";
        }
        else
        {
            return "还需要努力学习 ！";
        }
    }
}
