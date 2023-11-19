using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // ���
    private Animator ani;
    private Rigidbody2D rbody;
    private Transform trans;
    private SpriteRenderer sRenderer;

    //�����������
    public float moveSpeed = 5f;
    public float maxHp = 100;
    public float curHp;
    public bool die = false;
    private bool isInvincible;//�ж��Ƿ��޵У��������ƽ�ɫ��Ѫ�������ü������

    public float MyMaxHealth { get { return maxHp; } }
    public float MyCurrentHealth { get { return curHp; } }
    public int numBlink;
    public float blinkTime;
    public float dieTime;

    void Start()
    {
        // ��ȡ�������ʼ����ɫѪ��
        ani = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody2D>();
        trans = GetComponent<Transform>();
        sRenderer = GetComponent<SpriteRenderer>();

        curHp = maxHp;
    }

    void Update()
    {
        if (!die)
        {
            Move();
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
                BlinkPlayer(numBlink, blinkTime);
            }
        }
    }

    //�����������ֵ
    public void InHealth(float val)
    {
        curHp = Mathf.Clamp(curHp + val, 0 , maxHp);
    }

    // �����������
    private void KillPlayer()
    {
        Destroy(gameObject);
    }

    // �����յ���������
    private void BlinkPlayer(int num, float time)
    {
        // ����Э�̣�������˸����
        StartCoroutine(DoBlinkPlayer(num, time));
    }

    // Э��(IEnumerator)���ڲ��������ܵ�����ʱ����˸����
    IEnumerator DoBlinkPlayer(int num, float time)
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
}
