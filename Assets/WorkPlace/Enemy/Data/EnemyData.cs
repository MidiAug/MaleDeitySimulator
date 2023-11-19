using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敌人数据
/// </summary>

//此行在资源的创建选项中创建了一个新的选项Enemy,在创建时文件自动命名为NewEnemy
[CreateAssetMenu(menuName = "ScriptableObject/Enemy", fileName = "newEnemy")]

//派生自ScriptableObject
public class EnemyData : ScriptableObject
{
    /// <summary>
    /// 从上至下依次为敌人的精灵，名字，种族，血量最大值，移动速度
    /// 敌人的相关信息(Textarea表示可以在unity中使用多行文本编辑器编辑)，敌人的预制体
    /// </summary>
    public Sprite enemySprite;
    public string Name;
    public string race;
    public float maxHp;
    public float moveSpeed;
    public float boxPossibility;
    [TextArea] public string enemyInfo;

    public GameObject enemyPrefab;
    public GameObject boxPrefab;
}
