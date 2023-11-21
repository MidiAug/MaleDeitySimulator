using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//背包物品类
[System.Serializable]
public class Item
{
    public enum ItemType
    {
        goldCoin,//金币
        copperCoin,//铜币
        silverCoin,//银币
        bloodpacks//血瓶
    }
    public string Itemname;
    public int Itemamount;
    public ItemType itemType;
    //获取当前物品的图片
    public Sprite Getitemsprite()
    {
        switch (itemType)
        {
            default:
            case Item.ItemType.copperCoin:return (Itemassets.Instance.Coopercoinprite);
            case Item.ItemType.goldCoin: return (Itemassets.Instance.Goldcoinsprite);
            case Item.ItemType.silverCoin: return (Itemassets.Instance.slivercoinprite);
            case Item.ItemType.bloodpacks: return (Itemassets.Instance.Bloodpacksprite);
        }
    }
}
