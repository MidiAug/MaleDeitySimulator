using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//������Ʒ��
[System.Serializable]
public class Item
{
    public enum ItemType
    {
        goldCoin,//���
        copperCoin,//ͭ��
        silverCoin,//����
        bloodpacks//Ѫƿ
    }
    public string Itemname;
    public int Itemamount;
    public ItemType itemType;
    //��ȡ��ǰ��Ʒ��ͼƬ
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
