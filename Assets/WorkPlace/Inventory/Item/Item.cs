using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//������Ʒ��
[System.Serializable]
public class Item
{
    public enum ItemType
    {
        //goldCoin,//���
        //copperCoin,//ͭ��
        //silverCoin,//����
        bloodpacks,//Ѫƿ
        damagepacks
    }
    public string Itemname;//��Ʒ����
    public int Itemamount;//��Ʒ����
    public ItemType itemType;
    //��ȡ��ǰ��Ʒ��ͼƬ
    public Sprite Getitemsprite()
    {
        switch (itemType)
        {
            default: //��Itemassets�й���������������Ʒ��ͼƬ��ֱ�����ü���
            //case Item.ItemType.copperCoin:return (Itemassets.Instance.Coopercoinprite);//ͭ��
            //case Item.ItemType.goldCoin: return (Itemassets.Instance.Goldcoinsprite);//���
            //case Item.ItemType.silverCoin: return (Itemassets.Instance.slivercoinprite);//����
            case Item.ItemType.bloodpacks: return (Itemassets.Instance.Bloodpacksprite); //Ѫƿ
            case Item.ItemType.damagepacks: return (Itemassets.Instance.Damagepacksprite); //Ѫƿ
        }
    }//��ƷͼƬ һһ��
}
