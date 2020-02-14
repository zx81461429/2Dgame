using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ButtonResponse : MonoBehaviour
{
    public GameObject StartBtn;  //开始界面
    public GameObject OptionBtn; //选项界面
    public GameObject loadScreen;//加载界面


    public Slider slider;       //加载进度条
    public Text sliderpro;      //加载文本
    void Start()
    {
        
    }
    //鼠标进入按钮
    public void MouseEnter()
    {
        AudioManager.BtnenterSource();
    }
    //选项按钮被点击时
    public void BtnOption()
    {
        AudioManager.BtnclickSource();
        StartBtn.SetActive(false);
        OptionBtn.SetActive(true);
    }
    //返回按钮被点击时
    public void BtnBack()
    {
        AudioManager.BtnclickSource();
        StartBtn.SetActive(true);
        OptionBtn.SetActive(false);
    }
    //退出按钮被点击时
    public void BtnQuit()
    {
        AudioManager.BtnclickSource();
        Application.Quit();
    }
    //开始按钮被点击时
    public void BtnStart()
    {
        AudioManager.BtnclickSource();
        StartCoroutine(Loadlevel());  //协程方式异步加载
    }

    IEnumerator Loadlevel()
    {
        loadScreen.SetActive(true);
        

        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        //operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            slider.value = operation.progress;
            sliderpro.text = 100 * slider.value + "%";

            if (slider.value >= 0.9f)
            {
                slider.value = 1;
                sliderpro.text = 100 + "%";
                AudioManager.StartLevelAudio();
            }
            yield return null;
        }
    }
}
