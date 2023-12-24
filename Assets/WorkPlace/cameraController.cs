using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    // 属性
    public Vector3 positionOffset;    // 相机相对于目标的偏移位置
    public Transform target;          // 目标对象的 Transform，即摄像机要跟随的对象
    public Vector2 minBounds;         // 相机移动范围的最小边界
    public Vector2 maxBounds;         // 相机移动范围的最大边界

    [Range(0, 1)]                     // 平滑过渡的时间，设定范围在 [0, 1]
    public float smoothTime;

    private void Update()
    {
        // 在 Update 中添加你的输入检测逻辑，例如处理玩家的输入
        // 例如，你可以在这里检测玩家的输入来移动目标对象
        // 示例： target.Translate(new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * Time.deltaTime);
    }

    // LateUpdate 在 Update 之后被调用，用于处理相机跟随
    void LateUpdate()
    {
        if (target != null)
        {
            // 计算目标位置并加上偏移
            Vector3 targetPosition = target.position + positionOffset;

            // 限制相机移动范围
            targetPosition.x = Mathf.Clamp(targetPosition.x, minBounds.x + positionOffset.x, maxBounds.x + positionOffset.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minBounds.y + positionOffset.y, maxBounds.y + positionOffset.y);

            // 使用 Vector3.Lerp 实现平滑过渡，使摄像机跟随目标对象
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothTime);
        }
    }
}

