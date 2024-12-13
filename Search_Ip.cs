using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;


//��ѯ����ip
public class Search_Ip : MonoBehaviour
{
    public static Search_Ip ins; 
    public string player_ip = null;
    private const string apiUrl = "http://httpbin.org/ip";
    // Start is called before the first frame update
    private void Awake()
    {
        ins = this;
    }

    private void Start()
    {
        StartCoroutine(FetchPublicIP());
    }
    
    IEnumerator FetchPublicIP()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(apiUrl))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error fetching public IP: " + www.error);
            }
            else
            {
                string responseText = www.downloadHandler.text;
                ParseIPResponse(responseText);
            }
        }
    }

    //��ѯip���ص����ݸ�ʽ
    [System.Serializable]
    public class TextCallback
    {
        public string origin;
    }


    private void ParseIPResponse(string response)
    {
        // Parse the response to extract the public IP address
        // The response might be in JSON format, so you need to use a JSON parser or string manipulation
        // to extract the relevant information.
        // This is just a basic example and may need modification based on the actual response format.
        string[] splitResponse = response.Split(':');
        if (splitResponse.Length > 1)
        {
            //�������ص�����
            TextCallback textCallback = JsonUtility.FromJson<TextCallback>(response);
            
            string ipAddress = textCallback.origin;
            Debug.Log("Public IP Address: " + ipAddress);
            player_ip = ipAddress;
        }
        else
        {
            Debug.LogError("Unable to parse public IP address from response.");

        }
    }


}
