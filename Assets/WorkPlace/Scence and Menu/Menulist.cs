using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private AudioSource bgm;
    public GameObject gameUI;// 暂停界面时把ui隐藏
    public GameObject menuList;
    private bool menukey=true;

    Slider volumeSlider;
    Audio audioSystem;
    private void Awake()
    {
        volumeSlider = menuList.transform.Find("Menu").Find("Volume").GetComponent<Slider>();
    }
    void Start()
    {
        volumeSlider.value = 1;
        audioSystem = GameObject.FindGameObjectWithTag("Audio").GetComponent<Audio>();
        menuList.SetActive(false);
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
                //audioSystem.SetVolum(audioSystem.curVolum/2);
                // bgm.Pause();//bgm暂停
                gameUI.SetActive(false);
               
            }
            
        }
        else// 菜单中
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                menuList.SetActive(false);
                menukey = true;
                Time.timeScale = (1);//时间恢复正常
                //audioSystem.SetVolum(audioSystem.curVolum * 2);
                gameUI.SetActive(true);
            }
            audioSystem.SetVolum((float)(volumeSlider.value));
        }

    }
    public void Return()//返回游戏
    {
        //按键音效
        audioSystem.PlaySFX(audioSystem.selectMenu);

        menuList.SetActive(false);
        menukey = true;
        Time.timeScale = (1);//时间恢复正常
        bgm.Play();//bgm播放
        gameUI.SetActive(true);
    }
    public void Restart()//重开
    {
        //按键音效
        audioSystem.PlaySFX(audioSystem.selectMenu);

        SceneManager.LoadScene(0);
        Time.timeScale = 1;
        gameUI.SetActive(true);
    }
    public void Quit()//退出游戏
    {
        //按键音效
        audioSystem.PlaySFX(audioSystem.selectMenu);

        Application.Quit();
    }
}
