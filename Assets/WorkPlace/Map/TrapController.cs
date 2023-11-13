using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    // ÕÊº“∂‘œÛ
    PlayerController player;

    // …À∫¶
    public float damage;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player")&&collision.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            player.Attacked(damage);
        }
    }

}
