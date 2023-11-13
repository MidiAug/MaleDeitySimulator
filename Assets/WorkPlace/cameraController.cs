using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    //����
    public Vector3 positionOffset;    // ��������Ŀ���ƫ��λ��
    public Transform target;          // Ŀ������Transform���������Ҫ����Ķ���

    [Range(0, 1)]                     // ƽ�����ɵ�ʱ�䣬�趨��Χ�� [0, 1]
    public float smoothTime;

    private void Update()
    {
        
    }

    // LateUpdate �� Update ֮�󱻵��ã����ڴ����������
    void LateUpdate()
    {
        if (target != null)
        {
            // �������λ����Ŀ��λ�ò�ͬ�����ƶ�
            if (transform.position != target.position)
            {
                // ����Ŀ��λ�ò�����ƫ��
                Vector3 targetPosition = target.position + positionOffset;

                // ʹ�� Vector3.Lerp ʵ��ƽ�����ɣ�ʹ���������Ŀ�����
                transform.position = Vector3.Lerp(transform.position, targetPosition, smoothTime);
            }
        }
    }
}

