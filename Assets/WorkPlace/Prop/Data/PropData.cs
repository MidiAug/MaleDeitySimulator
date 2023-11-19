using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Prop", fileName = "newProp")]

public class PropData:ScriptableObject
{
    public string Name;
    public string type;
    public float val;// ֵ�����ͺܶ࣬����Ѫ���Ļظ�ֵ����ҵĽ��ȵ�
    public float weight;// Ȩ��
    [TextArea] public string info;

    public GameObject prefab;
}
