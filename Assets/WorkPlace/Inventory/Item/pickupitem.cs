using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupitem : MonoBehaviour
{
    private Inventory inventory;
    private bool Ispick = false;
    private Dropitem dropitem;
    public WeaponList WeaponList;
    private void Start()//����ʹ��awake��Ϊʵ����û����
    {
        Inventorymanager.Instance.SetPlayer(this.gameObject);
        inventory = new Inventory();
        //���������Ʒ
        //inventory.Additem(new Item { itemType = Item.ItemType.goldCoin, Itemamount = 6, Itemname = "goldCoin" });
        //inventory.Additem(new Item { itemType = Item.ItemType.copperCoin, Itemamount = 5, Itemname = "copperCoin" });
        //inventory.Additem(new Item { itemType = Item.ItemType.silverCoin, Itemamount = 4, Itemname = "silverCoin" });
        inventory.Additem(new Item { itemType = Item.ItemType.bloodpacks, Itemamount = 3, Itemname = "bloodpacks" });
        inventory.Additem(new Item { itemType = Item.ItemType.damagepacks, Itemamount = 3, Itemname = "damagepacks" });
        Inventorymanager.Instance.Setplayerinventory(inventory);
        anobag.Instance.Setplayerinventory(inventory);
    }
    private void Update()
    {
        if (Ispick == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                inventory.Additem(dropitem.item);
                Inventorymanager.Instance.Refreshinventoryui();
                Ispick = false;
                GameObject.Destroy(dropitem.gameObject);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))//ʹ��Ѫƿ
        {
            Inventory playerInventory = Inventorymanager.Instance.GetplayerInventory();
            for (int i = 0; i < playerInventory.GetItemList().Count; i++)
            {
                if (playerInventory.GetItemList()[i] != null)
                {
                    Item item = playerInventory.GetItemList()[i];
                    if (item.Itemname == "bloodpacks")
                    {
                        if (PlayerController.curHp == PlayerController.maxHp)
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
                        PlayerController.curHp += (PlayerController.maxHp) / 4;
                        if (PlayerController.curHp > PlayerController.maxHp)
                        {
                            PlayerController.curHp = PlayerController.maxHp;
                        }
                        Inventorymanager.Instance.Refreshinventoryui();
                        anobag.Instance.Refreshinventoryui();
                    }
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            StartCoroutine(ApplyDamagePacks());//ʹ���˺�ҩˮ
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            StartCoroutine(ApplywudiPacks());//ʹ���˺�ҩˮ
        }
    }
    IEnumerator ApplyDamagePacks()//�ӳ�5sҩˮЧ��
    {
        Inventory playerInventory = Inventorymanager.Instance.GetplayerInventory();

        for (int i = 0; i < playerInventory.GetItemList().Count; i++)
        {
            if (playerInventory.GetItemList()[i] != null)
            {
                Item item = playerInventory.GetItemList()[i];

                if (item.Itemname == "damagepacks")
                {
                    if (item.Itemamount > 1)
                    {
                        item.Itemamount--;
                    }
                    else
                    {
                        playerInventory.GetItemList().Remove(item);
                    }

                    Damageup();
                    Inventorymanager.Instance.Refreshinventoryui();
                    anobag.Instance.Refreshinventoryui();

                    // Wait for 5 seconds
                    yield return new WaitForSeconds(5f);

                    Damagedown();
                    Inventorymanager.Instance.Refreshinventoryui();
                    anobag.Instance.Refreshinventoryui();
                }
            }
        }
    }
    IEnumerator ApplywudiPacks()//�ӳ��޵�Ч��
    {
        Inventory playerInventory = Inventorymanager.Instance.GetplayerInventory();

        for (int i = 0; i < playerInventory.GetItemList().Count; i++)
        {
            if (playerInventory.GetItemList()[i] != null)
            {
                Item item = playerInventory.GetItemList()[i];

                if (item.Itemname == "wudipacks")
                {
                    if (item.Itemamount > 1)
                    {
                        item.Itemamount--;
                    }
                    else
                    {
                        playerInventory.GetItemList().Remove(item);
                    }

                    Damageup();
                    Inventorymanager.Instance.Refreshinventoryui();
                    anobag.Instance.Refreshinventoryui();

                    yield return new WaitForSeconds(5f);

                    Damagedown();
                    Inventorymanager.Instance.Refreshinventoryui();
                    anobag.Instance.Refreshinventoryui();
                }
            }
        }
    }
    private void Awake()
    {
        WeaponList = Resources.Load<WeaponList>(typeof(WeaponList).Name);
    }
    public void Damageup()
    {
        IList list = WeaponList.list;
        for (int i = 0; i < list.Count; i++)
        {
            WeaponData tmp = (WeaponData)list[i];
            tmp.damage *= 2;
        }
    }
    public void Damagedown()
    {
        IList list = WeaponList.list;
        for (int i = 0; i < list.Count; i++)
        {
            WeaponData tmp = (WeaponData)list[i];
            tmp.damage /= 2;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
        {
            if(collision.gameObject.CompareTag("item"))
            {
                dropitem = collision.gameObject.GetComponent<Dropitem>();
                if(dropitem!=null)//���ǿ�������һ��������
                {
                    Ispick = true;//ͨ������ispick�ж�ʰȡ
                }
            }
        }//����������ײ������ʰȡ��Ʒ
    private void OnTriggerExit2D(Collider2D collision)
    {
        Ispick = false;
    }
}

