using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ¿‡À∆µ–»À—™ÃıUI
/// </summary>
public class HpUI : MonoBehaviour
{
    Slider hp;
    PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        hp = GetComponent<Slider>();
        playerController = transform.Find("../..").gameObject.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        hp.value = playerController.curHp / playerController.maxHp;
    }
}
