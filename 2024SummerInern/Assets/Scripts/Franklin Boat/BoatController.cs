using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour
{


    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;

    public float maxSpeed = 20.0f;          // 最大速度
    public float maxAngularVelocity = 10.0f; // 最大角速度

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        //// 获取输入
        //float moveForward = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        //float rotate = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;

        //// 前后移动（沿X轴方向）
        //Vector3 movement = transform.right * moveForward*-1;
        //rb.MovePosition(rb.position + movement);

        //// 左右旋转（绕Y轴）
        //Quaternion turn = Quaternion.Euler(0f, rotate, 0f);
        //rb.MoveRotation(rb.rotation * turn);



        // 获取输入
        float moveForward = Input.GetAxis("Vertical");
        float rotate = Input.GetAxis("Horizontal");

        // 前后移动
        Vector3 movement = transform.right * -1 * moveForward * speed * Time.deltaTime;
        rb.AddForce(movement, ForceMode.VelocityChange);

        // 左右旋转
        Vector3 rotation = Vector3.up * rotate * rotationSpeed * Time.deltaTime;
        rb.AddTorque(rotation, ForceMode.VelocityChange);

        // 限制最大速度
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }


    //public float moveSpeed = 10.0f;          // 移动速度
    //public float rotationSpeed = 100.0f;     // 旋转速度
    //public float maxSpeed = 20.0f;           // 最大速度
    //public float drag = 1.0f;                // 阻力
    //public float angularDrag = 1.0f;         // 角阻力

    //private Rigidbody rb;

    //void Start()
    //{
    //    rb = GetComponent<Rigidbody>();
    //    rb.drag = drag;                 // 设置阻力
    //    rb.angularDrag = angularDrag;   // 设置角阻力
    //}

    //void FixedUpdate()
    //{
    //    // 获取输入
    //    float moveVertical = Input.GetAxis("Vertical");   // W和S控制前后移动
    //    float moveHorizontal = Input.GetAxis("Horizontal"); // A和D控制左右移动

    //    // 计算移动方向
    //    Vector3 moveDirection = transform.forward * moveVertical * -1 * moveSpeed * Time.deltaTime;
    //    Vector3 strafeDirection = transform.right * moveHorizontal * -1 * moveSpeed * Time.deltaTime;

    //    // 应用移动力
    //    rb.AddForce(moveDirection + strafeDirection, ForceMode.VelocityChange);

    //    // 限制最大速度
    //    if (rb.velocity.magnitude > maxSpeed)
    //    {
    //        rb.velocity = rb.velocity.normalized * maxSpeed;
    //    }
    //}










}



