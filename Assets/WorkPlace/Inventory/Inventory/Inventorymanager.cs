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
            }
        }
    }
 }
