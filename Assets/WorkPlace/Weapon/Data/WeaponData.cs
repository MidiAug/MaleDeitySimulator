using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Weapon", fileName = "NewWeapon")]
//��������ģ��
public class WeaponData : ScriptableObject
{
    public Sprite sprite;
    public string Name;
    public string type;
    public float damage;
    [TextArea] public string info;
    public GameObject prefab;

    //�ӵ�
    public float FireTimer;//����

}
