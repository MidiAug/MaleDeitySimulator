using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//背包物品类
[System.Serializable]
public class Item
{
    public enum ItemType
    {
        //goldCoin,//金币
        //copperCoin,//铜币
        //silverCoin,//银币
        bloodpacks,//血瓶
        damagepacks
    }
    public string Itemname;//物品名字
    public int Itemamount;//物品数量
    public ItemType itemType;
    //获取当前物品的图片
    public Sprite Getitemsprite()
    {
        switch (itemType)
        {
            default: //在Itemassets中挂载着所有所需物品的图片，直接引用即可
            //case Item.ItemType.copperCoin:return (Itemassets.Instance.Coopercoinprite);//铜币
            //case Item.ItemType.goldCoin: return (Itemassets.Instance.Goldcoinsprite);//金币
            //case Item.ItemType.silverCoin: return (Itemassets.Instance.slivercoinprite);//银币
            case Item.ItemType.bloodpacks: return (Itemassets.Instance.Bloodpacksprite); //血瓶
            case Item.ItemType.damagepacks: return (Itemassets.Instance.Damagepacksprite); //血瓶
        }
    }//物品图片 一一绑定
}
