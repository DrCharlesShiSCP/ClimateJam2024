using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject prefabToSpawn; // Ҫ���ɵ�Prefab
    public float spawnCooldown = 5f; // ������ȴʱ�䣨�룩

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
                int count = Random.Range(1, 5); // �������1��4������
                for (int i = 0; i < count; i++)
                {
                    Vector3 spawnPosition = new Vector3(Random.Range(-5f, 5f), 0f, Random.Range(-5f, 5f)); // �������λ��
                    Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity); // ����Prefab
                }

                spawningAllowed = false; // ��ֹ����
                yield return new WaitForSeconds(spawnCooldown); // �ȴ���ȴʱ��
                spawningAllowed = true; // ��������
            }
            yield return null; // �ȴ�һ֡
        }
    }
}
