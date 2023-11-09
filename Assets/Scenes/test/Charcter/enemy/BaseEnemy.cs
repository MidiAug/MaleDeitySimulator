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
    public float track_radius = 20000f;
    private Transform player_transform_;
    virtual public void Start()
    {
        enemy_body_ = GetComponent<Rigidbody2D>();
        move_direction_ = is_vertical ? Vector2.up : Vector2.right;
        //��ȡ��ҵ�λ��
        player_transform_ = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    /// <summary>
    /// �����ƶ�����
    /// </summary>
    virtual public void EnemyMove()
    {
        if(player_transform_ != null){
            float distance=(transform.position - player_transform_.position).sqrMagnitude;
            if (distance < track_radius){
                transform.position=Vector2.MoveTowards(transform.position,player_transform_.position,speed_*Time.deltaTime);
            }
        }
    }

    /// <summary>
    /// ˢ�µ���״̬
    /// </summary>
    virtual public void Update()
    {
        EnemyMove();
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
