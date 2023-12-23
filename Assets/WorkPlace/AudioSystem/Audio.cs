using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    //������Ч��Audio����
    [SerializeField] AudioSource bgmPlayer;//���������ֲ���
    [SerializeField] AudioSource sfxPlayer;//������Ч����

        //������Ч
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
    //��ʼʱ����bgm
    private void Start()
    {
        bgmPlayer.clip = bgm;
        bgmPlayer.Play();
    }   

    //�ṩ��������Ϸ�������Ƶ���ţ�������Ϸ�������audio�������������Ч
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
