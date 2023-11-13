using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/WeaponList", fileName = "NewWeaponList")]
//ÎäÆ÷ÁÐ±í
public class WeaponList : ScriptableObject
{
    public List<WeaponData> list = new List<WeaponData>();
}
