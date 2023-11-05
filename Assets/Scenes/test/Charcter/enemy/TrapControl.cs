using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class TrapControl : MonoBehaviour
{
    PlayerControl player;
    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag( "Player")&&collision.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            player.Attacked(30);
        }
    }

}
