using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Weapon/NewWeapon", fileName = "NewWeapon")]
//��������ģ��
public class WeaponData : ScriptableObject
{
    public Sprite weaponSprite;
    public string weaponName;
    public string weaponType;
    [TextArea] public string weaponInfo;
    public GameObject weaponPrefab;

    //�ӵ�
    public float FireTimer;//����

}
