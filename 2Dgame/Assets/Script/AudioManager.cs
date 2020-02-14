using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    static AudioManager current;
    [Header("按钮音效")]
    public AudioClip Btnenter;
    public AudioClip Btnclick;

    [Header("环境声音")]
    public AudioClip ambientClip;       //环境音效
    public AudioClip musicClip;         //背景音乐

    [Header("Robbie音效")]
    public AudioClip[] walkStepClips;
    public AudioClip[] crouchStepClips;
    public AudioClip jumpClip;
    public AudioClip jumpVoiceClip;     //跳跃说话声



    AudioSource Btnsource;              //按钮播放器
    AudioSource ambientSource;
    AudioSource musicSource;
    AudioSource playerSourece;
    AudioSource voiceSource;
    void Awake()
    {
        current = this;

        DontDestroyOnLoad(gameObject);

        Btnsource = gameObject.AddComponent<AudioSource>();
        ambientSource = gameObject.AddComponent<AudioSource>();
        musicSource = gameObject.AddComponent<AudioSource>();
        playerSourece = gameObject.AddComponent<AudioSource>();
        voiceSource = gameObject.AddComponent<AudioSource>();
    }
    //鼠标进入按钮音效
    public static void BtnenterSource()   
    {
        current.Btnsource.clip = current.Btnenter;
        current.Btnsource.Play();
    }
    //按钮按下音效
    public static void BtnclickSource()  
    {
        current.Btnsource.clip = current.Btnclick;
        current.Btnsource.Play();
    }
    //走、跑音效
    public static void PlayFootstepAudio() 
    {
        int Index = Random.Range(0, current.walkStepClips.Length);
        current.playerSourece.clip = current.walkStepClips[Index];
        current.playerSourece.Play();
    }
    //下蹲走路音效
    public static void PlayCrouchstepAudio() 
    {
        int Index = Random.Range(0, current.crouchStepClips.Length);
        current.playerSourece.clip = current.crouchStepClips[Index];
        current.playerSourece.Play();
    }
    //环境音效
    public static void StartLevelAudio()  
    {
        current.ambientSource.clip = current.ambientClip;
        current.ambientSource.loop = true;
        current.ambientSource.Play();

        current.musicSource.clip = current.musicClip;
        current.musicSource.loop = true;
        current.musicSource.Play();
    }
    //跳跃音效
    public static void PlayJumpAudio()
    {
        current.playerSourece.clip = current.jumpClip;
        current.playerSourece.Play();

        current.voiceSource.clip = current.jumpVoiceClip;
        current.voiceSource.Play();
    }
}
