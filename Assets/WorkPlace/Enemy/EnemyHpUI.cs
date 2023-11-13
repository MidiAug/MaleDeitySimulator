using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 敌人血条
/// </summary>
public class EnemyHpUI : MonoBehaviour
{
    // 定义一个 Slider 控件用于显示生命值
    private Slider hp;

    // 引用 EnemyController 类型的对象，用于获取敌人的生命值和最大生命值等信息
    private EnemyController enemyController;

    void Start()
    {
        // 获取当前对象上的 Slider 组件
        hp = GetComponent<Slider>();

        // 通过向上两级（"../.."）寻找，获取该对象的父级的父级（通常是敌人的根对象）上的 EnemyController 组件
        enemyController = transform.Find("../..").gameObject.GetComponent<EnemyController>();
    }

    void Update()
    {
        // 在每一帧更新时，将 Slider 的值设置为当前敌人生命值与最大生命值的比例，实现生命值的实时显示
        hp.value = enemyController.curHp / enemyController.enemyData.maxHp;
    }
}