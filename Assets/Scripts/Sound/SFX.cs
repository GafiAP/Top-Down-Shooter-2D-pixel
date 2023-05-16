using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    private static SFX instance;
    [SerializeField] private AudioSource sfx,sfx1,sfx2,sfx3;
    [SerializeField] private AudioClip audioclip1, audioclip2, audioclip3, audioclip4, audioclip5, audioclip6;
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
    //stop play all sfx
    public void StopPlay()
    {
        sfx.Stop();
        sfx1.Stop();
        sfx2.Stop();
    }
    //play button sfx
    public void PlayButtonSFX()
    {
        sfx3.clip = audioclip1;
        sfx3.Play();
    }
    //play explode sfx
    public void ExplodeSFX()
    {
        sfx.clip = audioclip2;
        sfx.Play();
    }
    //play arrow hit sfx
    public void ArrowHitSFX()
    {
        sfx.clip = audioclip3;
        sfx.Play();
    }
    //play punch sfx
    public void PunchSFX()
    {
        sfx2.clip = audioclip4;
        sfx2.Play();
    }
    //play collectibles sfx
    public void CollectiblesSFx()
    {
        sfx1.clip = audioclip5;
        sfx1.Play();
    }
    //play spiketrap sfx
    public void SpikeTrapSFX()
    {
        sfx1.clip = audioclip6;
        sfx1.Play();
    }
}
