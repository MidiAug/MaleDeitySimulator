using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private List<Item> itemList;//背包在实质上是一个物品列表
    public Inventory()
    {
        itemList = new List<Item>();
    }//初始化背包

    //添加物品 在拾取物品的时候调用
    public void Additem(Item item)
    {
        itemList.Add(item);
    }
    //获取物品列表
    public List<Item> GetItemList()
    {
        return itemList;
    }
}
