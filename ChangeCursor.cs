using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeCursor : MonoBehaviour
{
    public Texture2D customCursor; // 要设置的鼠标图形
    public Button changeCursorButton; // 触发更改鼠标图形的按钮

    void Start()
    {
        // 添加按钮点击事件
        if (changeCursorButton != null)
        {
            changeCursorButton.onClick.AddListener(ChangeMouseCursor);
        }
    }

    void ChangeMouseCursor()
    {
        // 设置鼠标图形
        Cursor.SetCursor(customCursor, Vector2.zero, CursorMode.Auto);
    }
}
