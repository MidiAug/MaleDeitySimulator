using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//���ڹ�����Ʒ��ͼƬ
public class Itemassets : MonoBehaviour
{
    public static Itemassets Instance { get; set; }
    private void Awake()
    {
        Instance=this;
    }//ȷ��ֻ����һ��ʵ������֮��ͬ���ظ����ɡ�

    //public Sprite Goldcoinsprite;//ͭ��ͼƬ
    public Sprite Bloodpacksprite;//ѪƿͼƬ
    public Sprite Damagepacksprite;//ѪƿͼƬ
    //public Sprite Coopercoinprite;//ͭ��ͼƬ
    //public Sprite slivercoinprite;//����ͼƬ
    public GameObject DropitemPrefab;//���������
}
