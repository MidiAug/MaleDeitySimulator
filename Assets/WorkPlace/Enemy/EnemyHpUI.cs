using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ����Ѫ��
/// </summary>
public class EnemyHpUI : MonoBehaviour
{
    // ����һ�� Slider �ؼ�������ʾ����ֵ
    private Slider hp;

    // ���� EnemyController ���͵Ķ������ڻ�ȡ���˵�����ֵ���������ֵ����Ϣ
    private EnemyController enemyController;

    void Start()
    {
        // ��ȡ��ǰ�����ϵ� Slider ���
        hp = GetComponent<Slider>();

        // ͨ������������"../.."��Ѱ�ң���ȡ�ö���ĸ����ĸ�����ͨ���ǵ��˵ĸ������ϵ� EnemyController ���
        enemyController = transform.Find("../..").gameObject.GetComponent<EnemyController>();
    }

    void Update()
    {
        // ��ÿһ֡����ʱ���� Slider ��ֵ����Ϊ��ǰ��������ֵ���������ֵ�ı�����ʵ������ֵ��ʵʱ��ʾ
        hp.value = enemyController.curHp / enemyController.enemyData.maxHp;
    }
}