using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDisplayController : MonoBehaviour
{
    public GameObject firstObject; // 第一个游戏对象
    public GameObject secondObject; // 第二个游戏对象
    public float delayInSeconds = 3f; // 等待时间，可在编辑器中设置

    private void Start()
    {
        // 初始化，将第二个对象隐藏
        if (secondObject != null)
        {
            secondObject.SetActive(false);
        }

        // 启动协程，等待指定时间后显示第二个对象
        StartCoroutine(ShowSecondObjectAfterDelay());
    }

    IEnumerator ShowSecondObjectAfterDelay()
    {
        yield return new WaitForSeconds(delayInSeconds); // 等待指定时间

        // 显示第二个对象
        if (secondObject != null)
        {
            firstObject.SetActive(false);
            secondObject.SetActive(true);
        }
    }
}

