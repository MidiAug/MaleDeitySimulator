using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��Unity�༭���д����µ���������ģ��
[CreateAssetMenu(menuName = "ScriptableObject/Weapon", fileName = "NewWeapon")]
public class WeaponData : ScriptableObject
{
    // ����������
    public Sprite sprite;    // �����ľ��飬������Ⱦ����Ϸ����
    public string Name;       // ����������
    public string type;       // ����������
    public float damage;      // �������˺�ֵ
    public float critical;    // ��������
    public int weight;        // ����Ȩ��
    [TextArea] public string info;  // ������������Ϣ
    public GameObject prefab; // ������Ԥ���壨��������Ϸ������������

    // �ӵ��������
    public float FireTimer;   // ���٣���ʾ���������ӵ����ٶ�
}
