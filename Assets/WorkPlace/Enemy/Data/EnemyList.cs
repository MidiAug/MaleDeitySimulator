using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����б�
/// </summary>

//��������Դ�Ĵ���ѡ���д�����һ���µ�ѡ��EnemyList,�ڴ���ʱ�ļ��Զ�����ΪEnemyList
[CreateAssetMenu(menuName = "ScriptableObject/EnemyList", fileName = "EnemyList")]
public class EnemyList : ScriptableObject
{
    //��EnemyData����list
    public List<EnemyData> list = new List<EnemyData>();
}
