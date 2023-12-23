using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class CoincCtrl : MonoBehaviour
{
    public PropData propData;
    GameObject player;
    float moveSpeed = 12;
    PlayerController playerController;
    private Audio audio;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        audio = GameObject.FindGameObjectWithTag("Audio").GetComponent<Audio>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    private void Move()
    {
        if (player != null)
        {
            //获取一个指向玩家的向量
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);

            // 根据玩家与敌人的x轴坐标，翻转图像
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (player != null)
        {
            if (collision != null && collision.gameObject.CompareTag("Player"))
            {
                playerController.coinNum += (int)propData.val;
                GameObject.Find("GameUI").transform.GetChild(0).GetChild(1).GetComponent<Text>().text = playerController.coinNum.ToString();
                audio.PlaySFX(audio.pickUpCoin);
                Destroy(gameObject);
            }
        }
    }

}
