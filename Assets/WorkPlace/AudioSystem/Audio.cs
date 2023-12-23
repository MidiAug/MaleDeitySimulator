using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    //所有音效由Audio管理
    [SerializeField] AudioSource bgmPlayer;//管理背景音乐播放
    [SerializeField] AudioSource sfxPlayer;//管理音效播放

        //其他音效
    public AudioClip bgm;
    public AudioClip shootSFX1;
    public AudioClip lost;
    public AudioClip shootSFX2;
    public AudioClip getShot;
    public AudioClip levelUp;
    public AudioClip selectMenu;
    public AudioClip win;
    public AudioClip enemyDead;
    public AudioClip pickUpCoin;

    public float curVolum;
    //开始时播放bgm
    private void Start()
    {
        bgmPlayer.clip = bgm;
        bgmPlayer.Play();
    }   

    //提供给其他游戏对象的音频播放，其他游戏对象借助audio来播放自身的音效
    public void PlaySFX(AudioClip clip)
    {
        sfxPlayer.PlayOneShot(clip);
        if(clip==lost)
            bgmPlayer.Pause();
    }
    public void SetVolum(float val)
    {
        bgmPlayer.volume = sfxPlayer.volume = val;
    }

}
