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
    private GameObject crystal;//����ˮ��

    private Audio enemyAudio;//���˵���Ч��� Ŀǰ����ֻ���ܻ�����������Ч

    // ��ر�������ǰ����ֵ����ɫ��������ʧʱ�䣬����״̬
    public float curHp;
    public float desTime;
    public bool die = false;
    private float shootInterval = 2f;
    private float shootTimer = 0f;
    private int blinkNum =1 ;
    private float blinkTime = 0.2f;
    bool isBlink;// ����Ƶ���յ��˺�ʱ���Э����˸ͬʱ����

    private PropList coinList;
    private float conProb = 5;
    private float boxProb = 3;
    private float nonProb = 1;
    /// <summary>
    /// EnemyController����ط���
    /// </summary>
    private void Awake()
    {
        coinList = (PropList)Resources.Load("coinList");
    }
    private void Start()
    {
        //��ȡ���
        animator = GetComponent<Animator>();
        rigidbody2 = GetComponent<Rigidbody2D>();
        boxCollider2 = GetComponent<BoxCollider2D>();
        spriteRenderer2 = GetComponent<SpriteRenderer>();

        enemyAudio = GameObject.FindGameObjectWithTag("Audio").GetComponent<Audio>();
        enemySystem = GameObject.Find("EnemySystem");        
        player = GameObject.FindGameObjectWithTag("Player");
        crystal = GameObject.FindGameObjectWithTag("Crystal");

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
            animator.SetBool("Idle", true);
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
      if (enemyData.Name != "hyr" && player != null)
        //��ȡһ��ָ����ҵ�����
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemyData.moveSpeed * Time.deltaTime);

      else if (enemyData.Name == "hyr" && crystal != null)
        transform.position = Vector2.MoveTowards(transform.position, crystal.transform.position, enemyData.moveSpeed * Time.deltaTime);
      
      // �����������˵�x�����꣬��תͼ��
      if (transform.position.x > player.transform.position.x)
        Flip(true);

      else if (transform.position.x < player.transform.position.x)
        Flip(false);

      animator.SetBool("Run", true);

    }

  // ��ײ�˺�
  private void OnTriggerStay2D(Collider2D collision)
    {
      if (!die)
      {
        if (collision.gameObject.CompareTag("Player"))
        {
          PlayerController playerController = collision.GetComponent<PlayerController>();
          if (playerController != null)
          {
            playerController.Attacked(10f);
          }
        }
        else if (collision.gameObject.CompareTag("Crystal"))
        {
          CrystallController crystalController = collision.gameObject.GetComponent<CrystallController>();
          if (crystalController != null)
          {
            crystalController.Attacked(10f);
          }
        }
      }
  }

  // �ܵ�����
  public void Attacked(float damage)
    {
        curHp -= damage;
        //�����ܻ���Ч
        enemyAudio.PlaySFX(enemyAudio.getShot);

        //�������ж�С��0����������
        if (curHp < Mathf.Epsilon) 
        {
            enemyAudio.PlaySFX(enemyAudio.enemyDead);
            spriteRenderer2.color = Color.white;
            StopCoroutine("AttackedAni");
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
            GenerateProp();

            // ����
            player.GetComponent<PlayerController>().InExp(20);
        }
        else
        {
            if(!isBlink) StartCoroutine("AttackedAni");
        }
    }

    // �ܹ�����ɫ��˸
    IEnumerator AttackedAni()
    {
        isBlink = true;
        // ѭ��ִ����˸�Ĵ�����ÿ��ѭ���л���ʾ�����أ�
        for (int i = 0; i < blinkNum; i++)
        {
            // �л��������Ⱦ״̬��ʵ����ʾ�����ص�Ч��
            spriteRenderer2.color = Color.red;
            // �ȴ�ָ����ʱ����
            yield return new WaitForSeconds(blinkTime);
            spriteRenderer2.color = Color.white;
            yield return new WaitForSeconds(blinkTime);
        }

        // ѭ��������ȷ������������ʾ
        spriteRenderer2.color = Color.white;
        isBlink = false;
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

    // ���ɵ��� ��д�ıȽ���������ν��
    void GenerateProp()
    {
        //Instantiate(obj[Random.Range(0, obj.Length)], pos1, Quaternion.identity);
        float totalWeight = conProb + boxProb + nonProb;

        float randWeight = Random.value * totalWeight;

        randWeight -= conProb;
        if(randWeight < 0f)
        {
            float atotalWeight = 0f;
            foreach (PropData tmp in coinList.list)
            {
                atotalWeight += tmp.weight;
            }

            float arandomValue = Random.value * atotalWeight;
            foreach (PropData tmp in coinList.list)
            {
                arandomValue -= tmp.weight;
                if (arandomValue <= 0f)
                {
                    Instantiate(tmp.prefab, transform.position, Quaternion.identity);
                    break;
                }
            }
        }
        else
        {
            randWeight -= boxProb;
            if (randWeight < 0f)
            {
                Instantiate(enemyData.boxPrefab, transform.position, Quaternion.identity);
            }
        }

        
    }

}
