using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSpawn : MonoBehaviour
{
    public GameObject[] prefabsToSpawn; // Ҫ���ɵĲ�ͬ�����Prefab����
    public float spawnCooldown = 5f; // ������ȴʱ�䣨�룩
    public float spawnRadius = 5f; // ���ɰ뾶
    public float minSpeed = 5f; // ��С�ٶ�
    public float maxSpeed = 10f; // ����ٶ�

    private bool spawningAllowed = true; // �Ƿ���������

    void Start()
    {
        StartCoroutine(SpawnObjectsRoutine());
    }

    IEnumerator SpawnObjectsRoutine()
    {
        while (true) // ����ѭ����ֱ����Ϸ��������������
        {
            if (spawningAllowed)
            {
                int prefabIndex = Random.Range(0, prefabsToSpawn.Length); // ���ѡ��һ��Prefab����
                GameObject prefabToSpawn = prefabsToSpawn[prefabIndex];

                int count = Random.Range(1, 5); // �������1��4������
                for (int i = 0; i < count; i++)
                {
                    float angle = Random.Range(0f, 360f); // ����Ƕ�
                    float speed = Random.Range(minSpeed, maxSpeed); // ����ٶ�

                    Vector3 spawnPosition = transform.position + Quaternion.Euler(0f, angle, 0f) * Vector3.forward * Random.Range(0f, spawnRadius); // �������λ��
                    GameObject obj = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity); // ����Prefab

                    // ���ó��ٶ�
                    Rigidbody rigidbody = obj.GetComponent<Rigidbody>();
                    if (rigidbody != null)
                    {
                        rigidbody.velocity = Quaternion.Euler(0f, angle, 0f) * Vector3.forward * speed;
                    }
                }

                spawningAllowed = false; // ��ֹ����
                yield return new WaitForSeconds(spawnCooldown); // �ȴ���ȴʱ��
                spawningAllowed = true; // ��������
            }
            yield return null; // �ȴ�һ֡
        }
    }
}

