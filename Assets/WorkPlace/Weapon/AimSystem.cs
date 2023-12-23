using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// ��׼ϵͳ
/// </summary>
public class AimSystem : MonoBehaviour
{
    // ��̬���������ڱ�ֻ֤��һ�� AimSystem ��ʵ��
    public static AimSystem instance { get; private set; } //get˵���ⲿ���Ի�ȡ��������ԣ�private set˵������ֻ�ܱ����ڲ�����

    public GameObject currentWeapon;            // ��ǰʹ�õ�����
    Vector3 mouseScreenPos = Vector3.zero;      // �������Ļ�ϵ�λ��
    Vector3 aimDir = Vector3.zero;              // ��׼����

    // ���
    Camera cam;
    SpriteRenderer spriteRenderer;
    SpriteRenderer kuntou;

    //Awake��Start����֮ǰ�����ã���δ���Ϊ�˱�ֻ֤��һ��AimSystemʵ��
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }   //�������Դ����ڶ���ʵ������ᱻ����
    }

    void Start()
    {
        cam = Camera.main;
        spriteRenderer = this.gameObject.transform.parent.GetComponent<SpriteRenderer>();   // ��ȡ��������͸������ SpriteRenderer ���
        kuntou = transform.parent.Find("kuntou").GetComponent<SpriteRenderer>(); 

        currentWeapon = this.transform.GetChild(0).gameObject;    // ��ȡ��ǰ��������
    }

    void Update()
    {
        // ÿ֡������׼
        Aim();
    }

    // ��׼�ľ���ʵ��
    void Aim()
    {
        // ��ȡ�������Ļ�ϵ�λ��
        mouseScreenPos = Input.mousePosition;

        // ������׼����
        aimDir = mouseScreenPos - cam.WorldToScreenPoint(currentWeapon.transform.position);
        aimDir.z = 0;
        aimDir.Normalize();

        // ������׼����ĽǶȲ���������ת����Ӧ�ĽǶ�
        float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;
        currentWeapon.transform.eulerAngles = new Vector3(0, 0, angle);

        // ���ݽǶȵ�������ĳ���
        PlayerAim(angle);
    }

    // ���ݽǶȵ�������ĳ���
    void PlayerAim(float angle)
    {
        // ����Ƕ��� -90 �� 90 ֮�䣬����ת SpriteRenderer������ת
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

    // ���������ķ���
    public static void UpdateWeapon(WeaponData weaponData)
    {
        // ����Ѿ����������������ٵ�ǰ�����������µ�����
        if (instance.gameObject.transform.childCount > 1)
        {
            instance.currentWeapon = Instantiate(weaponData.prefab, instance.transform);
        }
        else
        {
            // ���������������ֱ�������µ�����
            Destroy(instance.currentWeapon);
            instance.currentWeapon = Instantiate(weaponData.prefab, instance.transform);    //Instantiate������������instance��λ����weaponDateԤ���崴��
        }
    }
}
