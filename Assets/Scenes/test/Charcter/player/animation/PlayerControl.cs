
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    
    private Animator ani;
    private Rigidbody2D rbody;
    private Transform trans;
    private SpriteRenderer sRenderer;

    public float moveSpeed = 5f;
    public float hp = 100;
    public int NumBlink;
    public float BlinkTime;
    public float dieTime;
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody2D>();
        trans = GetComponent<Transform>();
        sRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        //Flip();用枪口方向确定人的方向
        Attack();
    }
    void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        ani.SetFloat("Horizontal", horizontal);
        ani.SetFloat("Vertical", vertical);

        bool playerMove = horizontal != 0f || vertical != 0f;
        ani.SetBool("Run", playerMove);

        bool playerIdle = horizontal < Mathf.Epsilon && vertical < Mathf.Epsilon;
        ani.SetBool("Idle", playerIdle);

        Vector2 dir = new Vector2(horizontal, vertical);
        if (horizontal * vertical != 0) rbody.velocity = dir * 5f * (float)System.Math.Sqrt(0.5f);//斜着走速度*根号2
        else rbody.velocity = dir * 5f;
    }
    void Flip()
    {
        bool playerDirRight = rbody.velocity.magnitude > Mathf.Epsilon;
        if (playerDirRight)
        {
            if(rbody.velocity.x > Mathf.Epsilon)
            {
                transform.localRotation = Quaternion.Euler(0,0,0);
            }
            if (rbody.velocity.x < -Mathf.Epsilon)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }
    void Attack() 
    {
        if (Input.GetButton("Attack"))
        {
            ani.SetTrigger("Attack");
            
        }
    }
    public void Attacked(float damage)
    {
        //WarriorEnemyController enemy=GetComponent<WarriorEnemyController>();
        //if (enemy != null) { return; }
        //characterData.hp = characterData.hp > 0 ? characterData.hp - enemy.harsh_ : characterData.hp;

        hp -= damage;
        if (hp < Mathf.Epsilon)
        {
            ani.SetTrigger("Die");
            for (int i = 0; i < transform.childCount; i++)
            {
                Debug.Log(transform.GetChild(i).name);
                Destroy(transform.GetChild(i));
            }
            
            Invoke("KillPlayer", dieTime);
        }
        BlinkPlayer(NumBlink, BlinkTime);
    }

    private void KillPlayer()
    {
        Destroy(gameObject);
    }
    private void BlinkPlayer(int num,float time)
    {
        StartCoroutine(DoBlinkPlayer(num,time));
    }
    IEnumerator DoBlinkPlayer(int num, float time)
    {
        for (int i = 0; i < num*2; i++)
        {
            sRenderer.enabled = !sRenderer.enabled;
            yield return new WaitForSeconds(time);
        }
        sRenderer.enabled = true;
    }
}
