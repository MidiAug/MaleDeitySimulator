using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��������ϵͳ
public class WeaponSystem : MonoBehaviour
{
    // ���������б�͵�ǰ��������
    WeaponList weaponList;
    WeaponData weaponData;

    // ��ǰ����������
    int weaponIndex = 0;

    // �� Awake �׶Σ���start֮ǰ��ȡ�ļ�
    private void Awake()
    {
        // ���������б�
        weaponList = Resources.Load<WeaponList>("WeaponList");
        if (weaponList != null)
        {
            // ��ʼ����ǰ��������Ϊ�б��еĵ�һ����������������׼ϵͳ��������Ϣ
            weaponData = weaponList.list[0];
            AimSystem.UpdateWeapon(weaponData);
        }
    }

    // ��ÿһ֡���¼�������л�
    private void Update()
    {
        ChangeWeapon();
    }

    // ���������л��߼�
    void ChangeWeapon()
    {
        // �л�����һ������
        if (Input.GetKeyUp(KeyCode.Q))
        {
            ++weaponIndex;
            if (weaponList.list.Count == weaponIndex)
            {
                weaponIndex = 0;
            }

            // ��ȡ�µ��������ݣ���������׼ϵͳ��������Ϣ
            weaponData = weaponList.list[weaponIndex];
            AimSystem.UpdateWeapon(weaponData);
        }

        // �л�����һ������
        if (Input.GetKeyUp(KeyCode.E))
        {
            --weaponIndex;
            if (-1 == weaponIndex)
            {
                weaponIndex = weaponList.list.Count - 1;
            }

            // ��ȡ�µ��������ݣ���������׼ϵͳ��������Ϣ
            weaponData = weaponList.list[weaponIndex];
            AimSystem.UpdateWeapon(weaponData);
        }
    }
}
