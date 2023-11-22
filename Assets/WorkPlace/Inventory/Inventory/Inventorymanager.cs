using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Inventorymanager : MonoBehaviour
{
    //背包管理器
    public static Inventorymanager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    private Inventory playerinventory;
    //
    [SerializeField]private Transform slotpanel;
    [SerializeField]private GameObject slotprefab;
    private GameObject player;

    //设置玩家背包
    public void Setplayerinventory(Inventory inventory)
    {
        playerinventory = inventory;
        Refreshinventoryui();
    }
    //设置玩家
    public void SetPlayer(GameObject player)
    {
        this.player = player;
    }
    //获取玩家
    public GameObject Getplayer()
    {
        return this.player;
    }
    //刷新背包
    public void Refreshinventoryui()
    {
        //删除原来旧的
       foreach(Transform child in slotpanel.transform)
       {
         GameObject.Destroy(child.gameObject);
       }
       //便利背包物品 建立新的插槽
       for(int i=0;i<playerinventory.GetItemList().Count;i++)
        {
            if(playerinventory.GetItemList()[i]!=null)
            {
                Item item = playerinventory.GetItemList()[i];
                //生成slot
                GameObject newslot = Instantiate(slotprefab, slotpanel);
                newslot.GetComponent<Drop>().item=item;
                //信息同步到item上
                newslot.GetComponent<Image>().sprite = item.Getitemsprite();
                if(item.Itemamount>1)
                {
                    newslot.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(item.Itemamount.ToString());
                }
            }
        }
    }
 }
