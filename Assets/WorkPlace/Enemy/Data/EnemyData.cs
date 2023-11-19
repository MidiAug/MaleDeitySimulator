using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��������
/// </summary>

//��������Դ�Ĵ���ѡ���д�����һ���µ�ѡ��Enemy,�ڴ���ʱ�ļ��Զ�����ΪNewEnemy
[CreateAssetMenu(menuName = "ScriptableObject/Enemy", fileName = "newEnemy")]

//������ScriptableObject
public class EnemyData : ScriptableObject
{
    /// <summary>
    /// ������������Ϊ���˵ľ��飬���֣����壬Ѫ�����ֵ���ƶ��ٶ�
    /// ���˵������Ϣ(Textarea��ʾ������unity��ʹ�ö����ı��༭���༭)�����˵�Ԥ����
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
