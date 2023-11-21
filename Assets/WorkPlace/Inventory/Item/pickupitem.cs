using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupitem : MonoBehaviour
{
    private Inventory inventory;
    private void Awake()
    {
        Inventorymanager.Instance.SetPlayer(this.gameObject);
        inventory = new Inventory();        

        //≤‚ ‘ÃÌº”ŒÔ∆∑
        inventory.Additem(new Item { itemType=Item.ItemType.goldCoin,Itemamount = 666, Itemname = "goldCoin" });
        inventory.Additem(new Item { itemType = Item.ItemType.copperCoin,Itemamount = 555, Itemname = "bloodpacks" });
        inventory.Additem(new Item { itemType = Item.ItemType.silverCoin, Itemamount = 444, Itemname = "bloodpacks" });
        inventory.Additem(new Item { itemType = Item.ItemType.bloodpacks, Itemamount = 333, Itemname = "bloodpacks" });
        Inventorymanager.Instance.Setplayerinventory(inventory);
    }

    private void Start()
    {

    }
}
