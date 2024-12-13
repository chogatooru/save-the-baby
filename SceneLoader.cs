using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string targetSceneName = "YourTargetScene"; // 设置目标场景名称
    public float waitTime = 3f; // 设置等待时间（秒）

    void Start()
    {
        StartCoroutine(LoadSceneAfterDelay());
    }

    IEnumerator LoadSceneAfterDelay()
    {
        yield return new WaitForSeconds(waitTime);

        // 使用SceneManager加载目标场景
        SceneManager.LoadScene(targetSceneName);
    }
}

