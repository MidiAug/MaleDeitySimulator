using UnityEngine;
using UnityEngine.UI;

public class bottonController : MonoBehaviour
{
    public Item item;
    public void onclick()
    {
        //item = Drop.Instance.item;

        if(gameObject.activeSelf == false)
        {
           gameObject.SetActive(true);
        }
        else
        {
           gameObject.SetActive(false);
        }
        // ÇÐ»»BackgroundµÄActive×´Ì¬£¨ÏÔÊ¾»òÒþ²Ø£©
    }
}