using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject menuList;
    [SerializeField] private AudioSource bgm;
    [SerializeField] private bool menukey=true;
    void Start()
    {
        
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
}
