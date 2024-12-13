using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;
using System.Text;

public class GameStart : MonoBehaviour
{
    public InputField playerNameInputField;
    public Text dialogText;
    public GameObject currentDialogPanel;
    public GameObject nextDialogPanel;
    //控制游戏开始的一个单例
    public GameObject startmgr;

    private const string serverURL = "http://cc.savethebaby.cn/doctor/api/player/saveOrUpdate";
    private const string playerNameKey = "PlayerName";
    private const string playerIpKey = "PlayerIp";


    private bool dialogShown = false;

    private void Start()
    {
        //单例关闭
        startmgr.SetActive(false);
    }

    public void StartGame()
    {
        ShowInputDialog("您的名字是？");
    }

    public void SetPlayerName()
    {
        //单例开启
        startmgr.SetActive(true);
        string playerName = playerNameInputField.text;

        if (string.IsNullOrEmpty(playerName))
        {
            Debug.LogError("玩家名字不能为空！");
            return;
        }

        string playerIp = Search_Ip.ins.player_ip;
        
        //查询本地的公网ip
        Debug.Log("公网ip：" + playerIp);
        //存储玩家数据
        PlayerPrefs.SetString(playerNameKey, playerName);
        PlayerPrefs.SetString(playerIpKey, playerIp);
        UploadPlayerToServer(playerName, playerIp);

        playerNameInputField.gameObject.SetActive(false);

        ShowNextDialog($"您好，{playerName}。");
    }

    void ShowInputDialog(string message)
    {
        playerNameInputField.gameObject.SetActive(true);
        dialogText.text = message;
        dialogShown = false;
    }

    void ShowNextDialog(string message)
    {
        if (dialogShown)
        {
            nextDialogPanel.SetActive(true);
            currentDialogPanel.SetActive(false);
            dialogShown = false;
        }
        else
        {
            dialogText.text = message;
            ShowThisDialog();
        }
    }

    void ShowThisDialog()
    {
        string playerName = PlayerPrefs.GetString("PlayerName", "默认名字");
        dialogText.text = $"{playerName}，你好！";
        dialogShown = true;
    }

    void UploadPlayerToServer(string playerName, string playerIp)
    {
        StartCoroutine(UploadPlayerData(playerName, playerIp));
    }

    IEnumerator UploadPlayerData(string playerName, string playerIp)
    {
        Debug.Log($"JSON 数据: playerName={playerName}, playerIp={playerIp}");

        Dictionary<string, string> data = new Dictionary<string, string>
        {
            { "playerName", playerName },
            { "playerIp", playerIp }
        };

        string jsonData = DictToJson(data);
        Debug.Log($"Sending JSON data to server: {jsonData}");

        byte[] postData = Encoding.UTF8.GetBytes(jsonData);

        foreach (var entry in data)
        {
            Debug.Log($"字典内容: Key = {entry.Key}, Value = {entry.Value}");
        }

        Debug.Log($"上传前的 JSON 参数: {JsonUtility.ToJson(data)}");

        using (UnityWebRequest www = new UnityWebRequest(serverURL, UnityWebRequest.kHttpVerbPOST))
        {
            www.uploadHandler = new UploadHandlerRaw(postData);
            www.downloadHandler = new DownloadHandlerBuffer();

            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"上传玩家数据失败。错误: {www.error}");
            }
            else
            {
                Debug.Log("成功上传玩家数据！");

                Debug.Log($"服务器响应: {www.downloadHandler.text}");

                HandleServerResponse(www.downloadHandler.text);

                GameStartupManager.Instance.StartGame();
            }
        }
    }

    string DictToJson(Dictionary<string, string> dict)
    {
        StringBuilder jsonString = new StringBuilder("{");

        foreach (var pair in dict)
        {
            jsonString.AppendFormat("\"{0}\":\"{1}\",", pair.Key, pair.Value);
        }

        jsonString.Length--; 
        jsonString.Append("}");

        return jsonString.ToString();
    }

    void HandleServerResponse(string response)
    {
        if (!string.IsNullOrEmpty(response))
        {
            try
            {
                SucResponse sucResponse = JsonUtility.FromJson<SucResponse>(response);
                Debug.Log($"解析服务端数据: {"msg: "+sucResponse.msg+" "+"code: "+sucResponse.code+" "+"data: "+sucResponse.data}");
            }
            catch (Exception ex)
            {
                Debug.LogError($"解析 JSON 错误: {ex.Message}");
                Debug.LogError($"来自服务器的原始 JSON: {response}");
            }
        }
    }

    //注册登录接口，服务器返回信息
    [Serializable]
    public class SucResponse
    {
        public string msg;
        public string code;
        public string data;
    }



}