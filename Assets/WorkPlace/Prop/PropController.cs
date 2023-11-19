using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 拾取生命值增加物品
/// </summary>
public class PropController : MonoBehaviour
{
    public PropData propData;
    PlayerController playerController;


    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    // Update is called once per frame
    void OnTriggerStay2D(Collider2D other)
    {
        if (playerController != null && other.CompareTag("Player"))
        {
            // 按键拾取失败，触发碰撞时往往键位是false
            // if (propData.type == "BloodPacks"&&Input.GetKeyUp(KeyCode.F)) BloodPacks();
            
            if (propData.type == "BloodPacks") BloodPacks();


        }

    }
    void BloodPacks()
    {
        playerController.InHealth(propData.val);
        Destroy(gameObject);
    }
}