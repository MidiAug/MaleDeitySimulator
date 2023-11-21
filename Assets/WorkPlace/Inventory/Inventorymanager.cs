using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventorymanager : MonoBehaviour
{
    //����������
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


    //������ұ���
    public void Setplayerinventory(Inventory inventory)
    {
        playerinventory = inventory;
    }

}
