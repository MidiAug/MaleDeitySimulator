using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//���ڹ�����Ʒ��ͼƬ
public class Itemassets : MonoBehaviour
{
    public static Itemassets Instance { get; set; }
    private void Awake()
    {
        Instance = this;
    }//ȷ��ֻ����һ��ʵ������֮��ͬ���ظ����ɡ�

    public Sprite Bloodpacksprite;//ѪƿͼƬ
    public Sprite Damagepacksprite;//ѪƿͼƬ
    public Sprite wudipacksprite;//�޵�ҩˮͼƬ
    public Sprite crytalpacksprite;//ˮ����ѪҩˮͼƬ
    public GameObject DropitemPrefab;//���������
}