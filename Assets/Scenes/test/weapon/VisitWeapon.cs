using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��������
public class VisitWeapon : MonoBehaviour
{
    public WeaponData weaponData;
    public float timer = 0;
    public GameObject bullet;
    private Rigidbody2D rb;
    private Transform bulletPos;
    public float gunForce;
    private bool isFire = false;
   
    void Start()
    {
        bulletPos = this.gameObject.transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))//����������
        {
            FireFuntion();
        }
    }

    private void FixedUpdate()
    {
        if (isFire==true)
        {
            FireBullet();
        }
        isFire = false;
    }
    //�������
    private void FireFuntion()
    {
        timer += Time.deltaTime;
        if (timer >= weaponData.FireTimer)
        {
            isFire = true;
            timer = 0;
        }
        
    }
    //�����ӵ�
    private void FireBullet()
    {
        Quaternion rotarionOffset = Quaternion.AngleAxis(270, Vector3.forward);
        Vector3 bulletPosOffset = new Vector3(0f,0.05f,0f);
        GameObject newBullet = Instantiate(bullet, bulletPos.position, this.gameObject.transform.rotation * rotarionOffset);//�ӵ�ͼƬ����

        rb = newBullet.gameObject.GetComponent<Rigidbody2D>();
        gunForce = 10f;
        rb.AddForce(bulletPos.right* gunForce,ForceMode2D.Impulse);
    }
    // Start is called before the first frame update
}
