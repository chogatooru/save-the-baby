using UnityEngine;
using UnityEngine.UI;

public class LongPressButton : MonoBehaviour
{
    public GameObject objectA;
    public GameObject objectB;
    public Text countdownText; 
    public Image longPressImage; // 添加UGUI Image 变量
    public Sprite[] longPressSprites; // 图片数组，包含图形0到图形3

    private float pressStartTime = 0f;
    private bool isButtonPressed = false;

    void Update()
    {
        // 如果按钮被按下
        if (Input.GetButtonDown("Fire1"))
        {
            pressStartTime = Time.time;
            isButtonPressed = true;

            // 设置初始图形为图形0
            if (longPressImage != null && longPressSprites.Length > 0)
            {
                longPressImage.sprite = longPressSprites[0];
            }
        }

        // 持续按下
        if (Input.GetButton("Fire1") && isButtonPressed)
        {
            float elapsedTime = Time.time - pressStartTime;

            // 长按3秒后执行相应操作
            if (elapsedTime >= 3f)
            {
                // 隐藏游戏对象A，显示游戏对象B
                objectA.SetActive(false);
                objectB.SetActive(true);
            }
            
            // 更新倒计时文本
            UpdateCountdownText(elapsedTime);

            // 更新长按图片
            UpdateLongPressImage(elapsedTime);
        }

        // 松开按钮
        if (Input.GetButtonUp("Fire1"))
        {
            isButtonPressed = false;
            ResetCountdownText();
            
            // 按钮松开后，重置长按图片为图形0
            if (longPressImage != null && longPressSprites.Length > 0)
            {
                longPressImage.sprite = longPressSprites[0];
            }
        }
    }

    void UpdateCountdownText(float elapsedTime)
    {
        // 计算剩余时间
        float remainingTime = Mathf.Max(0f, 3f - elapsedTime);

        // 将剩余时间显示在Text组件上
        if (countdownText != null)
        {
            countdownText.text = remainingTime.ToString("F1");
        }
    }

    void UpdateLongPressImage(float elapsedTime)
    {
        // 计算当前应该显示的图形索引
        int currentSpriteIndex = Mathf.Clamp(Mathf.FloorToInt(elapsedTime), 0, longPressSprites.Length - 1);

        // 将对应图形显示在Image组件上
        if (longPressImage != null && longPressSprites.Length > 0)
        {
            longPressImage.sprite = longPressSprites[currentSpriteIndex];
        }
    }

    void ResetCountdownText()
    {
        // 重置倒计时文本
        if (countdownText != null)
        {
            countdownText.text = "";
        }
    }
}
