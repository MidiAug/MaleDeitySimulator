using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Drop : MonoBehaviour,IPointerClickHandler
{
    public Item item;
    private Inventory playerinventory;
    private void Start()
    {
        playerinventory = Inventorymanager.Instance.GetplayerInventory();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            //丢弃物品
            Dropitem.Createitem(Inventorymanager.Instance.Getplayer().transform.position, item);//因为Inventorymanager挂载在人物身上
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
