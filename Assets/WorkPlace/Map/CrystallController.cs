using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CrystallController : MonoBehaviour
{
    public static CrystallController Instance { get; set; }
    //���� ˮ����һ��Ѫ�� ˮ������������Ϸ���� ��ҿ�����ˮ����ѡ��ǿ������ֵ�򹥻���
    public float maxHealth = 150f;
    public float curHealth;
    private bool isInvincible = false;//�ж��Ƿ��޵У��������ƽ�ɫ��Ѫ�������ü������
    private float isInvincibleTimer = 0f;
    private float isInvincibleInterval = 4f;

    // ���
    private PlayerController playerController;
    private BoxCollider2D boxCollider2;
    private Slider healthBar;

    private Slider healthBarInterval;

    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        healthBar = transform.Find("CrystalUI").GetChild(0).GetComponent<Slider>();
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

        healthBar.value = curHealth / maxHealth;
    }

    public void Attacked(float damage)
    {
        if (!isInvincible)
        {//ˮ������ ���ֱ������
            if (curHealth - damage < Mathf.Epsilon)
                playerController.CrystalFallen();
            else
                curHealth -= damage;
            isInvincible = true;
        }
    }
}