using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour
{


    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // 获取输入
        float moveForward = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float rotate = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;

        // 前后移动（沿X轴方向）
        Vector3 movement = transform.right * moveForward*-1;
        rb.MovePosition(rb.position + movement);

        // 左右旋转（绕Y轴）
        Quaternion turn = Quaternion.Euler(0f, rotate, 0f);
        rb.MoveRotation(rb.rotation * turn);
    }
}


