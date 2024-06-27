using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatStabilizer : MonoBehaviour
{
    public float recoverySpeed = 2f;  // 控制翻正速度的变量

    private void Update()
    {
        // 获取船的Z轴的旋转角度
        float zRotation = transform.eulerAngles.z;

        // 如果Z轴旋转角度超出允许范围
        if (zRotation > 90f && zRotation < 270f)
        {
            // 使用Slerp平滑地将旋转角度调整到正确的方向
            Quaternion targetRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * recoverySpeed);
        }
    }
}
