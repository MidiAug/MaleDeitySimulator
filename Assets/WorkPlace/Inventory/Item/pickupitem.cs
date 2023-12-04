using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupitem : MonoBehaviour
{
    private Inventory inventory;
    private bool Ispick=false;
    private Dropitem dropitem;
    private void Start()//不能使用awake因为实例还没创建
    {
        Inventorymanager.Instance.SetPlayer(this.gameObject);
        inventory = new Inventory();
        //测试添加物品
        //inventory.Additem(new Item { itemType = Item.ItemType.goldCoin, Itemamount = 6, Itemname = "goldCoin" });
        //inventory.Additem(new Item { itemType = Item.ItemType.copperCoin, Itemamount = 5, Itemname = "bloodpacks" });
        //inventory.Additem(new Item { itemType = Item.ItemType.silverCoin, Itemamount = 4, Itemname = "bloodpacks" });
        //inventory.Additem(new Item { itemType = Item.ItemType.bloodpacks, Itemamount = 3, Itemname = "bloodpacks" });
        Inventorymanager.Instance.Setplayerinventory(inventory);
    }
    private void Update()
    {
        if(Ispick==true)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                inventory.Additem(dropitem.item);
                Inventorymanager.Instance.Refreshinventoryui();
                Ispick = false;
                GameObject.Destroy(dropitem.gameObject);
            }
        }
    } 
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("item"))
        {
            dropitem = collision.gameObject.GetComponent<Dropitem>();
            if(dropitem!=null)
            {
                Ispick = true;
            }
        }
    }//持续触发碰撞器用于拾取物品

    private void OnTriggerExit2D(Collider2D collision)
    {
        Ispick = false;
    }
}

