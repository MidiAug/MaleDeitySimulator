using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private AudioSource bgm;
    public GameObject gameUI;// ��ͣ����ʱ��ui����
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
                Time.timeScale = (0);//ʱ����ͣ
                //audioSystem.SetVolum(audioSystem.curVolum/2);
                // bgm.Pause();//bgm��ͣ
                gameUI.SetActive(false);
               
            }
            
        }
        else// �˵���
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                menuList.SetActive(false);
                menukey = true;
                Time.timeScale = (1);//ʱ��ָ�����
                //audioSystem.SetVolum(audioSystem.curVolum * 2);
                gameUI.SetActive(true);
            }
            audioSystem.SetVolum((float)(volumeSlider.value));
        }

    }
    public void Return()//������Ϸ
    {
        //������Ч
        audioSystem.PlaySFX(audioSystem.selectMenu);

        menuList.SetActive(false);
        menukey = true;
        Time.timeScale = (1);//ʱ��ָ�����
        bgm.Play();//bgm����
        gameUI.SetActive(true);
    }
    public void Restart()//�ؿ�
    {
        //������Ч
        audioSystem.PlaySFX(audioSystem.selectMenu);

        SceneManager.LoadScene(0);
        Time.timeScale = 1;
        gameUI.SetActive(true);
    }
    public void Quit()//�˳���Ϸ
    {
        //������Ч
        audioSystem.PlaySFX(audioSystem.selectMenu);

        Application.Quit();
    }
}
