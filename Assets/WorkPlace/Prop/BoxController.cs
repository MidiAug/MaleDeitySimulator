using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    PropList propList;

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
                Instantiate(tmp.prefab, transform.position, Quaternion.identity);
                Destroy(gameObject);
                break;
            }
        }

    }
}
