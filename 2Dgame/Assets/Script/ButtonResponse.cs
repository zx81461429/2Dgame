using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonResponse : MonoBehaviour
{
    public GameObject Btn;
    public GameObject Option;
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
        SceneManager.LoadScene(1);
    }
}
