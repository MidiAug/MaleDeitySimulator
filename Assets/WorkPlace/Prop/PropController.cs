using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 拾取生命值增加物品
/// </summary>
public class PropController : MonoBehaviour
{
    public PropData propData;
    PlayerController playerController;

    private bool KeyCode_F;// F 键拾取
    public int coinNum = 0;
    private GameObject tar;
    private RectTransform target;//指向要移动的目的

    PickUoCoinAnimation pickUpAnimation;
    // Start is called before the first frame update
    void Start()
    {
        tar = GameObject.Find("Coin").transform.GetChild(0).gameObject;
        target = tar.GetComponent<RectTransform>();

        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        pickUpAnimation = GetComponent<PickUoCoinAnimation>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (KeyCode_F && Input.GetKeyUp(KeyCode.F) && propData.type == "BloodPacks") 
        {
            PickUpBloodPacks();// F键吃血包血包
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (playerController != null && other.CompareTag("Player"))
        {
            KeyCode_F = true;
            if (propData.type == "Coin") PickUpCoin();
        }



    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (playerController != null && other.CompareTag("Player"))
        {
            KeyCode_F = false;
        }

    }

    void PickUpBloodPacks()
    {
        playerController.InHealth(propData.val);
        Destroy(gameObject);
    }
    void PickUpCoin()
    {
        // 拾取金币
        //pickUpAnimation.Play(target);
        Debug.Log(propData.val);
        coinNum += (int)propData.val;
        GameObject.Find("Coin").transform.GetChild(1).GetComponent<Text>().text = coinNum.ToString();
        Destroy(gameObject);
    }
}