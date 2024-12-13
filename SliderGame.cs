using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderGame : MonoBehaviour
{    public Slider slider1;
    public Slider slider2;
    public Button completeButton;
    public Button restartButton;

    private bool isMousePressed = false;
    private bool isGameFinished = false;

    public Text timerText;

    public Text shenducm;
    public float shenduvalue=5;

    public GameObject nextDialog;
    public GameObject stepthree;

    public GameObject timerappear;



    void Start()
    {
        InitializeSliders();

        // 添加按钮点击事件监听器

    }

    void Update()
    {
        // 如果游戏已经结束，不再更新Slider的值
       // if (isGameFinished)
         //   return;

        // 如果鼠标左键按下
        if (Input.GetMouseButtonDown(0))
        {
            isMousePressed = true;
        }

        // 如果鼠标左键持续按下
        if (isMousePressed)
        {
            // 实时减少slider1的值
            if (slider1.value >= 7 ){
                slider1.value = Mathf.Clamp(slider1.value - Time.deltaTime, slider1.minValue, 14f);
                shenduvalue = 17 - slider1.value;
            }
            

            // 当slider1的值减至10时开始显示slider2
            if (slider1.value <= 8)
            {
                // 实时减少slider2的值
                slider2.value = Mathf.Clamp(slider2.value - Time.deltaTime, slider2.minValue, 8f);
                shenduvalue = 17 - slider2.value;
                slider2.gameObject.SetActive(true);
            }
        }

        // 如果鼠标左键释放
        if (Input.GetMouseButtonUp(0))
        {
            isMousePressed = false;

            // 游戏结束，显示按钮
            //isGameFinished = true;
            //completeButton.gameObject.SetActive(true);
            //restartButton.gameObject.SetActive(true);
        }

        shenducm.text = "深度："+Mathf.RoundToInt(shenduvalue).ToString()+"cm";
    }

    void CompleteGame()
    {
        // 判断是否在正确范围内
        if (slider1.value >= 6 && slider2.value >= 6 && slider2.value <= 8 && slider1.value <= 8)
        {
            timerText.text = "游戏完成，正确！";
            
            nextDialog.SetActive(true);
            timerappear.SetActive(true);
            stepthree.SetActive(false);
            
        }
        else
        {
            timerText.text = "数值不在正确范围，重来吧！";
           
        }
    }

    public void RestartGame()
    {
        InitializeSliders();

        // 重置游戏状态
        isMousePressed = false;
        //isGameFinished = false;

        

        //completeButton.gameObject.SetActive(false);
        //restartButton.gameObject.SetActive(false);
    }

    void InitializeSliders()
    {
        completeButton.onClick.AddListener(CompleteGame);
        restartButton.onClick.AddListener(RestartGame);
        // 初始化Slider的值
        slider1.maxValue = 12;
        slider1.minValue = 0;
        slider1.value = 12;

        slider2.maxValue = 8;
        slider2.minValue = 5;
        slider2.value =8;
        slider2.gameObject.SetActive(false);
    }
}