using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    /// <summary>
    /// EnemyController�ĳ�Ա����
    /// </summary>
    // ����EnemyData
    public EnemyData enemyData;

    // �����ǰ��Ϊ���˱�������������Ϊ��ң�����׷�����λ��
    private Animator animator;
    private Rigidbody2D rigidbody2;
    private BoxCollider2D boxCollider2;
    private SpriteRenderer spriteRenderer2;

    public GameObject arrowPrefab; // ��ʸԤ����
    private Transform firePoint; // ����㣨��ʸ����λ�ã�

    private GameObject enemySystem;
    private GameObject player;

    // ��ر�������ǰ����ֵ����ɫ��������ʧʱ�䣬����״̬
    public float curHp;
    public float desTime;
    public bool die = false;
    private float shootInterval = 2f;
    private float shootTimer = 0f;

    /// <summary>
    /// EnemyController����ط���
    /// </summary>
    private void Start()
    {
        //��ȡ���
        animator = GetComponent<Animator>();
        rigidbody2 = GetComponent<Rigidbody2D>();
        boxCollider2 = GetComponent<BoxCollider2D>();
        spriteRenderer2 = GetComponent<SpriteRenderer>();

        enemySystem = GameObject.Find("EnemySystem");        
        player = GameObject.FindGameObjectWithTag("Player");

        firePoint = transform;

        // ���ݳ�ʼ��
        curHp = enemyData.maxHp;
    }

    private void Update()
    {
        shootTimer += Time.deltaTime;
        //����û���������û��������л
        if (!die&&player!=null&&!player.GetComponent<PlayerController>().die)
        {
            Move();
            rigidbody2.velocity = Vector2.zero;// ���������������ײ�������ƶ����������ٶȵ�

            //Զ�̵���Ҫ���
            if (enemyData.Name == "lcq"&&shootTimer>=shootInterval) Shoot();
        }
        else
        {
            //animator.SetBool("Idle", true);
            animator.SetBool("Run", false);
        }
    }

    private void Shoot()
    {
        // ������ʸ�����÷���
        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, new Quaternion(0, 0, 0, 1), enemySystem.transform.GetChild(1));
        shootTimer = 0;
    }

    // ׷�����
    private void Move()
    {
        if (player != null)
        {
            //��ȡһ��ָ����ҵ�����
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemyData.moveSpeed * Time.deltaTime);
            
            // �����������˵�x�����꣬��תͼ��
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

    // ��ײ�˺�
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!die)
        {
            //�����ײ��������ң������Attacked�ܵ���Ӧ���˺�
            //TODO:�������Ĭ���ܵ��˺�Ϊ10������Ӧ����Ҫ��ȡ��ҵ�װ��ϵͳ�Ի�ȡ��ȷ���˺�ֵ
            if (collision.gameObject.tag == "Player"&&collision.GetType().ToString()== "UnityEngine.CapsuleCollider2D")
            {
                collision.GetComponent<PlayerController>().Attacked(10f);
            }
        }
    }

    // �ܵ�����
    public void Attacked(float damage)
    {
        curHp -= damage;
        //�������ж�С��0����������
        if (curHp < Mathf.Epsilon) 
        {
            animator.SetTrigger("Die");
            die = true;
            for (int i = 0; i < transform.childCount; i++)
            {
                //�������������岢����
                Destroy(transform.GetChild(i).gameObject);
            }
            // ������������һ����ʱ��
            Invoke("DestorySelf", 1f);

            // ʹʬ���޷�����ҽ���
            Destroy(gameObject.GetComponent<BoxCollider2D>());
            Destroy(gameObject.GetComponent<CapsuleCollider2D>());

            // ���ɱ���
            BoxGenerate();
        }
    }

    // ������������
    private void DestorySelf()
    {
        Destroy(gameObject);
    }

    // �����ת
    private void Flip(bool isOrNot)
    {
        spriteRenderer2.flipX = isOrNot;
    }

    void BoxGenerate()
    {
        //Instantiate(obj[Random.Range(0, obj.Length)], pos1, Quaternion.identity);
        if(Random.value < enemyData.boxPossibility)
        {
            Instantiate(enemyData.boxPrefab, transform.position, Quaternion.identity);
        }
    }

}
