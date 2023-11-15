using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    /// <summary>
    /// EnemyController的成员变量
    /// </summary>
    // 引用EnemyData
    public EnemyData enemyData;

    // 组件，前四者为敌人本身的组件，后者为玩家，便于追踪玩家位置
    private Animator animator;
    private Rigidbody2D rigidbody2;
    private BoxCollider2D boxCollider2;
    private SpriteRenderer spriteRenderer2;

    private GameObject player;

    // 相关变量，当前生命值，角色死亡后消失时间，生存状态
    public float curHp;
    public float desTime;
    private bool die = false;

    /// <summary>
    /// EnemyController的相关方法
    /// </summary>
    private void Start()
    {
        //获取组件
        animator = GetComponent<Animator>();
        rigidbody2 = GetComponent<Rigidbody2D>();
        boxCollider2 = GetComponent<BoxCollider2D>();
        spriteRenderer2 = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");

        // 数据初始化
        curHp = enemyData.maxHp;
    }

    private void Update()
    {
        if (!die&&player!=null&&!player.GetComponent<PlayerController>().die)
        {
            Move();
            rigidbody2.velocity = Vector2.zero;
        }//敌人没死并且玩家没死，调用移动函数
        else
        {
            animator.SetBool("Idle", true);
            animator.SetBool("Run", false);
        }
    }

    // 追踪玩家
    private void Move()
    {
        if (player != null)
        {
            //获取一个指向玩家的向量
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemyData.moveSpeed * Time.deltaTime);
            
            // 根据玩家与敌人的x轴坐标，翻转图像
            if (transform.position.x > player.transform.position.x)
            {
                Flip(true);
            }
            else if (transform.position.x < player.transform.position.x)
            {
                Flip(false);
            }
            animator.SetBool("Run", true);
        }
    }

    // 碰撞伤害
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!die)
        {
            //如果碰撞物来自玩家，则调用Attacked受到相应的伤害
            //TODO:这里敌人默认受到伤害为10，后续应该需要获取玩家的装备系统以获取正确的伤害值
            if (collision.gameObject.tag == "Player"&&collision.GetType().ToString()== "UnityEngine.CapsuleCollider2D")
            {
                collision.GetComponent<PlayerController>().Attacked(10f);
            }
        }
    }

    // 受到攻击
    public void Attacked(float damage)
    {
        curHp -= damage;
        //浮点数判断小于0
        if (curHp < Mathf.Epsilon) 
        {
            animator.SetTrigger("Die");
            die = true;
            for (int i = 0; i < transform.childCount; i++)
            {
                //遍历所有子物体并销毁
                Destroy(transform.GetChild(i).gameObject);
            }
            //Invoke是延时调用方法，在一秒后调用DestorySelf方法(Destory拼错了)
            Invoke("DestorySelf", 1f);
            Destroy(gameObject.GetComponent<BoxCollider2D>());
            Destroy(gameObject.GetComponent<CapsuleCollider2D>());
        }
    }

    // 死亡消除对象
    private void DestorySelf()
    {
        Destroy(gameObject);
    }

    // 方向掉转
    private void Flip(bool isOrNot)
    {
        spriteRenderer2.flipX = isOrNot;
    }
}
