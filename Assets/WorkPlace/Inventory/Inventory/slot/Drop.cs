using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Drop : MonoBehaviour,IPointerClickHandler
{
    public Item item;
    private Item Orignalitem;
    private Inventory playerinventory;
    private void Start()
    {
        playerinventory = Inventorymanager.Instance.GetplayerInventory();
        Orignalitem = new Item { itemType = item.itemType, Itemamount = 1, Itemname = item.Itemname };
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            //������Ʒ
            Dropitem.Createitem(Inventorymanager.Instance.Getplayer().transform.position, Orignalitem, true);//��ΪInventorymanager��������������
            if (item.Itemamount > 1)
            {
                item.Itemamount--;

            }
            else
            {
                playerinventory.GetItemList().Remove(item);
            }
            Inventorymanager.Instance.Refreshinventoryui();
        }
    }
}
