using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Inventorymanager : MonoBehaviour
{
    //����������
    public static Inventorymanager Instance { get; set; }
    private void Awake()
    {
        Instance = this;
    }//ͬ���ñ����������ɣ���ֻ��һ��ʵ��

    private Inventory playerinventory;//��ұ���

    [SerializeField]public Transform slotpanel;
    [SerializeField]public GameObject slotprefab;

    private GameObject player;//��ҵ�ʵ��

    //������ұ���
    public void Setplayerinventory(Inventory inventory)
    {
        playerinventory = inventory;
        Refreshinventoryui();//ˢ�±�������ʾ
    }
    //��ȡ��ұ���
    public Inventory GetplayerInventory()
    {
        return playerinventory;
    }
    //�������
    public void SetPlayer(GameObject player1)
    {
        player = player1;
    }
    //��ȡ���
    public GameObject Getplayer()
    {
        return player;
    }
    //ˢ�±���
    public string Getinformation(Item item)
    {
        string iteminform;
        switch (item.Itemname)
        {
            default: //��Itemassets�й���������������Ʒ��ͼƬ��ֱ�����ü���
            case "bloodpacks": iteminform = "Here are some bloodpacks!  You can click the ��f�� to use it"; break;
            case "copperCoin": iteminform = "Here are some bloodpacks! You can click the ��f�� to use it"; break;
            case "goldCoin": iteminform = "Here are some bloodpacks! You can click the ��f�� to use it"; break;
            case "silverCoin": iteminform = "Here are some bloodpacks!  You can click the ��f�� to use it"; break;
        }
        return iteminform;
    }
    public void Refreshinventoryui()
    {
        //ɾ��ԭ���ɵ�
       foreach(Transform child in slotpanel.transform)
       {
         GameObject.Destroy(child.gameObject);
       }
       //����������Ʒ �����µĲ��
       for(int i=0;i<playerinventory.GetItemList().Count;i++)
        {
            if(playerinventory.GetItemList()[i]!=null)//playerinventory.GetItemList() ΪItemList 
            {//��ȡ��ǰ����������Ʒ
                Item item = playerinventory.GetItemList()[i];
                //����slot
                GameObject newslot = Instantiate(slotprefab, slotpanel);
                //Item Orignalitem = new Item { itemType= item.itemType,Itemamount= item.Itemamount,Itemname=item.Itemname};
                newslot.GetComponent<Drop>().item= item;
                newslot.GetComponent<Image>().sprite = item.Getitemsprite();
                if(item.Itemamount>1)
                {
                    newslot.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(item.Itemamount.ToString());
                }
                //��Ʒ����Ϣ��
                newslot.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().SetText(Getinformation(item).ToString());//Inventoryinform.Instance.Getinformation(item));
                newslot.transform.GetChild(1).gameObject.SetActive(false);
            }
        }
    }
}

