using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Itemassets : MonoBehaviour
{
    public static Itemassets Instance { get; private set; }
    private void Awake()
    {
        Instance=this;
    }
    public Sprite Goldcoinsprite;
    public Sprite Bloodpacksprite;
    public Sprite Coopercoinprite;
    public Sprite slivercoinprite;

    public GameObject DropitemPrefab;
}
