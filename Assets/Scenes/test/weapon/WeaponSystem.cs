using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//����ϵͳ
public class WeaponSystem : MonoBehaviour
{
    WeaponList weaponList;
    //��ȡ�ļ�awake
    private void Awake()
    {
        weaponList = Resources.Load<WeaponList>(typeof(WeaponList).Name);
        Debug.Log(weaponList);
    }
}
