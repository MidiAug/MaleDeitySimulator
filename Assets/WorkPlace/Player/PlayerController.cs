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
    public bool die = false;
    private bool isInvincible;//判断是否无敌，用于限制角色掉血方法调用间隔过短

    public float MyMaxHealth { get { return maxHp; } }
    public float MyCurrentHealth { get { return curHp; } }
    public int numBlink;
    public float blinkTime;
    public float dieTime;

    void Start()
    {
        // 获取组件，初始化角色血量
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

    // 角色的移动方法
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
        if (horizontal * vertical != 0) rbody.velocity = dir * moveSpeed * (float)System.Math.Sqrt(0.5f);//斜着走速度*根号2
        else rbody.velocity = dir * moveSpeed;
    }

    // 受到攻击
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

                //在一定时间后销毁玩家对象，这段时间用于播放玩家的死亡动画
                Invoke("KillPlayer", dieTime);
            }
            if (!die)
            {
                BlinkPlayer(numBlink, blinkTime);
            }
        }
    }

    //增加玩家生命值
    public void InHealth(float val)
    {
        curHp = Mathf.Clamp(curHp + val, 0 , maxHp);
    }

    // 销毁人物对象
    private void KillPlayer()
    {
        Destroy(gameObject);
    }

    // 人物收到攻击动画
    private void BlinkPlayer(int num, float time)
    {
        // 启动协程，播放闪烁动画
        StartCoroutine(DoBlinkPlayer(num, time));
    }

    // 协程(IEnumerator)用于播放人物受到攻击时的闪烁动画
    IEnumerator DoBlinkPlayer(int num, float time)
    {
        // 循环执行闪烁的次数（每次循环切换显示和隐藏）
        for (int i = 0; i < num * 2; i++)
        {
            // 切换人物的渲染状态，实现显示和隐藏的效果
            sRenderer.enabled = !sRenderer.enabled;

            // 等待指定的时间间隔
            yield return new WaitForSeconds(time);
        }

        // 循环结束后，将人物的渲染状态设置为显示
        sRenderer.enabled = true;

        // 表示角色可以接受下一次伤害
        isInvincible = false;
    }
}
