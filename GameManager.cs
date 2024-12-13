using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Transform Target
{
    get { return destinationArea; }
}
    [Header("Game Elements")]
    public Transform player; // 玩家对象
    public Transform destinationArea; // 下一个区域的位置
    public GameObject guideCircle; // 圆圈指引

    [Header("UI Elements")]
    public GameObject dialogPanel; // 对话框面板
    public string nextSceneName; // 下一个场景的名称
    public int nextSceneBuildIndex; // 下一个场景的编号

    private bool reachedDestination = false;
    public bool nextsceee = true;

    void Update()
    {
        if (!reachedDestination)
        {
            CheckPlayerArrival();
        }
    }

    void CheckPlayerArrival()
    {
        // 计算玩家和目标区域之间的距离
        float distanceToDestination = Vector3.Distance(player.position, destinationArea.position);

        // 如果玩家抵达目标区域，触发相应的事件
        if (distanceToDestination < 1.0f)
        {
            reachedDestination = true;
            ShowDialog("Welcome to the next area!");
            ShowGuideCircle(true);
            if (nextsceee){

                MoveToNextScene();

            }
            this.gameObject.SetActive(false);
        }
        else
        {
           // ShowGuideCircle(true);
        }
    }

    void ShowDialog(string message)
    {
        // 在这里你可以显示对话框，并传递相应的消息
        if (dialogPanel != null)
        {
            dialogPanel.SetActive(true);
            // 设置对话框中的文本内容
            // dialogPanel.GetComponentInChildren<Text>().text = message;
            Debug.Log(message);
        }
    }

    void ShowGuideCircle(bool show)
    {
        // 在这里你可以控制是否显示圆圈指引
        if (guideCircle != null)
        {
            guideCircle.SetActive(show);
        }
    }

    void MoveToNextScene()
    {
        // 在这里编写移动到下一个场景的逻辑
        Debug.Log("Moving to the next scene...");

        // 根据选择，可以根据场景名称或场景编号进行加载
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else if (nextSceneBuildIndex >= 0 && nextSceneBuildIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneBuildIndex);
        }
        else
        {
            Debug.LogError("Invalid scene settings. Please provide a valid scene name or build index.");
        }
    }
}
