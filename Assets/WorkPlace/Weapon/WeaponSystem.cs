using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 武器管理系统
public class WeaponSystem : MonoBehaviour
{
    // 引用武器列表和当前武器数据
    WeaponList weaponList;
    WeaponData weaponData;

    // 当前武器的索引
    int weaponIndex = 0;

    // 在 Awake 阶段，即start之前获取文件
    private void Awake()
    {
        // 加载武器列表
        weaponList = Resources.Load<WeaponList>("WeaponList");
        if (weaponList != null)
        {
            // 初始化当前武器数据为列表中的第一把武器，并更新瞄准系统的武器信息
            weaponData = weaponList.list[0];
            AimSystem.UpdateWeapon(weaponData);
        }
    }

    // 在每一帧更新检测武器切换
    private void Update()
    {
        ChangeWeapon();
    }

    // 处理武器切换逻辑
    void ChangeWeapon()
    {
        // 切换到下一把武器
        if (Input.GetKeyUp(KeyCode.Q))
        {
            ++weaponIndex;
            if (weaponList.list.Count == weaponIndex)
            {
                weaponIndex = 0;
            }

            // 获取新的武器数据，并更新瞄准系统的武器信息
            weaponData = weaponList.list[weaponIndex];
            AimSystem.UpdateWeapon(weaponData);
        }

        // 切换到上一把武器
        if (Input.GetKeyUp(KeyCode.E))
        {
            --weaponIndex;
            if (-1 == weaponIndex)
            {
                weaponIndex = weaponList.list.Count - 1;
            }

            // 获取新的武器数据，并更新瞄准系统的武器信息
            weaponData = weaponList.list[weaponIndex];
            AimSystem.UpdateWeapon(weaponData);
        }
    }
}
