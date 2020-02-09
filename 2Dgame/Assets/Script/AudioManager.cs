using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    static AudioManager current;
    // Start is called before the first frame update
    [Header("按钮音效")]
    public AudioClip Btnenter;
    public AudioClip Btnclick;



    AudioSource Btnsource;
    void Awake()
    {
        current = this;

        DontDestroyOnLoad(gameObject);

        Btnsource = gameObject.AddComponent<AudioSource>();
    }

    public static void BtnenterSource()
    {
        current.Btnsource.clip = current.Btnenter;
        current.Btnsource.Play();
    }

    public static void BtnclickSource()
    {
        current.Btnsource.clip = current.Btnclick;
        current.Btnsource.Play();
    }
}
