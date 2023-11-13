using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 武器列表
/// </summary>

//此行在资源的创建选项中创建了一个新的选项EnemyList,在创建时文件自动命名为EnemyList
[CreateAssetMenu(menuName = "ScriptableObject/EnemyList", fileName = "EnemyList")]
public class EnemyList : ScriptableObject
{
    //用EnemyData创建list
    public List<EnemyData> list = new List<EnemyData>();
}
