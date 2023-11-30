using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Drop : MonoBehaviour,IPointerClickHandler
{
    public Item item;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            //¶ªÆúÎïÆ·
            Dropitem.Createitem((Vector2)Inventorymanager.Instance.Getplayer().transform.position, item);
            if (item.Itemamount > 1)
            {
                item.Itemamount--;
            }
            else
            {
                
            }
            Inventorymanager.Instance.Refreshinventoryui();
        }
    }
}
