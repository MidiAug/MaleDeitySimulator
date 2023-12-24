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

    // 组件，前者为敌人本身的组件，后者为玩家，便于追踪玩家位置
    private Animator animator;
    private Rigidbody2D rigidbody2;
    private BoxCollider2D boxCollider2;
    private SpriteRenderer spriteRenderer2;

    public GameObject arrowPrefab; // 箭矢预制体
    private Transform firePoint; // 发射点（箭矢生成位置）

    private GameObject enemySystem;
    private GameObject player;
    private GameObject crystal;//坤门水晶

    private Audio enemyAudio;//敌人的音效插件 目前包内只有受击、死亡的音效

    // 相关变量，当前生命值，角色死亡后消失时间，生存状态
    public float curHp;
    public float desTime;
    public bool die = false;
    private float shootInterval = 2f;
    private float shootTimer = 0f;
    private int blinkNum =1 ;
    private float blinkTime = 0.2f;
    bool isBlink;// 避免频繁收到伤害时多次协程闪烁同时开启

    private PropList coinList;
    private float conProb = 5;
    private float boxProb = 3;
    private float nonProb = 1;
    /// <summary>
    /// EnemyController的相关方法
    /// </summary>
    private void Awake()
    {
        coinList = (PropList)Resources.Load("coinList");
    }
    private void Start()
    {
        //获取组件
        animator = GetComponent<Animator>();
        rigidbody2 = GetComponent<Rigidbody2D>();
        boxCollider2 = GetComponent<BoxCollider2D>();
        spriteRenderer2 = GetComponent<SpriteRenderer>();

        enemyAudio = GameObject.FindGameObjectWithTag("Audio").GetComponent<Audio>();
        enemySystem = GameObject.Find("EnemySystem");        
        player = GameObject.FindGameObjectWithTag("Player");
        crystal = GameObject.FindGameObjectWithTag("Crystal");

        firePoint = transform;

        // 数据初始化
        curHp = enemyData.maxHp;
    }

    private void Update()
    {
        shootTimer += Time.deltaTime;
        //敌人没死并且玩家没死，则进行活动
        if (!die&&player!=null&&!player.GetComponent<PlayerController>().die)
        {
            Move();
            rigidbody2.velocity = Vector2.zero;// 避免人物与敌人碰撞，敌人移动方法是无速度的

            //远程敌人要射击
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
        // 创建箭矢并设置方向
        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, new Quaternion(0, 0, 0, 1), enemySystem.transform.GetChild(1));
        shootTimer = 0;
    }

    // 追踪玩家
    private void Move()
    {
      if (enemyData.Name != "hyr" && player != null)
        //获取一个指向玩家的向量
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemyData.moveSpeed * Time.deltaTime);

      else if (enemyData.Name == "hyr" && crystal != null)
        transform.position = Vector2.MoveTowards(transform.position, crystal.transform.position, enemyData.moveSpeed * Time.deltaTime);
      
      // 根据玩家与敌人的x轴坐标，翻转图像
      if (transform.position.x > player.transform.position.x)
        Flip(true);

      else if (transform.position.x < player.transform.position.x)
        Flip(false);

      animator.SetBool("Run", true);

    }

  // 碰撞伤害
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

  // 受到攻击
  public void Attacked(float damage)
    {
        curHp -= damage;
        //播放受击音效
        enemyAudio.PlaySFX(enemyAudio.getShot);

        //浮点数判断小于0，敌人死亡
        if (curHp < Mathf.Epsilon) 
        {
            enemyAudio.PlaySFX(enemyAudio.enemyDead);
            spriteRenderer2.color = Color.white;
            StopCoroutine("AttackedAni");
            animator.SetTrigger("Die");
            die = true;
            for (int i = 0; i < transform.childCount; i++)
            {
                //遍历所有子物体并销毁
                Destroy(transform.GetChild(i).gameObject);
            }
            // 保留怪物遗体一部分时间
            Invoke("DestorySelf", 1f);

            // 使尸体无法与玩家交互
            Destroy(gameObject.GetComponent<BoxCollider2D>());
            Destroy(gameObject.GetComponent<CapsuleCollider2D>());

            // 生成宝箱
            GenerateProp();

            // 经验
            player.GetComponent<PlayerController>().InExp(20);
        }
        else
        {
            if(!isBlink) StartCoroutine("AttackedAni");
        }
    }

    // 受攻击红色闪烁
    IEnumerator AttackedAni()
    {
        isBlink = true;
        // 循环执行闪烁的次数（每次循环切换显示和隐藏）
        for (int i = 0; i < blinkNum; i++)
        {
            // 切换人物的渲染状态，实现显示和隐藏的效果
            spriteRenderer2.color = Color.red;
            // 等待指定的时间间隔
            yield return new WaitForSeconds(blinkTime);
            spriteRenderer2.color = Color.white;
            yield return new WaitForSeconds(blinkTime);
        }

        // 循环结束后，确保敌人正常显示
        spriteRenderer2.color = Color.white;
        isBlink = false;
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

    // 生成道具 先写的比较拉，无所谓了
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
