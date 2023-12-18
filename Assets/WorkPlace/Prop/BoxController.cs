using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    public PropList propList;

    private PropData propData;
    private void Awake()
    {
        propList = Resources.Load<PropList>(typeof(PropList).Name);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject.CompareTag("Player"))
        {
            open();
        }
    }
    private void open()
    {
        float totalWeight = 0f;
        foreach (PropData tmp in propList.list) {
            totalWeight += tmp.weight;
        }

        float randomValue = Random.value * totalWeight;
        foreach (PropData tmp in propList.list)
        {
            randomValue -= tmp.weight;
            if (randomValue <= 0f)
            {
                //�������´���ʹ�ý���proplist���ñ���ϵͳ�ĵ��������ɺ������ɵ�����
                Item item = new Item { itemType = 0, Itemamount = 1, Itemname = "0" };
                switch (tmp.name)//��ȡ��ǰ�б����Ʒ��
                {
                    default:
                    case "bloodpacks": item.itemType = Item.ItemType.bloodpacks; item.Itemname = "bloodpacks"; break;
                    case "silverCoin": item.itemType = Item.ItemType.silverCoin; item.Itemname = "silverCoin"; break;
                    case "goldCoin": item.itemType = Item.ItemType.goldCoin; item.Itemname = "goldCoin"; break;
                    case "copperCoin": item.itemType = Item.ItemType.copperCoin; item.Itemname = "copperCoin"; break;
                }//�����б��е���Ʒ�����ڵ�ǰ��Ʒ��ʱ����ȨֵС�������ж����������Ʒ��Ȼ�����õ���������ƣ�����
                Dropitem.Createitem(transform.position, item, false);
                //Instantiate(tmp.prefab, transform.position, Quaternion.identity);ԭ����
                Destroy(gameObject);
                break;
            }
        }
    }
}
