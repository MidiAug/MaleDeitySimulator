using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropitem : MonoBehaviour
{
    Item item;
    public static Dropitem createitem(Vector2 position, Item item)
    {
        GameObject newDropitem = Instantiate(Itemassets.Instance.DropitemPrefab,position,Quaternion.identity);
        //设置物品信息

        Rigidbody2D rg = newDropitem.GetComponent<Rigidbody2D>();
        rg.velocity = (position.normalized) * 4.0f;
        Dropitem dropitem = newDropitem.GetComponent<Dropitem>();
        dropitem.Setitem(item);
        return dropitem;
    }
    private void Setitem(Item item)
    {
        this.item = item;
    }
}
