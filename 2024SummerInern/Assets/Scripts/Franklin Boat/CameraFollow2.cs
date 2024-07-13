using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2 : MonoBehaviour
{
    public Transform target;  // Ŀ�괬ֻ
    public float followSpeed = 2f;  // ����ͷ������ٶ�

    private Vector3 offset;


    void Start()
    {   
        target = GameObject.FindWithTag("Player").GetComponent<Transform>();
        if (target == null)
        {
            Debug.LogError("δ����Ŀ�괬ֻ��");
            return;
        }

        // ��¼��ʼ��ƫ����
        offset = transform.position - target.position;
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // ����Ŀ��λ��
            Vector3 targetPosition = target.position + offset;

            // ʹ�����Բ�ֵʹ����ͷƽ�����洬ֻ
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
    }
}
