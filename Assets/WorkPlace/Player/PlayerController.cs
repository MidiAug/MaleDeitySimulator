using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 组件
    private Animator ani;
    private Rigidbody2D rbody;
    private Transform trans;
    private SpriteRenderer sRenderer;

    //人物相关属性
    public float moveSpeed = 5f;
    public float maxHp = 100;
    public float curHp;
    public int NumBlink;
    public float BlinkTime;
    public float dieTime;

    private bool die = false;
    private bool isInvincible;
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody2D>();
        trans = GetComponent<Transform>();
        sRenderer = GetComponent<SpriteRenderer>();

        curHp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        if(!die)
        {
            Move();
        }
    }
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
        if (horizontal * vertical != 0) rbody.velocity = dir * 5f * (float)System.Math.Sqrt(0.5f);//斜着走速度*根号2
        else rbody.velocity = dir * 5f;
    }

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
                Invoke("KillPlayer", dieTime);
            }
            BlinkPlayer(NumBlink, BlinkTime);
        }
    }

    // 销毁人物对象
    private void KillPlayer()
    {
        Destroy(gameObject);
    }

    // 人物收到攻击动画
    private void BlinkPlayer(int num,float time)
    {
        StartCoroutine(DoBlinkPlayer(num,time));
    }
    IEnumerator DoBlinkPlayer(int num, float time)
    {
        for (int i = 0; i < num*2; i++)
        {
            sRenderer.enabled = !sRenderer.enabled;
            yield return new WaitForSeconds(time);
        }
        sRenderer.enabled = true;
        isInvincible = false;
    }
}
