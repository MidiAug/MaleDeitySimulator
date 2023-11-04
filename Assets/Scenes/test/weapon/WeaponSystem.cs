using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��������ϵͳ
public class WeaponSystem : MonoBehaviour
{
    WeaponList weaponList;
    WeaponData weaponData;
    int weaponIndex = 0;
    //��ȡ�ļ�awake
    private void Awake()
    {
        weaponList = Resources.Load<WeaponList>(typeof(WeaponList).Name);
        if (weaponList != null)
        {
            weaponData = weaponList.list[0];
            AimSystem.UpdateWeapon(weaponData);
        }
        
    }
    private void Update()
    {
        ChangeWeapon();
    }
    void ChangeWeapon()
    {
        //��һ��
        if (Input.GetKeyUp(KeyCode.Q))
        {
            ++weaponIndex;
            if(weaponList.list.Count == weaponIndex) 
            {
                weaponIndex = 0;
            }
            weaponData = weaponList.list[weaponIndex];
            AimSystem.UpdateWeapon(weaponData);
            
        }

        //��һ��
        if (Input.GetKeyUp(KeyCode.E))
        {
            --weaponIndex;
            if (-1 == weaponIndex)
            {
                weaponIndex = weaponList.list.Count-1;
            }
            weaponData = weaponList.list[weaponIndex];
            AimSystem.UpdateWeapon(weaponData);

        }
    }
}