using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private List<Item> itemList;
    public Inventory()
    {
        itemList = new List<Item>();
    }

    //������Ʒ
    public void Additem(Item item)
    {
        itemList.Add(item);
    }

    //��ȡ��Ʒ�б�
    public List<Item> GetItemList()
    {
        return itemList;
    }


}