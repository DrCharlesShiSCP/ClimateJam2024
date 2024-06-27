using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatWobble : MonoBehaviour
{
    public float wobbleSpeedX = 1.0f;  // 控制X轴摇摆速度
    public float wobbleAmountX = 5.0f; // 控制X轴摇摆幅度
    public float wobbleSpeedZ = 1.0f;  // 控制Z轴摇摆速度
    public float wobbleAmountZ = 5.0f; // 控制Z轴摇摆幅度

    private float originalXRotation;
    private float originalZRotation;

    void Start()
    {
        if (transform.childCount > 0)
        {
            Transform wobbleChild = transform.GetChild(0);
            originalXRotation = wobbleChild.localEulerAngles.x;
            originalZRotation = wobbleChild.localEulerAngles.z;
        }
        else
        {
            Debug.LogError("BoatWobble script requires a child object to apply the wobble effect.");
        }
    }

    void Update()
    {
        if (transform.childCount > 0)
        {
            float wobbleAngleX = Mathf.Sin(Time.time * wobbleSpeedX) * wobbleAmountX;
            float wobbleAngleZ = Mathf.Sin(Time.time * wobbleSpeedZ) * wobbleAmountZ;
            Transform wobbleChild = transform.GetChild(0);
            Vector3 currentRotation = wobbleChild.localEulerAngles;
            currentRotation.x = originalXRotation + wobbleAngleX;
            currentRotation.z = originalZRotation + wobbleAngleZ;
            wobbleChild.localEulerAngles = currentRotation;
        }
    }
}
