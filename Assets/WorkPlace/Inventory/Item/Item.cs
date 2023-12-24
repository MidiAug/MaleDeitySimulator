using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//������Ʒ��
[System.Serializable]
public class Item
{
    public enum ItemType
    {
        bloodpacks,//Ѫƿ
        damagepacks,//�˺�ҩˮ
        wudipacks,//�޵�ҩˮ
        crytalpacks//ˮ��ҩˮ
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
            case Item.ItemType.crytalpacks: return (Itemassets.Instance.crytalpacksprite);//���
            case Item.ItemType.wudipacks: return (Itemassets.Instance.wudipacksprite);//����
            case Item.ItemType.bloodpacks: return (Itemassets.Instance.Bloodpacksprite); //Ѫƿ
            case Item.ItemType.damagepacks: return (Itemassets.Instance.Damagepacksprite); //Ѫƿ
        }
    }//��ƷͼƬ һһ��
}
