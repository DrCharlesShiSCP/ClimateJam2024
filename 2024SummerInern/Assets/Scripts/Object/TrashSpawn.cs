using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSpawn : MonoBehaviour
{
    public GameObject[] prefabsToSpawn; // 要生成的不同种类的Prefab数组
    public float spawnCooldown = 5f; // 生成冷却时间（秒）
    public float spawnRadius = 5f; // 生成半径
    public float minSpeed = 5f; // 最小速度
    public float maxSpeed = 10f; // 最大速度

    private bool spawningAllowed = true; // 是否允许生成

    void Start()
    {
        StartCoroutine(SpawnObjectsRoutine());
    }

    IEnumerator SpawnObjectsRoutine()
    {
        while (true) // 无限循环，直到游戏结束或其他条件
        {
            if (spawningAllowed)
            {
                int prefabIndex = Random.Range(0, prefabsToSpawn.Length); // 随机选择一个Prefab类型
                GameObject prefabToSpawn = prefabsToSpawn[prefabIndex];

                int count = Random.Range(1, 5); // 随机生成1到4个物体
                for (int i = 0; i < count; i++)
                {
                    float angle = Random.Range(0f, 360f); // 随机角度
                    float speed = Random.Range(minSpeed, maxSpeed); // 随机速度

                    Vector3 spawnPosition = transform.position + Quaternion.Euler(0f, angle, 0f) * Vector3.forward * Random.Range(0f, spawnRadius); // 随机生成位置
                    GameObject obj = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity); // 生成Prefab

                    // 设置初速度
                    Rigidbody rigidbody = obj.GetComponent<Rigidbody>();
                    if (rigidbody != null)
                    {
                        rigidbody.velocity = Quaternion.Euler(0f, angle, 0f) * Vector3.forward * speed;
                    }
                }

                spawningAllowed = false; // 禁止生成
                yield return new WaitForSeconds(spawnCooldown); // 等待冷却时间
                spawningAllowed = true; // 允许生成
            }
            yield return null; // 等待一帧
        }
    }
}

