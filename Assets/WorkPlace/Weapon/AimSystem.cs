using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// 瞄准系统
/// </summary>
public class AimSystem : MonoBehaviour
{
    // 静态变量，用于保证只有一个 AimSystem 的实例
    public static AimSystem instance { get; private set; } //get说明外部可以获取本类的属性，private set说明属性只能被类内部设置

    public GameObject currentWeapon;            // 当前使用的武器
    Vector3 mouseScreenPos = Vector3.zero;      // 鼠标在屏幕上的位置
    Vector3 aimDir = Vector3.zero;              // 瞄准方向

    // 组件
    Camera cam;
    SpriteRenderer spriteRenderer;
    SpriteRenderer kuntou;

    //Awake在Start方法之前被调用，这段代码为了保证只有一个AimSystem实例
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }   //如若尝试创建第二个实例，则会被销毁
    }

    void Start()
    {
        cam = Camera.main;
        spriteRenderer = this.gameObject.transform.parent.GetComponent<SpriteRenderer>();   // 获取主摄像机和父对象的 SpriteRenderer 组件
        kuntou = transform.parent.Find("kuntou").GetComponent<SpriteRenderer>(); 

        currentWeapon = this.transform.GetChild(0).gameObject;    // 获取当前武器对象
    }

    void Update()
    {
        // 每帧更新瞄准
        Aim();
    }

    // 瞄准的具体实现
    void Aim()
    {
        // 获取鼠标在屏幕上的位置
        mouseScreenPos = Input.mousePosition;

        // 计算瞄准方向
        aimDir = mouseScreenPos - cam.WorldToScreenPoint(currentWeapon.transform.position);
        aimDir.z = 0;
        aimDir.Normalize();

        // 计算瞄准方向的角度并将武器旋转到对应的角度
        float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;
        currentWeapon.transform.eulerAngles = new Vector3(0, 0, angle);

        // 根据角度调整人物的朝向
        PlayerAim(angle);
    }

    // 根据角度调整人物的朝向
    void PlayerAim(float angle)
    {
        // 如果角度在 -90 到 90 之间，不翻转 SpriteRenderer；否则翻转
        if (angle < 90 && angle > -90)
        {
            spriteRenderer.flipX = false;
            kuntou.flipX = false;
            currentWeapon.GetComponent<SpriteRenderer>().flipY = false;
        }
        else
        {
            spriteRenderer.flipX = true;
            kuntou.flipX = true;
            currentWeapon.GetComponent<SpriteRenderer>().flipY = true;
        }
    }

    // 更新武器的方法
    public static void UpdateWeapon(WeaponData weaponData)
    {
        // 如果已经存在武器，则销毁当前武器，生成新的武器
        if (instance.gameObject.transform.childCount > 1)
        {
            instance.currentWeapon = Instantiate(weaponData.prefab, instance.transform);
        }
        else
        {
            // 如果不存在武器，直接生成新的武器
            Destroy(instance.currentWeapon);
            instance.currentWeapon = Instantiate(weaponData.prefab, instance.transform);    //Instantiate创建武器，在instance的位置用weaponDate预制体创建
        }
    }
}
