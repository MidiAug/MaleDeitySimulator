using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 武器列表类
/// </summary>

//在unity的Asset处新创建一个WeaponList选项
[CreateAssetMenu(menuName = "ScriptableObject/WeaponList", fileName = "NewWeaponList")]
//武器列表
public class WeaponList : ScriptableObject
{
    public List<WeaponData> list = new List<WeaponData>();
}
