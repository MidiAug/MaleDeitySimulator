using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Dropitem : MonoBehaviour
{
    Item item;
    public static Dropitem Createitem(Vector2 position, Item item)
    {
        GameObject newDropitem = Instantiate(Itemassets.Instance.DropitemPrefab,position,Quaternion.identity);
        //设置物品信息
        Rigidbody2D rg = newDropitem.GetComponent<Rigidbody2D>();
        rg.velocity = (position.normalized) * 4.0f;
        Dropitem dropitem = newDropitem.GetComponent<Dropitem>();
        dropitem.Setitem(item);
        return dropitem;
    }

    private SpriteRenderer itemspriteRenderer;

    private TextMeshProUGUI itemamount;
    private void Awake()
    {
        itemspriteRenderer = this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        itemamount = this.gameObject.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    private void Setitem(Item item)
    {
        this.item = item;
        itemspriteRenderer.sprite = this.item.Getitemsprite();
        if(this.item.Itemamount>1)
        {
            itemamount.SetText(this.item.Itemamount.ToString());
        }

        Inventorymanager.Instance.Refreshinventoryui();
    }
}
