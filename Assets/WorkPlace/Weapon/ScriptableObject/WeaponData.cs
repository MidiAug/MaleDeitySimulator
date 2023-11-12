using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Weapon/NewWeapon", fileName = "NewWeapon")]
//武器数据模板
public class WeaponData : ScriptableObject
{
    public Sprite weaponSprite;
    public string weaponName;
    public string weaponType;
    [TextArea] public string weaponInfo;
    public GameObject weaponPrefab;

    //子弹
    public float FireTimer;//射速

}
