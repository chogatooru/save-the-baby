using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class SavePlayer : MonoBehaviour
{
    public static SavePlayer Ins;
    //public string PlayerIp;
    //public string PlayerName;
    public string PlayerDate;
    public long PlayerId;

    private bool IsJieXi = false;
    private string baseURL = "http://cc.savethebaby.cn/doctor";

    public string Post_res { get; set; }
    
    private void Awake()
    {
        Ins = this;
        Post_res = null;
        System.DateTime currentTime = System.DateTime.Now;

        // 将时间格式化为指定的字符串
        string formattedTime = currentTime.ToString("yyyy-MM-dd HH:mm:ss");

        // 打印格式化后的时间字符串
        Debug.Log("Formatted Time: " + formattedTime);
        PlayerDate = formattedTime;
    }


    // 创建游戏记录
    public void CreateGameRecord(string pip, string startDate, string pName)
    {
        string url = baseURL + "/api/record/save";
        GameRecordData gameRecordData = new GameRecordData(pip, startDate, pName);

        // 将 GameRecordData 对象转换为 JSON 字符串
        string json = JsonUtility.ToJson(gameRecordData);

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
        public class GameSavePosData
        {
            public string msg;
            public int code;
            public Data data;

            [System.Serializable]
            public class Data
            {
                public long id;
                public string pip;
                public string startDate;
                public string finishDate;
                public string totalPoint;
                public string pName;
            }
         }


    // Start is called before the first frame update
    void Start()
    {
        //调用该脚本里的GameRecord
        CreateGameRecord(PlayerPrefs.GetString("PlayerIp"),PlayerDate, PlayerPrefs.GetString("PlayerName"));
    }


    // Update is called once per frame
    void Update()
    {
        if (Post_res != null)
        {
            if (!IsJieXi)
            {
                Debug.Log("Post_res:" + Post_res);
                string res = RemovePrefix(Post_res, "Response:");
                Debug.Log(res);
                ////解析返回的数据
                GameSavePosData gamesavedData = JsonUtility.FromJson<GameSavePosData>(res);
                Debug.Log(gamesavedData.data.id);
                PlayerId = gamesavedData.data.id;
                IsJieXi = true;

            }
        }


    }


    string RemovePrefix(string input, string prefix)
    {
        // 检查字符串是否以指定前缀开头
        if (input.StartsWith(prefix))
        {
            // 使用Substring方法去除前缀
            return input.Substring(prefix.Length);
        }

        // 如果不是以指定前缀开头，则返回原始字符串
        return input;
    }
}
