using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Inventorymanager : MonoBehaviour
{
    //背包管理器
    public static Inventorymanager Instance { get; set; }
    private void Awake()
    {
        Instance = this;
    }//同理让背包最先生成，且只有一个实例

    private Inventory playerinventory;//玩家背包

    [SerializeField]public Transform slotpanel;
    [SerializeField]public GameObject slotprefab;

    private GameObject player;//玩家的实例

    //设置玩家背包
    public void Setplayerinventory(Inventory inventory)
    {
        playerinventory = inventory;
        Refreshinventoryui();//刷新背包的显示
    }
    //获取玩家背包
    public Inventory GetplayerInventory()
    {
        return playerinventory;
    }
    //设置玩家
    public void SetPlayer(GameObject player1)
    {
        player = player1;
    }
    //获取玩家
    public GameObject Getplayer()
    {
        return player;
    }
    //刷新背包
    public string Getinformation(Item item)
    {
        string iteminform;
        switch (item.Itemname)
        {
            default: //在Itemassets中挂载着所有所需物品的图片，直接引用即可
            case "bloodpacks": iteminform = "Here are some bloodpacks!  You can click the ‘f’ to use it"; break;
            case "copperCoin": iteminform = "Here are some bloodpacks! You can click the ‘f’ to use it"; break;
            case "goldCoin": iteminform = "Here are some bloodpacks! You can click the ‘f’ to use it"; break;
            case "silverCoin": iteminform = "Here are some bloodpacks!  You can click the ‘f’ to use it"; break;
        }
        return iteminform;
    }
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
            if(playerinventory.GetItemList()[i]!=null)//playerinventory.GetItemList() 为ItemList 
            {//获取当前遍历到的物品
                Item item = playerinventory.GetItemList()[i];
                //生成slot
                GameObject newslot = Instantiate(slotprefab, slotpanel);
                //Item Orignalitem = new Item { itemType= item.itemType,Itemamount= item.Itemamount,Itemname=item.Itemname};
                newslot.GetComponent<Drop>().item= item;
                newslot.GetComponent<Image>().sprite = item.Getitemsprite();
                if(item.Itemamount>1)
                {
                    newslot.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(item.Itemamount.ToString());
                }
                //物品的信息：
                newslot.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().SetText(Getinformation(item).ToString());//Inventoryinform.Instance.Getinformation(item));
                newslot.transform.GetChild(1).gameObject.SetActive(false);
            }
        }
    }
}

