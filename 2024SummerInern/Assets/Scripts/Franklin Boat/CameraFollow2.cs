using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2 : MonoBehaviour
{
    public Transform target;  // 目标船只
    public float followSpeed = 2f;  // 摄像头跟随的速度

    private Vector3 offset;


    void Start()
    {   
        target = GameObject.FindWithTag("Player").GetComponent<Transform>();
        if (target == null)
        {
            Debug.LogError("未设置目标船只！");
            return;
        }

        // 记录初始的偏移量
        offset = transform.position - target.position;
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // 计算目标位置
            Vector3 targetPosition = target.position + offset;

            // 使用线性插值使摄像头平滑跟随船只
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
    }
}
