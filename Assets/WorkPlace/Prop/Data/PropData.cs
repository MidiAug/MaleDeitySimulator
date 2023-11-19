using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Prop", fileName = "newProp")]

public class PropData:ScriptableObject
{
    public string Name;
    public string type;
    public float val;// 值的类型很多，比如血包的回复值，金币的金额等等
    public float weight;// 权重
    [TextArea] public string info;

    public GameObject prefab;
}
