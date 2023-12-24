using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Dropitem : MonoBehaviour
{
    public Item item;
   
    public static Dropitem Createitem(Vector2 position, Item item, bool Isdrop)//是否通过丢弃的形式创建，后面把掉落物也用这个函数生成
    {
        Vector2 Dropdir = Vector2.zero; 
        GameObject newDropitem;
        if(Isdrop)
        {
            Dropdir = new Vector2(Random.Range(1.0f, -1.0f), Random.Range(1.0f, -1.0f));
            newDropitem = Instantiate(Itemassets.Instance.DropitemPrefab, position, Quaternion.identity);
            Rigidbody2D rg = newDropitem.GetComponent<Rigidbody2D>(); 
            rg.velocity = (Dropdir.normalized)* 2.5f;
        }//如果是丢弃物还要给一个速度，完善丢弃过程
        else
        {
            newDropitem = Instantiate(Itemassets.Instance.DropitemPrefab,position,Quaternion.identity);
        }//直接在宝箱原地生成
        Dropitem dropitem = newDropitem.GetComponent<Dropitem>();//旁边控制栏中生成掉落物实例
        dropitem.Setitem(item);
        return dropitem;
    }

    private SpriteRenderer itemspriteRenderer;
    private TextMeshProUGUI itemamount;
    private void Awake()
    {
        itemspriteRenderer = this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        itemamount = this.gameObject.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
    }//用于控制掉落物的图片和内容（数量等

    private void Setitem(Item item)
    {
        this.item = item;
        itemspriteRenderer.sprite = this.item.Getitemsprite();
    }
}
