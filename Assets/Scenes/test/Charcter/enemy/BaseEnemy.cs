using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���˿��ƽű�
/// </summary>

public class BaseEnemy : MonoBehaviour
{
    /// <summary>
    /// ���˻�������
    /// </summary>
    public float health =100f;
    public float speed_ = 5f;//�����ƶ��ٶ�
    private Rigidbody2D enemy_body_;//���˵ĸ���
    public bool is_vertical;//�ж��Ƿ�ֱ�ƶ�
    private Vector2 move_direction_;//�ƶ�����
    public float harsh_ = 1f;
    void Start()
    {
        enemy_body_ = GetComponent<Rigidbody2D>();
        move_direction_ = is_vertical ? Vector2.up : Vector2.right;
    }

    /// <summary>
    /// �����ƶ�����
    /// </summary>
    void EnemyMove()
    {
        Vector2 position = enemy_body_.position;
        position.x += move_direction_.x * speed_ * Time.deltaTime;
        position.y += move_direction_.y * speed_ * Time.deltaTime;
        enemy_body_.MovePosition(position);
    }

    /// <summary>
    /// ˢ�µ���״̬
    /// </summary>
    void Update()
    {
        //EnemyMove();
        if(health <Mathf.Epsilon)
            Destroy(this.gameObject);
        enemy_body_.velocity = Vector2.zero;
}
public virtual void Attack() { }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            collision.gameObject.GetComponent<PlayerControl>().Attacked(10);
            print("1");
        }
    }
}
