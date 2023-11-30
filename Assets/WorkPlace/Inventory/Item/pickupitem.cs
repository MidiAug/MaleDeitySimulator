using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupitem : MonoBehaviour
{
    private Inventory inventory;
    private void Start()//����ʹ��awake��Ϊʵ����û����
    {
        Inventorymanager.Instance.SetPlayer(this.gameObject);
        inventory = new Inventory();
        //���������Ʒ
        inventory.Additem(new Item { itemType = Item.ItemType.goldCoin, Itemamount = 6, Itemname = "goldCoin" });
        inventory.Additem(new Item { itemType = Item.ItemType.copperCoin, Itemamount = 5, Itemname = "bloodpacks" });
        inventory.Additem(new Item { itemType = Item.ItemType.silverCoin, Itemamount = 4, Itemname = "bloodpacks" });
        inventory.Additem(new Item { itemType = Item.ItemType.bloodpacks, Itemamount = 3, Itemname = "bloodpacks" });
        Inventorymanager.Instance.Setplayerinventory(inventory);

        Dropitem.Createitem(this.gameObject.transform.position, new Item { itemType = Item.ItemType.goldCoin, Itemamount = 666, Itemname = "goldCoin" });
        Dropitem.Createitem(this.gameObject.transform.position, new Item { itemType = Item.ItemType.copperCoin, Itemamount = 555, Itemname = "bloodpacks" });
    }
}

