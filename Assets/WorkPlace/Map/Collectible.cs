using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 拾取生命值增加物品
/// </summary>
public class Collectible : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController pc = other.GetComponent<PlayerController>();
        if (pc != null)
        {
            if (pc.MyCurrentHealth < pc.MyMaxHealth)
            {
                pc.ChangeHealth(5);
                Destroy(this.gameObject);
            }

        }

    }
}