using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHpUI : MonoBehaviour
{
    Slider hp;
    EnemyController enemyController;
    // Start is called before the first frame update
    void Start()
    {
        hp = GetComponent<Slider>();
        enemyController = transform.Find("../..").gameObject.GetComponent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        hp.value = enemyController.curHp / enemyController.enemyData.maxHp;
    }
}
