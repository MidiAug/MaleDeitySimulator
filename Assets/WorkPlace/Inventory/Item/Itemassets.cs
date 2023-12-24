using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//用于管理物品的图片
public class Itemassets : MonoBehaviour
{
    public static Itemassets Instance { get; set; }
    private void Awake()
    {
        Instance = this;
    }//确保只有这一个实例，且之后不同再重复生成。

    public Sprite Bloodpacksprite;//血瓶图片
    public Sprite Damagepacksprite;//血瓶图片
    public Sprite wudipacksprite;//无敌药水图片
    public Sprite crytalpacksprite;//水晶回血药水图片
    public GameObject DropitemPrefab;//管理掉落物
}