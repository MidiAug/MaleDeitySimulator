using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/EnemyList", fileName = "EnemyList")]
//�����б�
public class EnemyList : ScriptableObject
{
    public List<EnemyData> list = new List<EnemyData>();
}
