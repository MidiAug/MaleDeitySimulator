using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Weapon", fileName = "NewWeapon")]
//武器数据模板
public class WeaponData : ScriptableObject
{
    public Sprite sprite;
    public string Name;
    public string type;
    public float damage;
    [TextArea] public string info;
    public GameObject prefab;

    //子弹
    public float FireTimer;//射速

}
