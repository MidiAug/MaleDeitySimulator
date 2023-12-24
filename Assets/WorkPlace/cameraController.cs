using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    // ����
    public Vector3 positionOffset;    // ��������Ŀ���ƫ��λ��
    public Transform target;          // Ŀ������ Transform���������Ҫ����Ķ���
    public Vector2 minBounds;         // ����ƶ���Χ����С�߽�
    public Vector2 maxBounds;         // ����ƶ���Χ�����߽�

    [Range(0, 1)]                     // ƽ�����ɵ�ʱ�䣬�趨��Χ�� [0, 1]
    public float smoothTime;

    private void Update()
    {
        // �� Update ���������������߼������紦����ҵ�����
        // ���磬���������������ҵ��������ƶ�Ŀ�����
        // ʾ���� target.Translate(new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * Time.deltaTime);
    }

    // LateUpdate �� Update ֮�󱻵��ã����ڴ����������
    void LateUpdate()
    {
        if (target != null)
        {
            // ����Ŀ��λ�ò�����ƫ��
            Vector3 targetPosition = target.position + positionOffset;

            // ��������ƶ���Χ
            targetPosition.x = Mathf.Clamp(targetPosition.x, minBounds.x + positionOffset.x, maxBounds.x + positionOffset.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minBounds.y + positionOffset.y, maxBounds.y + positionOffset.y);

            // ʹ�� Vector3.Lerp ʵ��ƽ�����ɣ�ʹ���������Ŀ�����
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothTime);
        }
    }
}

