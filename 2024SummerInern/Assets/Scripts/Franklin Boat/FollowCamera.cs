using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;  // 船只的Transform
    private Vector3 initialOffset;

    private void Start()
    {
        // 在游戏开始时计算和存储船与摄像机之间的初始偏移
        initialOffset = transform.position - target.position;
    }

    private void LateUpdate()
    {
        // 保持初始的偏移量跟随船只
        transform.position = target.position + target.rotation * initialOffset;

        // 使摄像机看向船的位置
        transform.LookAt(target.position);
    }
}
