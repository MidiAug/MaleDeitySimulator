using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
                Time.timeScale = (0);//ʱ����ͣ
                bgm.Pause();//bgm��ͣ
            }
        }
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            menuList.SetActive(false);
            menukey = true;
            Time.timeScale = (1);//ʱ��ָ�����
            bgm.Play();//bgm����
        }
    }
    public void Return()//������Ϸ
    {
        menuList.SetActive(false);
        menukey = true;
        Time.timeScale = (1);//ʱ��ָ�����
        bgm.Play();//bgm����
    }
    public void Restart()//�ؿ�
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
    public void Quit()//�˳���Ϸ
    {
        Application.Quit();
    }
}
