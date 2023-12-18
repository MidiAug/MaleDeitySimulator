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
                //增加以下代码使得借助proplist调用背包系统的掉落物生成函数生成掉落物
                Item item = new Item { itemType = 0, Itemamount = 1, Itemname = "0" };
                switch (tmp.name)//获取当前列表的物品名
                {
                    default:
                    case "bloodpacks": item.itemType = Item.ItemType.bloodpacks; item.Itemname = "bloodpacks"; break;
                    case "silverCoin": item.itemType = Item.ItemType.silverCoin; item.Itemname = "silverCoin"; break;
                    case "goldCoin": item.itemType = Item.ItemType.goldCoin; item.Itemname = "goldCoin"; break;
                    case "copperCoin": item.itemType = Item.ItemType.copperCoin; item.Itemname = "copperCoin"; break;
                }//遍历列表中的物品，当在当前物品的时候总权值小于零则判定掉落这个物品，然后设置掉落物的名称，种类
                Dropitem.Createitem(transform.position, item, false);
                //Instantiate(tmp.prefab, transform.position, Quaternion.identity);原代码
                Destroy(gameObject);
                break;
            }
        }
    }
}
