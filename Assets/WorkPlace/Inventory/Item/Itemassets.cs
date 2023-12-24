using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//用于管理物品的图片
public class Itemassets : MonoBehaviour
{
    public static Itemassets Instance { get; set; }
    private void Awake()
    {
        Instance=this;
    }//确保只有这一个实例，且之后不同再重复生成。

    //public Sprite Goldcoinsprite;//铜币图片
    public Sprite Bloodpacksprite;//血瓶图片
    public Sprite Damagepacksprite;//血瓶图片
    //public Sprite Coopercoinprite;//铜币图片
    //public Sprite slivercoinprite;//银币图片
    public GameObject DropitemPrefab;//管理掉落物
}
