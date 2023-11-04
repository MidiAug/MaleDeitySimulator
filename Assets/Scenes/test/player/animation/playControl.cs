
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class playControl : MonoBehaviour
{
    private Animator ani;
    private Rigidbody2D rbody;
    private Transform trans;
    private SpriteRenderer sRenderer;
    public float speedCoef;

    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody2D>();
        trans = GetComponent<Transform>();
        sRenderer = GetComponent<SpriteRenderer>();

        speedCoef = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Flip();
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
        if (horizontal * vertical != 0) rbody.velocity = dir * speedCoef * (float)System.Math.Sqrt(0.5f);//斜着走速度*根号2
        else rbody.velocity = dir * speedCoef;
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
}
