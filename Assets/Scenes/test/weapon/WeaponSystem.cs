using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//武器系统
public class WeaponSystem : MonoBehaviour
{
    WeaponList weaponList;
    //获取文件awake
    private void Awake()
    {
        weaponList = Resources.Load<WeaponList>(typeof(WeaponList).Name);
        Debug.Log(weaponList);
    }
}
