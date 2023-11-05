using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敌人控制脚本
/// </summary>

public class BaseEnemy : MonoBehaviour
{
    /// <summary>
    /// 敌人基础属性
    /// </summary>
    public float speed_ = 5f;//敌人移动速度
    private Rigidbody2D enemy_body_;//敌人的刚体
    public bool is_vertical;//判断是否垂直移动
    private Vector2 move_direction_;//移动方向
    public float harsh_ = 1f;
    void Start()
    {
        enemy_body_ = GetComponent<Rigidbody2D>();
        move_direction_ = is_vertical ? Vector2.up : Vector2.right;
    }

    /// <summary>
    /// 敌人移动函数
    /// </summary>
    void EnemyMove()
    {
        Vector2 position = enemy_body_.position;
        position.x += move_direction_.x * speed_ * Time.deltaTime;
        position.y += move_direction_.y * speed_ * Time.deltaTime;
        enemy_body_.MovePosition(position);
    }

    /// <summary>
    /// 刷新敌人状态
    /// </summary>
    void Update()
    {
        EnemyMove();
    }
    public virtual void Attack() { }
}
