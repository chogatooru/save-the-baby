using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NumberController : MonoBehaviour
{
    public Text numberText;
    private float targetValue = 130f;
    private float startingValue;

    void Start()
    {
        // 获取 Text 组件上的当前文本值并将其转换为浮点数
        startingValue = float.Parse(numberText.text);

        // 启动协程，在10秒内以6倍速上涨到130
        StartCoroutine(IncreaseNumberOverTime());
    }

    IEnumerator IncreaseNumberOverTime()
    {
        float elapsedTime = 0f;
        float duration = 10f;
        float speed = 3f;

        while (elapsedTime < duration)
        {
            // 使用线性插值在10秒内从起始值到目标值
            float newValue = Mathf.Lerp(startingValue, targetValue, elapsedTime / duration);
            
            // 更新文本显示
            numberText.text = Mathf.FloorToInt(newValue).ToString();

            elapsedTime += Time.deltaTime * speed;
            yield return null;
        }

        // 最终将数字设置为目标值
        numberText.text = Mathf.FloorToInt(targetValue).ToString();
    }
}


