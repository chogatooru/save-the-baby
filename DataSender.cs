using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;

public class DataSender : MonoBehaviour
{
    public string PlayerName { get; private set; }
    private string baseURL = "http://cc.savethebaby.cn/doctor";

    private static DataSender _instance;
    public static DataSender Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<DataSender>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(DataSender).Name;
                    _instance = obj.AddComponent<DataSender>();
                }
            }
            return _instance;
        }
    }


    public string Post_res { get; private set; }
    
    // 创建用户
    public void CreateUser(string playerIp)
{
    // 获取存储的玩家名字
    PlayerName = PlayerPrefs.GetString("PlayerName", "默认名字");
    string url = baseURL + "/api/player/saveOrUpdate";
    PlayerData playerData = new PlayerData(PlayerName, playerIp);

    // 将 PlayerData 对象转换为 JSON 字符串
    string json = JsonUtility.ToJson(playerData);

    StartCoroutine(PostRequest(url, json));
}

// 创建游戏记录
public void CreateGameRecord(string pip, string startDate, string pName)
{
    string url = baseURL + "/api/record/save";
    GameRecordData gameRecordData = new GameRecordData(pip, startDate, pName);

    // 将 GameRecordData 对象转换为 JSON 字符串
    string json = JsonUtility.ToJson(gameRecordData);

    StartCoroutine(PostRequest(url, json));
    //GameObject obi = GameObject.FindWithTag("SavePlayer");
    //SavePlayer saveplayer= obi.GetComponent<SavePlayer>();
    //    //解析Postres返回的Player_id
    //    Debug.Log("Post_res" + Post_res);
    //    if (Post_res!=null) 
    //    {
    //        Debug.Log("Post_res"+Post_res);
        
    //    }
}


// 保存关卡完成情况
public void SaveLevelDetails(int level, int finishedTime, int isRight, long gameRecordId)
{
    string url = baseURL + "/api/record/saveDetail";
    LevelDetailsData levelDetailsData = new LevelDetailsData(level, finishedTime, isRight, gameRecordId);

    // 将 LevelDetailsData 对象转换为 JSON 字符串
    string json = JsonUtility.ToJson(levelDetailsData);

    StartCoroutine(PostRequest(url, json));
}

    // 发送 POST 请求
    IEnumerator PostRequest(string url, string jsonData)
    {
        Debug.Log("Request JSON Data: " + jsonData);
        using (UnityWebRequest www = new UnityWebRequest(url, "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();

            // 设置请求头
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + www.error);
            }
            else
            {
                Debug.Log("Request successful");
                Debug.Log("Response: " + www.downloadHandler.text);

                // 处理响应，如果需要
                string resultMessage = "Response: " + www.downloadHandler.text;
                // 这里可以使用 resultMessage 做进一步处理
                Post_res = resultMessage;
            }
        }
    }

    [System.Serializable]
    private class PlayerData
    {
        public string PlayerName;
        public string playerIp;

        public PlayerData(string name, string ip)
        {
            PlayerName = name;
            playerIp = ip;
        }
    }

    [System.Serializable]
    private class GameRecordData
    {
        public string pip;
        public string startDate;
        public string pName;

        public GameRecordData(string playerIp, string start, string name)
        {
            pip = playerIp;
            startDate = start;
            pName = name;
        }
    }

    [System.Serializable]
    private class LevelDetailsData
    {
        public int level;
        public int finishedTime;
        public int isRight;
        public long gameRecordId;

        public LevelDetailsData(int lvl, int time, int right, long recordId)
        {
            level = lvl;
            finishedTime = time;
            isRight = right;
            gameRecordId = recordId;
        }
    }
}
