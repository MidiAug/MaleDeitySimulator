using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ʰȡ����ֵ������Ʒ
/// </summary>
public class PropController : MonoBehaviour
{
    public PropData propData;
    PlayerController playerController;


    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    // Update is called once per frame
    void OnTriggerStay2D(Collider2D other)
    {
        if (playerController != null && other.CompareTag("Player"))
        {
            // ����ʰȡʧ�ܣ�������ײʱ������λ��false
            // if (propData.type == "BloodPacks"&&Input.GetKeyUp(KeyCode.F)) BloodPacks();
            
            if (propData.type == "BloodPacks") BloodPacks();


        }

    }
    void BloodPacks()
    {
        playerController.InHealth(propData.val);
        Destroy(gameObject);
    }
}