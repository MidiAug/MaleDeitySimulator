using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Role/player", fileName = "NewPlayer")]
public class CharacterData : ScriptableObject
{
    public Sprite playerSprite;
    public string roleName;
    public string race;
    public float maxHp;
    public float hp;
    public float moveSpeed;
    [TextArea] public string playerInfo;

    public GameObject playerPrefab;
}
