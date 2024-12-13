using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public int targetSceneIndex; // 目标场景的编号

    public void SwitchToScene()
    {
        // 切换到指定场景
        SceneManager.LoadScene(targetSceneIndex);
    }
}

