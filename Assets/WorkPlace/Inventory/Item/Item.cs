using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//������Ʒ��
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
        goldCoin,//���
        copperCoin,//ͭ��
        silverCoin,//����
        bloodpacks//Ѫƿ
    }
    public string Itemname;
    public int Itemamount;
}
