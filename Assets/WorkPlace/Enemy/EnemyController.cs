using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // 引用数据
    public EnemyData enemyData;

    // 组件
    Animator animator;
    Rigidbody2D rigidbody2;
    BoxCollider2D boxCollider2;
    SpriteRenderer spriteRenderer2;

    GameObject player;

    // 相关变量
    public float curHp;
    public float desTime;
    private bool die = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2 = GetComponent<Rigidbody2D>();
        boxCollider2 = GetComponent<BoxCollider2D>();
        spriteRenderer2 = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");

        // 数据初始化
        curHp = enemyData.maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        if (!die&&!player.GetComponent<PlayerController>().die)
        {
            Move();
            rigidbody2.velocity = Vector2.zero;
        }
        else
        {
            animator.SetBool("Idle", true);
            animator.SetBool("Run", false);
        }
    }

    // 碰撞伤害
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!die)
        {
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
        if (curHp < Mathf.Epsilon) 
        {
            animator.SetTrigger("Die");
            die = true;
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
            Invoke("DestorySelf", 1f);
            Destroy(gameObject.GetComponent<BoxCollider2D>());
            Destroy(gameObject.GetComponent<CapsuleCollider2D>());
        }
    }

    // 死亡消除对象
    void DestorySelf()
    {
        Destroy(gameObject);
    }

    // 追踪玩家
    void Move()
    {
        if(player!= null)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemyData.moveSpeed * Time.deltaTime);
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

    // 方向掉转
    void Flip(bool isOrNot)
    {
        spriteRenderer2.flipX = isOrNot;
    }
}
