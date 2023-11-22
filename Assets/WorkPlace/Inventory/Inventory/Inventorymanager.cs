using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Inventorymanager : MonoBehaviour
{
    //����������
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

    //������ұ���
    public void Setplayerinventory(Inventory inventory)
    {
        playerinventory = inventory;
        Refreshinventoryui();
    }
    //�������
    public void SetPlayer(GameObject player)
    {
        this.player = player;
    }
    //��ȡ���
    public GameObject Getplayer()
    {
        return this.player;
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
            if(playerinventory.GetItemList()[i]!=null)
            {
                Item item = playerinventory.GetItemList()[i];
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
