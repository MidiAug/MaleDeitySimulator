using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//背包物品类
[System.Serializable]
public class Item
{
    PropList propList;
    void Awake()
    {
        propList = Resources.Load<PropList>(typeof(PropList).Name);
    }
    public enum ItemType
    {
        goldCoin,//金币
        copperCoin,//铜币
        silverCoin,//银币
        bloodpacks//血瓶
    }
    public string Itemname;
    public int Itemamount;
}
