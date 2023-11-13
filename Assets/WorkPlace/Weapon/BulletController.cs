using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����ӵ�����Ϊ����Ҫ��������Ŀ����ж�
/// </summary>
public class BulletController : MonoBehaviour
{
    GameObject curWeapon;    // ��ǰ��������Ϸ����
    private float damage;    // �ӵ��˺�ֵ
    public float fadeTime = 0;    // �ӵ�����ʱ��

    //���Գ�ʼ��
    private void Start()
    {
        curWeapon = GameObject.FindGameObjectWithTag("Weapon").transform.GetChild(0).gameObject;        // ��ȡ��ǰ��������Ϸ����
        damage = curWeapon.GetComponent<VisitWeapon>().weaponData.damage;                               // ��ȡ��ǰ�������˺�ֵ
    }

    // �ӵ�����Ŀ��
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ����ǽ��
        if (collision.gameObject.tag == "Build")
        {
            Destroy(gameObject, fadeTime);
        }

        // ���е���
        if (collision.gameObject.tag == "Enemy")
        {
            // ��ȡ��ײ�����ϵ� EnemyController ������������� Attacked �����������˺�ֵ
            collision.gameObject.GetComponent<EnemyController>().Attacked(damage);
            Destroy(gameObject, fadeTime);
        }
    }
}
