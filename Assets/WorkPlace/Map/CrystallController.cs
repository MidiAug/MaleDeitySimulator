using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CrystallController : MonoBehaviour
{
  //属性 水晶有一定血量 水晶被攻破则游戏结束 玩家可以在水晶处选择强化生命值或攻击力
  private const float maxHealth=150f;
  private float curHealth;
  private bool isInvincible = false;//判断是否无敌，用于限制角色掉血方法调用间隔过短
  private float isInvincibleTimer = 0f;
  private float isInvincibleInterval = 4f;

  // 组件
  private PlayerController playerController;
  private BoxCollider2D boxCollider2;
  private Slider healthBar;


  void Start()
  {
    playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    healthBar = GameObject.Find("CrystalUI").transform.GetChild(0).gameObject.GetComponent<Slider>();
    curHealth = maxHealth;
  }

  private void Update()
  {
    if (isInvincible)
    {
      isInvincibleTimer += Time.deltaTime;
      if (isInvincibleTimer >= isInvincibleInterval)
      {
        isInvincible = false;
        isInvincibleTimer = 0;
      }
    }
    healthBar.value= curHealth/maxHealth;
  }

  public void Attacked(float damage)
  {
    if (!isInvincible)
    {//水晶崩塌 玩家直接死亡
      if (curHealth - damage < Mathf.Epsilon)
        playerController.CrystalFallen();
      else
        curHealth -= damage;
      isInvincible = true;
    }
  }
}
