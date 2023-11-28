using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject menuList;
    [SerializeField] private AudioSource bgm;
    [SerializeField] private bool menukey=true;

    Audio selectSFX;
    void Start()
    {
        selectSFX=GameObject.FindGameObjectWithTag("Audio").GetComponent<Audio>();
    }

    // Update is called once per frame
    void Update()
    {
        if (menukey)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                menuList.SetActive(true);
                menukey = false;
                Time.timeScale = (0);//时间暂停
                bgm.Pause();//bgm暂停
            }
        }
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            menuList.SetActive(false);
            menukey = true;
            Time.timeScale = (1);//时间恢复正常
            bgm.Play();//bgm播放
        }
    }
    public void Return()//返回游戏
    {
        //按键音效
        selectSFX.PlaySFX(selectSFX.selectMenu);

        menuList.SetActive(false);
        menukey = true;
        Time.timeScale = (1);//时间恢复正常
        bgm.Play();//bgm播放
    }
    public void Restart()//重开
    {
        //按键音效
        selectSFX.PlaySFX(selectSFX.selectMenu);

        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
    public void Quit()//退出游戏
    {
        //按键音效
        selectSFX.PlaySFX(selectSFX.selectMenu);

        Application.Quit();
    }
}
