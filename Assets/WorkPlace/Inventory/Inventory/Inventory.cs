using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private List<Item> itemList;//������ʵ������һ����Ʒ�б�
    public Inventory()
    {
        itemList = new List<Item>();
    }//��ʼ������

    //�����Ʒ ��ʰȡ��Ʒ��ʱ�����
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
