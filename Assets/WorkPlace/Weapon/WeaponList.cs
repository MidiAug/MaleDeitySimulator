using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����б���
/// </summary>

//��unity��Asset���´���һ��WeaponListѡ��
[CreateAssetMenu(menuName = "ScriptableObject/WeaponList", fileName = "NewWeaponList")]
//�����б�
public class WeaponList : ScriptableObject
{
    public List<WeaponData> list = new List<WeaponData>();
}
