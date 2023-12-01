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
        int index = 0;
        for(int i=0;i<itemList.Count;i++)
        {
            if(itemList[i].itemType==item.itemType)
            {
                itemList[i].Itemamount+=item.Itemamount;
                break;
            }
            index++;
        }
        if(index==itemList.Count)
        {
            itemList.Add(item);
        }
    }
    //��ȡ��Ʒ�б�
    public List<Item> GetItemList()
    {
        return itemList;
    }
}
