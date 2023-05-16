using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackSoundManager : MonoBehaviour
{
    private static BackSoundManager instance;
    [SerializeField] private AudioSource bgm;
    [SerializeField] private AudioClip bgmClip1,bgmClip2,bgmClip3,bgmClip4;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    //play main menu bgm
    public void PlayMainMenuBGM()
    {
        Time.timeScale = 1f;
        bgm.clip = bgmClip1;
        bgm.Play();
    }
    //play battle bgm
    public void PlayBattleBGM()
    {
        bgm.clip = bgmClip2;
        bgm.Play();
    }
    //play gameover bgm
    public void PlayGameOverBGM()
    {
        bgm.clip = bgmClip3;
        bgm.Play();
    }
    //play completion level bgm
    public void PlayCompletionLevelBGM()
    {
        bgm.clip = bgmClip4;
        bgm.Play();
    }
}
