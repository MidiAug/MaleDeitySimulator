using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.UI;

public class HpUI : MonoBehaviour
{
    PlayerControl player;
    Slider hp;
    // Start is called before the first frame update
    void Start()
    {
        hp = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        hp.value = GameObject.FindWithTag("Player").GetComponent<PlayerControl>().hp / 100f;
    }
}
