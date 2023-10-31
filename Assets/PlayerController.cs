using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���ƽ�ɫ�ƶ���������������
/// </summary>
public class PlayerController : MonoBehaviour
{
    public float speed = 5f;//�ƶ��ٶ�

    private int maxHealth = 5;//�������ֵ

    private int currentHealth;//��ǰ����ֵ

    public int MyMaxHealth { get { return maxHealth; } }

    public int MycurrentHealth { get { return currentHealth; } }

    private float invincibleTime = 2f;//�޵�ʱ��2s

    private float invincibleTimer;//�޵м�ʱ��

    private bool isInvincible;//�Ƿ����޵�״̬

    // Start is called before the first frame update
    Rigidbody2D rbody;
    void Start()
    {
        currentHealth = 2;
        invincibleTimer = 0;
        rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");//����ˮƽ�ƶ����� A:-1 D:1 ����:0
        float moveY = Input.GetAxisRaw("Vertical");//���ƴ�ֱ�ƶ����� W:1  S:-1 ����:0

        Vector2 position = rbody.position;
        position.x += moveX * speed * Time.deltaTime;
        position.y += moveY * speed * Time.deltaTime;
        rbody.MovePosition(position);
        //�޵м�ʱ=====================================
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
            {
                isInvincible = false;
            }
        }
    }

    public void ChangeHealth(int amount)
    {//�������ܵ��˺�
        if (amount < 0)
        {
            if (isInvincible == true)
            {
                return;
            }
            isInvincible = true;
            invincibleTimer = invincibleTime;
        }
        Debug.Log(currentHealth + "/" + maxHealth);
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    }
}
