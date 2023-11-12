using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // ����
    public EnemyData enemyData;

    // ���
    Animator animator;
    Rigidbody2D rigidbody2;
    BoxCollider2D boxCollider2;
   

    // ��ر���
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!die)
        {
            if (collision.gameObject.tag == "Player")
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
            boxCollider2.isTrigger = true;
        }
    }
    void DestorySelf()
    {
        Destroy(gameObject);
    }
}
