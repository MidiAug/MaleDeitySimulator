using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // ���
    private Animator ani;
    private Rigidbody2D rbody;
    private SpriteRenderer sRenderer;
    private GameObject playerUI;
    private Slider expSlider;
    private Text levelText;
    private Slider hpSlider;

    //�����������
    private float moveSpeed = 5f;
    private float maxHp = 100;
    private float curHp;
    private float maxExp = 100;
    private float curExp = 0;
    public float curLevel =1;

    public bool die = false;
    private bool isInvincible;//�ж��Ƿ��޵У��������ƽ�ɫ��Ѫ�������ü������

    private int numBlink = 2;
    private float blinkTime = 0.2f;
    private float dieTime = 2f;// ����������ʧ

    public int coinNum;// ��ʱ�䵱����

    void Start()
    {
        // ��ȡ�������ʼ����ɫѪ��
        ani = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody2D>();
        sRenderer = GetComponent<SpriteRenderer>();

        playerUI = GameObject.Find("PlayerUI");
        hpSlider = playerUI.transform.GetChild(0).gameObject.GetComponent<Slider>();
        expSlider = playerUI.transform.GetChild(1).gameObject.GetComponent<Slider>();
        levelText = playerUI.transform.GetChild(2).gameObject.GetComponent<Text>();
        curHp = maxHp;
        curLevel = 1;
    }

    void Update()
    {
        if (!die)
        {
            Move();
            // ��������
            hpSlider.value = curHp / maxHp;
            expSlider.value = curExp / maxExp;
        }
    }

    // ��ɫ���ƶ�����
    void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        ani.SetFloat("Horizontal", horizontal);
        ani.SetFloat("Vertical", vertical);

        bool playerMove = horizontal != 0f || vertical != 0f;
        ani.SetBool("Run", playerMove);

        bool playerIdle = horizontal < Mathf.Epsilon && vertical < Mathf.Epsilon;
        ani.SetBool("Idle", playerIdle);

        Vector2 dir = new Vector2(horizontal, vertical);
        if (horizontal * vertical != 0) rbody.velocity = dir * moveSpeed * (float)System.Math.Sqrt(0.5f);//б�����ٶ�*����2
        else rbody.velocity = dir * moveSpeed;
    }

    // �ܵ�����
    public void Attacked(float damage)
    {
        if (!isInvincible)
        {
            isInvincible = true;
            curHp -= damage;
            if (curHp < Mathf.Epsilon)
            {
                ani.SetTrigger("Die");
                for (int i = 0; i < transform.childCount; i++)
                {
                    Destroy(transform.GetChild(i).gameObject);
                }
                die = true;
                rbody.velocity = Vector2.zero;

                //��һ��ʱ���������Ҷ������ʱ�����ڲ�����ҵ���������
                Invoke("KillPlayer", dieTime);
            }
            if (!die)
            {
                StartCoroutine(BlinkPlayer(numBlink, blinkTime));
            }
        }
    }

    //�����������ֵ
    public void InHealth(float val)
    {
        if(val<=0)
        {
            Debug.Log("����������С��0");
        }
        curHp = Mathf.Clamp(curHp + val, 0 , maxHp);
    }

    // �����������
    private void KillPlayer()
    {
        Destroy(gameObject);
    }

    // �����յ���������
    // Э��(IEnumerator)���ڲ��������ܵ�����ʱ����˸����
    IEnumerator BlinkPlayer(int num, float time)
    {
        // ѭ��ִ����˸�Ĵ�����ÿ��ѭ���л���ʾ�����أ�
        for (int i = 0; i < num * 2; i++)
        {
            // �л��������Ⱦ״̬��ʵ����ʾ�����ص�Ч��
            sRenderer.enabled = !sRenderer.enabled;

            // �ȴ�ָ����ʱ����
            yield return new WaitForSeconds(time);
        }

        // ѭ�������󣬽��������Ⱦ״̬����Ϊ��ʾ
        sRenderer.enabled = true;

        // ��ʾ��ɫ���Խ�����һ���˺�
        isInvincible = false;
    }

    // ��þ���
    public void InExp(float newExp)
    {
        curExp += newExp;
        if(curExp>=maxExp)
        {
            InLevel();
        }
    }

    // �ȼ�����
    private void InLevel()
    {
        maxHp += 10;
        curHp = maxHp;

        curLevel++;
        curExp = 0;
        levelText.text = "lv." + curLevel.ToString();

        maxExp += 60; // ����ֵ����
    }

    IEnumerator InlevelAni(int time)
    {
        yield return new WaitForSeconds(time);
    }
}
