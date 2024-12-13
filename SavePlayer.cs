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

        // ��ʱ���ʽ��Ϊָ�����ַ���
        string formattedTime = currentTime.ToString("yyyy-MM-dd HH:mm:ss");

        // ��ӡ��ʽ�����ʱ���ַ���
        Debug.Log("Formatted Time: " + formattedTime);
        PlayerDate = formattedTime;
    }


    // ������Ϸ��¼
    public void CreateGameRecord(string pip, string startDate, string pName)
    {
        string url = baseURL + "/api/record/save";
        GameRecordData gameRecordData = new GameRecordData(pip, startDate, pName);

        // �� GameRecordData ����ת��Ϊ JSON �ַ���
        string json = JsonUtility.ToJson(gameRecordData);

        StartCoroutine(PostRequest(url, json));

    }

    // ���� POST ����
    IEnumerator PostRequest(string url, string jsonData)
    {
        Debug.Log("Request JSON Data: " + jsonData);
        using (UnityWebRequest www = new UnityWebRequest(url, "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();

            // ��������ͷ
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

                // ������Ӧ�������Ҫ
                string resultMessage = "Response: " + www.downloadHandler.text;
                // �������ʹ�� resultMessage ����һ������
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
        //���øýű����GameRecord
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
                ////�������ص�����
                GameSavePosData gamesavedData = JsonUtility.FromJson<GameSavePosData>(res);
                Debug.Log(gamesavedData.data.id);
                PlayerId = gamesavedData.data.id;
                IsJieXi = true;

            }
        }


    }


    string RemovePrefix(string input, string prefix)
    {
        // ����ַ����Ƿ���ָ��ǰ׺��ͷ
        if (input.StartsWith(prefix))
        {
            // ʹ��Substring����ȥ��ǰ׺
            return input.Substring(prefix.Length);
        }

        // ���������ָ��ǰ׺��ͷ���򷵻�ԭʼ�ַ���
        return input;
    }
}
