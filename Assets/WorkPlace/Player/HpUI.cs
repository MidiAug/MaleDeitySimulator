using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.UI;

public class HpUI : MonoBehaviour
{
    Slider hp;
    // Start is called before the first frame update
    void Start()
    {
        hp = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        hp.value = transform.Find("../..").gameObject.GetComponent<PlayerController>().hp / 100f;
    }
}
