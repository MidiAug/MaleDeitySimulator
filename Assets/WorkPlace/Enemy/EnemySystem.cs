using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySystem : MonoBehaviour
{
    EnemyData enemyData;
    EnemyList enemyList;
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
        if (enemyData != null)
        {

            //Instantiate(enemyData.enemyPrefab,GameObject.FindGameObjectWithTag("Player").transform.position+Vector3.right,Quaternion.identity);
        }
    }
}
