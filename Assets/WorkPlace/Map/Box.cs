using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public GameObject[] gos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GetCoin()
    {
        //1.获取随机游戏对象
        //2.生成这个游戏对象
        //3.删除当前箱子
        Vector3 pos = transform.position;
        Instantiate(gos[Random.Range(0, gos.Length)], pos, Quaternion.identity);
        Destroy(gameObject);
    }
}
