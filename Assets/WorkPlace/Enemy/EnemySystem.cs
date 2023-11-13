using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySystem : MonoBehaviour
{
    // ����
    EnemyData enemyData;
    EnemyList enemyList;

    //���������
    GameObject enemy;

    //
    private void Awake()
    {
        enemyList = Resources.Load<EnemyList>(typeof(EnemyList).Name);
        if (enemyList != null)
        {
            enemyData = enemyList.list[0];          
        }

    }
    private void Start()
    {
        enemy = GameObject.Find("Enemy");
    }
    private void Update()
    {
        if (enemy.transform.childCount == 0)
        {
            if (enemyData != null)
            {

                Instantiate(enemyData.enemyPrefab, GameObject.FindGameObjectWithTag("Player").transform.position + Vector3.right*5, Quaternion.identity,enemy.transform);
            }
        }

    }
}
