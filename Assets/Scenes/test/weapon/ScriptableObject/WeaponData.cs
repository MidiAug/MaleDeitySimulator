using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Weapon/NewWeapon", fileName = "NewWeapon")]
//ÎäÆ÷Êý¾ÝÄ£°å
public class WeaponData : ScriptableObject
{
    public Sprite weaponSprite;
    public string weaponName;
    public string weaponType;
    [TextArea] public string weaponInfo;
    public GameObject weaponPrefab;

}
