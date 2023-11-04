using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimSystem : MonoBehaviour
{
    public static AimSystem instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public GameObject currentWeapon;
    Vector3 mouseScreenPos = Vector3.zero;
    Vector3 aimDir = Vector3.zero;
    Camera cam;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        spriteRenderer = this.gameObject.transform.parent.GetComponent<SpriteRenderer>();
        currentWeapon = this.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Aim();
    }

    void Aim()
    {
        mouseScreenPos = Input.mousePosition;
        aimDir = mouseScreenPos - cam.WorldToScreenPoint(currentWeapon.transform.position);
        aimDir.z = 0;
        aimDir.Normalize();
        float angle = Mathf.Atan2(aimDir.y, aimDir.x)*Mathf.Rad2Deg;
        currentWeapon.transform.eulerAngles = new Vector3(0, 0, angle);
    }

    //¸üÐÂÎäÆ÷
    public static void UpdateWeapon(WeaponData weaponData)
    {
        instance.currentWeapon = Instantiate(weaponData.weaponPrefab, instance.gameObject.transform);
    }
}
