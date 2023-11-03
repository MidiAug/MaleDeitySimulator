using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Weapon/NewWeapon", fileName = "NewWeapon")]
//ÎäÆ÷Êý¾ÝÄ£°å
public class WeaponData : ScriptableObject
{
    public Sprite WeaponSprite;
    public string WeaponName;
    public string WeaponType;
    [TextArea] public string WeaponInfo;

}
