using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//访问武器
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
        if(Input.GetMouseButton(0))//鼠标左键发射
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
    //射击方法
    private void FireFuntion()
    {
        timer += Time.deltaTime;
        if (timer >= weaponData.FireTimer)
        {
            isFire = true;
            timer = 0;
        }
        
    }
    //发射子弹
    private void FireBullet()
    {
        Quaternion rotarionOffset = Quaternion.AngleAxis(270, Vector3.forward);
        Vector3 bulletPosOffset = new Vector3(0f,0.05f,0f);
        GameObject newBullet = Instantiate(bullet, bulletPos.position, this.gameObject.transform.rotation * rotarionOffset);//子弹图片向上

        rb = newBullet.gameObject.GetComponent<Rigidbody2D>();
        gunForce = 10f;
        rb.AddForce(bulletPos.right* gunForce,ForceMode2D.Impulse);
    }
    // Start is called before the first frame update
}
