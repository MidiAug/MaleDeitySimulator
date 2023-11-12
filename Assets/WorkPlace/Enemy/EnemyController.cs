using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // 数据
    public EnemyData enemyData;

    // 组件
    Animator animator;
    Rigidbody2D rigidbody2;
    BoxCollider2D boxCollider2;
   

    // 相关变量
    public float curHp;
    public float desTime;

    private bool die = false;
    // Start is called before the first frame update
    void Start()
    {
        curHp = enemyData.maxHp;
        animator = GetComponent<Animator>();
        rigidbody2 = GetComponent<Rigidbody2D>();
        boxCollider2 = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!die)
        {
            rigidbody2.velocity = Vector2.zero;
        }
    }
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

    public void Attacked(float damage)
    {
        curHp -= damage;
        if (curHp < Mathf.Epsilon) 
        {
            animator.SetTrigger("Die");
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
            Invoke("DestorySelf", 1f);
            Destroy(gameObject.GetComponent<BoxCollider2D>());
            Destroy(gameObject.GetComponent<CapsuleCollider2D>());
        }
    }
    void DestorySelf()
    {
        Destroy(gameObject);
    }
}
