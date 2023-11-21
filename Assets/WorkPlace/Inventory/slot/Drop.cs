using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Drop : MonoBehaviour
{
    public Item item;

    public void OnPointerclick(PointerEventData eventData)
    {
        if(eventData.button==PointerEventData.InputButton.Right)
        {
            //¶ªÆúÎïÆ·
           Dropitem.createitem(Inventorymanager.Instance.Getplyer().transform.position,item);
            if(item.Itemamount>1)
            {
                item.Itemamount--;
            }
        }
    }
}
