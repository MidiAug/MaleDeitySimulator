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
            {
                Item item = playerinventory.GetItemList()[i];//��ȡ��ǰ����������Ʒ
                //����slot
                GameObject newslot = Instantiate(slotprefab, slotpanel);
                newslot.GetComponent<Drop>().item=item;
                //��Ϣͬ����item��
                newslot.GetComponent<Image>().sprite = item.Getitemsprite();
                if(item.Itemamount>1)
                {
                    newslot.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(item.Itemamount.ToString());
                }
            }
        }
    }
 }
