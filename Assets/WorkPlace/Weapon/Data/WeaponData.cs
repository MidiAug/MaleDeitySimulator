using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 在Unity编辑器中创建新的武器数据模板
[CreateAssetMenu(menuName = "ScriptableObject/Weapon", fileName = "NewWeapon")]
public class WeaponData : ScriptableObject
{
    // 武器的属性
    public Sprite sprite;    // 武器的精灵，用于渲染至游戏界面
    public string Name;       // 武器的名称
    public string type;       // 武器的类型
    public float damage;      // 武器的伤害值
    public float critical;    // 暴击概率
    public int weight;        // 生成权重
    [TextArea] public string info;  // 武器的描述信息
    public GameObject prefab; // 武器的预制体（用于在游戏中生成武器）

    // 子弹相关属性
    public float FireTimer;   // 射速，表示武器发射子弹的速度
}
