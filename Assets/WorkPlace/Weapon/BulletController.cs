using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    GameObject curWeapon;

    private float damage;
    public float deadTime = 0;

    private void Start()
    {
        curWeapon = GameObject.FindGameObjectWithTag("Weapon").transform.GetChild(0).gameObject;
        damage = curWeapon.GetComponent<VisitWeapon>().weaponData.damage;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Build")
            Destroy(gameObject, deadTime);
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyController>().Attacked(damage);
            Destroy(gameObject, deadTime);
        }
    }
}
