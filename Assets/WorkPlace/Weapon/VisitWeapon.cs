using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 访问武器
public class VisitWeapon : MonoBehaviour
{
    public WeaponData weaponData; // 武器的数据
    public GameObject bullet; // 子弹的预制体
    private Rigidbody2D bulletRigidBody; // 子弹的刚体
    private Transform bulletPos; // 子弹生成位置

    private GameObject bulletObject;//实例化后，子弹属于WeaponSystem下Bullet的子物体方便管理

    // 相关属性
    public float timer = 0; // 计时器，用于控制射击间隔
    public float gunForce; // 射击的力度
    private bool isFire = false; // 是否进行射击

    void Start()
    {
        bulletObject = GameObject.Find("Bullet");

        bulletPos = transform.GetChild(0); // 获取子弹生成位置，后期视武器下子对象是否修改
    }

    void Update()
    {
        if (Input.GetMouseButton(0)) // 鼠标左键发射
        {
            Fire(); // 触发射击
        }
    }

    private void FixedUpdate()
    {
        if (isFire == true)
        {
            FireBullet(); // 进行射击的实际操作
        }
        isFire = false; // 重置射击标志，确保在下一帧不重复射击
    }

    // 射击方法
    private void Fire()
    {
        timer += Time.deltaTime; // 累加计时器
        if (timer >= weaponData.FireTimer) // 如果计时器达到射击间隔
        {
            isFire = true; // 设置射击标志
            timer = 0; // 重置计时器
        }
    }

    // 发射子弹
    private void FireBullet()
    {
        //用欧拉数以表示旋转
        Quaternion rotationOffset = Quaternion.AngleAxis(270, Vector3.forward); // 调整子弹旋转方向，使其向上
        GameObject newBullet = Instantiate(bullet, bulletPos.position, this.gameObject.transform.rotation * rotationOffset,bulletObject.transform); // 生成子弹

        bulletRigidBody = newBullet.gameObject.GetComponent<Rigidbody2D>(); // 获取子弹的刚体组件
        gunForce = 10f; // 设置发射子弹的力，以便实现射击效果
        bulletRigidBody.AddForce(bulletPos.right * gunForce, ForceMode2D.Impulse); // 给子弹添加力，实现射击效果
    }
}
