using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Dropitem : MonoBehaviour
{
    public Item item;
   
    public static Dropitem Createitem(Vector2 position, Item item, bool Isdrop)//�Ƿ�ͨ����������ʽ����������ѵ�����Ҳ�������������
    {
        Vector2 Dropdir = Vector2.zero; 
        GameObject newDropitem;
        if(Isdrop)
        {
            Dropdir = new Vector2(Random.Range(1.0f, -1.0f), Random.Range(1.0f, -1.0f));
            newDropitem = Instantiate(Itemassets.Instance.DropitemPrefab, position, Quaternion.identity);
            Rigidbody2D rg = newDropitem.GetComponent<Rigidbody2D>(); 
            rg.velocity = (Dropdir.normalized)* 2.5f;
        }//����Ƕ����ﻹҪ��һ���ٶȣ����ƶ�������
        else
        {
            newDropitem = Instantiate(Itemassets.Instance.DropitemPrefab,position,Quaternion.identity);
        }//ֱ���ڱ���ԭ������
        Dropitem dropitem = newDropitem.GetComponent<Dropitem>();//�Ա߿����������ɵ�����ʵ��
        dropitem.Setitem(item);
        return dropitem;
    }

    private SpriteRenderer itemspriteRenderer;
    private TextMeshProUGUI itemamount;
    private void Awake()
    {
        itemspriteRenderer = this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        itemamount = this.gameObject.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
    }//���ڿ��Ƶ������ͼƬ�����ݣ�������

    private void Setitem(Item item)
    {
        this.item = item;
        itemspriteRenderer.sprite = this.item.Getitemsprite();
    }
}
