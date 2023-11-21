using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventorymanager : MonoBehaviour
{
    //背包管理器
    public static Inventorymanager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    private Inventory playerinventory;

    private void start()
    {
        Debug.Log(playerinventory.GetItemList().Count);
    }


    //设置玩家背包
    public void Setplayerinventory(Inventory inventory)
    {
        playerinventory = inventory;
    }

}
