using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleAnimation : MonoBehaviour
{
    public float scaleSpeed = 2f; // 缩放速度
    public float maxScale = 1.5f; // 最大缩放值
    public float minScale = 0.5f; // 最小缩放值

    private void Update()
    {
        float scale = Mathf.Sin(Time.time * scaleSpeed) * 0.5f + 1.0f; // 生成0.5到1.5之间的正弦波曲线

        // 限制缩放在minScale到maxScale之间
        scale = Mathf.Clamp(scale, minScale, maxScale);

        // 应用缩放
        transform.localScale = new Vector3(scale, scale, 1f);
    }
}
