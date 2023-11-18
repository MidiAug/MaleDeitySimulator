using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 控制子弹的行为，主要包含击中目标的判断
/// </summary>
public class BulletController : MonoBehaviour
{
    GameObject curWeapon;    // 当前武器的游戏对象
    private float damage;    // 子弹伤害值
    public float fadeTime = 0;    // 子弹存在时间

    //属性初始化
    private void Start()
    {
        curWeapon = GameObject.FindGameObjectWithTag("Weapon").transform.GetChild(0).gameObject;        // 获取当前武器的游戏对象
        damage = curWeapon.GetComponent<VisitWeapon>().weaponData.damage;                               // 获取当前武器的伤害值
    }

    // 子弹击中目标
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 击中墙壁
        if (collision.gameObject.tag == "Build")
        {
            Destroy(gameObject, fadeTime);
        }

        // 击中敌人
        if (collision.gameObject.tag == "Enemy")
        {
            EnemyController enemyController = collision.gameObject.GetComponent<EnemyController>();
            // 获取碰撞物体上的 EnemyController 组件，并调用其 Attacked 方法，传递伤害值
            if (enemyController.die ==  false)
            {
                enemyController.Attacked(damage);
                Destroy(gameObject, fadeTime);
            }
        }

        //击中箱子
        if (collision.gameObject.tag == "box")
        {
            collision.GetComponent<Box>().GetCoin();
        }
    }
}
