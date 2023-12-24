using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{    
    public static PlayerController Instance { get; set; }
    // 组件
    private Animator ani;
    private Rigidbody2D rbody;
    private SpriteRenderer sRenderer;
    private GameObject playerUI;
    private Slider expSlider;
    private Text levelText;
    private Slider hpSlider;

    //人物相关属性
    private float moveSpeed = 5f;
    public static float maxHp = 100;
    public static float curHp;
    private float maxExp = 100;
    private float curExp = 0;
    public float curLevel =1;
    private Audio playerAudio;//玩家音效，目前包内只有射击、胜利、战败、升级的音效

    public bool die = false;
    private bool isInvincible;//判断是否无敌，用于限制角色掉血方法调用间隔过短

    private int numBlink = 2;
    private float blinkTime = 0.2f;
    private float dieTime = 2f;// 死亡后多久消失

    public int coinNum;// 暂时充当背包

    //加载人物死亡界面
    public GameObject deadmenu;
    void Start()
    {
        // 获取组件，初始化角色血量
        ani = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody2D>();
        sRenderer = GetComponent<SpriteRenderer>();
        playerAudio = GameObject.FindGameObjectWithTag("Audio").GetComponent<Audio>();

        playerUI = GameObject.Find("PlayerUI");
        hpSlider = playerUI.transform.GetChild(0).gameObject.GetComponent<Slider>();
        expSlider = playerUI.transform.GetChild(1).gameObject.GetComponent<Slider>();
        levelText = playerUI.transform.GetChild(2).gameObject.GetComponent<Text>();
        curHp = maxHp;
        curLevel = 1;
        coinNum = 0;
        //初始不要加载死亡界面
        deadmenu.SetActive(false);
    }

    void Update()
    {
        if (!die)
        {
            Move();
            // 更新条条
            hpSlider.value = curHp / maxHp;
            expSlider.value = curExp / maxExp;
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
                //失败音效
                playerAudio.PlaySFX(playerAudio.lost);
                deadmenu.SetActive(true);
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
                StartCoroutine(BlinkPlayer(numBlink, blinkTime));
            }
        }
    }

    //增加玩家生命值
    public void InHealth(float val)
    {
        if(val<=0)
        {
            Debug.Log("错误，增加量小于0");
        }
        curHp = Mathf.Clamp(curHp + val, 0 , maxHp);
    }
    // 销毁人物对象
    private void KillPlayer()
    {
        Destroy(gameObject);
    }

    // 人物收到攻击动画
    // 协程(IEnumerator)用于播放人物受到攻击时的闪烁动画
    IEnumerator BlinkPlayer(int num, float time)
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

    // 获得经验
    public void InExp(float newExp)
    {
        curExp += newExp;
        if(curExp>=maxExp)
        {
            InLevel();
        }
    }

    // 等级提升
    private void InLevel()
    {
        maxHp += 10;
        curHp = maxHp;

        curLevel++;
        curExp = 0;
        levelText.text = "lv." + curLevel.ToString();

        //播放升级音效
        playerAudio.PlaySFX(playerAudio.levelUp);

        maxExp += 60; // 具体值待定
    }

    public IEnumerator InlevelAni(int time)
    {
        yield return new WaitForSeconds(time);
    }
}
