using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
   public Transform player; // 玩家对象
    public float followSpeed = 5f; // 摄像机跟随速度
    public Vector2 minBounds; // 摄像机的最小边界
    public Vector2 maxBounds; // 摄像机的最大边界

    void Update()
    {
        if (player != null)
        {
            // 计算摄像机新的位置
            Vector3 targetPosition = new Vector3(player.position.x, player.position.y, transform.position.z);

            // 限制摄像机在指定范围内移动
            targetPosition.x = Mathf.Clamp(targetPosition.x, minBounds.x, maxBounds.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minBounds.y, maxBounds.y);

            // 使用插值平滑地移动摄像机
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
    }
}
