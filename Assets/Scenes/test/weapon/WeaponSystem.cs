using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//武器管理系统
public class WeaponSystem : MonoBehaviour
{
    WeaponList weaponList;
    WeaponData weaponData;
    int weaponIndex = 0;
    //获取文件awake
    private void Awake()
    {
        weaponList = Resources.Load<WeaponList>(typeof(WeaponList).Name);
        weaponData = weaponList.list[0];
    }
    private void Update()
    {
        if (weaponList != null)
        {
            ChangeWeapon();
            AimSystem.UpdateWeapon(weaponData);
        }
    }
    void ChangeWeapon()
    {
        if (Input.GetKeyUp(KeyCode.Q))
        {
            ++weaponIndex;
            if (weaponList.list.Count > weaponIndex)
            {
                weaponData = weaponList.list[weaponIndex];
                AimSystem.UpdateWeapon(weaponData);
            }
            else
            {
                weaponIndex = 0;
            }
        }
        if (Input.GetKeyUp(KeyCode.E))
        {

        }
    }
}
