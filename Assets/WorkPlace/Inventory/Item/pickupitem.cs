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
        inventory.Additem(new Item { itemType = Item.ItemType.goldCoin, Itemamount = 6, Itemname = "goldCoin" });
        inventory.Additem(new Item { itemType = Item.ItemType.copperCoin, Itemamount = 5, Itemname = "copperCoin" });
        inventory.Additem(new Item { itemType = Item.ItemType.silverCoin, Itemamount = 4, Itemname = "silverCoin" });
        inventory.Additem(new Item { itemType = Item.ItemType.bloodpacks, Itemamount = 3, Itemname = "bloodpacks" });
        Inventorymanager.Instance.Setplayerinventory(inventory);
        anobag.Instance.Setplayerinventory(inventory);
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
        if (Input.GetKeyDown(KeyCode.F))//使用血瓶
        {
            Inventory playerInventory= Inventorymanager.Instance.GetplayerInventory();
            for (int i = 0; i < playerInventory.GetItemList().Count; i++)
            {
                if (playerInventory.GetItemList()[i] != null)
                {
                    Item item = playerInventory.GetItemList()[i];
                    if (item.Itemname == "bloodpacks")
                    {
                        if(PlayerController.curHp == PlayerController.maxHp)
                        {
                            break;
                        }
                        if (item.Itemamount > 1)
                        {
                            item.Itemamount--;
                        }
                        else
                        {
                            playerInventory.GetItemList().Remove(item);
                        }
                        PlayerController.curHp += (PlayerController.maxHp)/4;
                        if(PlayerController.curHp> PlayerController.maxHp)
                        {
                            PlayerController.curHp = PlayerController.maxHp;
                        }
                        Inventorymanager.Instance.Refreshinventoryui();
                        anobag.Instance.Refreshinventoryui();
                    }
                }
            }
        }
    } 
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("item"))
        {
            dropitem = collision.gameObject.GetComponent<Dropitem>();
            if(dropitem!=null)//不是空气而是一个掉落物
            {
                Ispick = true;//通过开关ispick判断拾取
            }
        }
    }//持续触发碰撞器用于拾取物品

    private void OnTriggerExit2D(Collider2D collision)
    {
        Ispick = false;
    }
}

