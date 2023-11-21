using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupitem : MonoBehaviour
{
    private Inventory inventory;
    private void Awake()
    {
        inventory = new Inventory();
        //≤‚ ‘ÃÌº”ŒÔ∆∑
        inventory.Additem(new Item { Itemamount = 1, Itemname = "GOLDCOIN" });
        inventory.Additem(new Item { Itemamount = 2, Itemname = "hleathybottom" });
        Inventorymanager.Instance.Setplayerinventory(inventory);


    }

    //

}
