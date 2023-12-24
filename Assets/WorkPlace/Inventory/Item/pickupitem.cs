using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupitem : MonoBehaviour
{
    private Inventory inventory;
    private bool Ispick = false;
    private Dropitem dropitem;
    public WeaponList WeaponList;
    public CrystallController crystallController;
    public PlayerController playerController;
    private void Start()//����ʹ��awake��Ϊʵ����û����
    {
        Inventorymanager.Instance.SetPlayer(this.gameObject);
        inventory = new Inventory();
        //���������
        inventory.Additem(new Item { itemType = Item.ItemType.bloodpacks, Itemamount = 3, Itemname = "bloodpacks" });
        inventory.Additem(new Item { itemType = Item.ItemType.damagepacks, Itemamount = 3, Itemname = "damagepacks" });
        inventory.Additem(new Item { itemType = Item.ItemType.wudipacks, Itemamount = 3, Itemname = "wudipacks" });
        inventory.Additem(new Item { itemType = Item.ItemType.crytalpacks, Itemamount = 3, Itemname = "crytalpacks" });
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
            StartCoroutine(ApplywudiPacks());//ʹ��w�޵�ҩˮ
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))//ʹ��Ѫƿ
        {
            Inventory playerInventory = Inventorymanager.Instance.GetplayerInventory();
            for (int i = 0; i < playerInventory.GetItemList().Count; i++)
            {
                if (playerInventory.GetItemList()[i] != null)
                {
                    Item item = playerInventory.GetItemList()[i];
                    if (item.Itemname == "crytalpacks")
                    {
                        if (crystallController.curHealth == crystallController.maxHealth)
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
                        crystallController.curHealth += (crystallController.maxHealth) / 4;
                        if (crystallController.curHealth > crystallController.maxHealth)
                        {
                            crystallController.curHealth = crystallController.maxHealth;
                        }
                        Inventorymanager.Instance.Refreshinventoryui();
                        anobag.Instance.Refreshinventoryui();
                    }
                }
            }
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
    IEnumerator ApplywudiPacks()
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
                    float elapsedTime = 0f;
                    float invincibleDuration = 5f;
                    Inventorymanager.Instance.Refreshinventoryui();
                    anobag.Instance.Refreshinventoryui();
                    // ��5���ڳ������ú���a
                    while (elapsedTime < invincibleDuration)
                    {
                        playerController.isInvincible=true;
                        elapsedTime += Time.deltaTime;
                        yield return null;
                    }
                    playerController.isInvincible = false;
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