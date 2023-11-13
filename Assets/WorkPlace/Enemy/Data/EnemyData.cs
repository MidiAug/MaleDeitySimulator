using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Enemy", fileName = "NewEnemy")]
public class EnemyData : ScriptableObject
{
    public Sprite enemySprite;
    public string Name;
    public string race;
    public float maxHp;
    public float moveSpeed;
    [TextArea] public string enemyInfo;

    public GameObject enemyPrefab;
}
