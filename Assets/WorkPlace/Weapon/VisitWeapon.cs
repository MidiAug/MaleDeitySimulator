using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��������
public class VisitWeapon : MonoBehaviour
{
    public WeaponData weaponData; // ����������
    public GameObject bullet; // �ӵ���Ԥ����
    private Rigidbody2D bulletRigidBody; // �ӵ��ĸ���
    private Transform bulletPos; // �ӵ�����λ��

    private GameObject bulletObject;//ʵ�������ӵ�����WeaponSystem��Bullet�������巽�����

    // �������
    public float timer = 0; // ��ʱ�������ڿ���������
    public float gunForce; // ���������
    private bool isFire = false; // �Ƿ�������

    void Start()
    {
        bulletObject = GameObject.Find("Bullet");

        bulletPos = transform.GetChild(0); // ��ȡ�ӵ�����λ�ã��������������Ӷ����Ƿ��޸�
    }

    void Update()
    {
        if (Input.GetMouseButton(0)) // ����������
        {
            Fire(); // �������
        }
    }

    private void FixedUpdate()
    {
        if (isFire == true)
        {
            FireBullet(); // ���������ʵ�ʲ���
        }
        isFire = false; // ���������־��ȷ������һ֡���ظ����
    }

    // �������
    private void Fire()
    {
        timer += Time.deltaTime; // �ۼӼ�ʱ��
        if (timer >= weaponData.FireTimer) // �����ʱ���ﵽ������
        {
            isFire = true; // ���������־
            timer = 0; // ���ü�ʱ��
        }
    }

    // �����ӵ�
    private void FireBullet()
    {
        //��ŷ�����Ա�ʾ��ת
        Quaternion rotationOffset = Quaternion.AngleAxis(270, Vector3.forward); // �����ӵ���ת����ʹ������
        GameObject newBullet = Instantiate(bullet, bulletPos.position, this.gameObject.transform.rotation * rotationOffset,bulletObject.transform); // �����ӵ�

        bulletRigidBody = newBullet.gameObject.GetComponent<Rigidbody2D>(); // ��ȡ�ӵ��ĸ������
        gunForce = 10f; // ���÷����ӵ��������Ա�ʵ�����Ч��
        bulletRigidBody.AddForce(bulletPos.right * gunForce, ForceMode2D.Impulse); // ���ӵ��������ʵ�����Ч��
    }
}
