using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ButtonResponse : MonoBehaviour
{
    public GameObject Btn;
    public GameObject Option;
    public GameObject loadScreen;


    public Slider slider;
    public Text sliderpro;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void MouseEnter()
    {
        AudioManager.BtnenterSource();
    }

    public void BtnOption()
    {
        AudioManager.BtnclickSource();
        Btn.SetActive(false);
        Option.SetActive(true);
    }

    public void BtnBack()
    {
        AudioManager.BtnclickSource();
        Btn.SetActive(true);
        Option.SetActive(false);
    }

    public void BtnQuit()
    {
        AudioManager.BtnclickSource();
        Application.Quit();
    }

    public void BtnStart()
    {
        AudioManager.BtnclickSource();
        StartCoroutine(Loadlevel());
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
            }
            yield return null;
        }
    }
}
