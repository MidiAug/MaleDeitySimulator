using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    //属性
    public Vector3 positionOffset;    // 相机相对于目标的偏移位置
    public Transform target;          // 目标对象的Transform，即摄像机要跟随的对象

    [Range(0, 1)]                     // 平滑过渡的时间，设定范围在 [0, 1]
    public float smoothTime;

    private void Update()
    {
        
    }

    // LateUpdate 在 Update 之后被调用，用于处理相机跟随
    void LateUpdate()
    {
        if (target != null)
        {
            // 若摄像机位置与目标位置不同，则移动
            if (transform.position != target.position)
            {
                // 计算目标位置并加上偏移
                Vector3 targetPosition = target.position + positionOffset;

                // 使用 Vector3.Lerp 实现平滑过渡，使摄像机跟随目标对象
                transform.position = Vector3.Lerp(transform.position, targetPosition, smoothTime);
            }
        }
    }
}

