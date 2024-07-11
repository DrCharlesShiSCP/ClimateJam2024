using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject prefabToSpawn; // 要生成的Prefab
    public float spawnCooldown = 5f; // 生成冷却时间（秒）

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
                int count = Random.Range(1, 5); // 随机生成1到4个物体
                for (int i = 0; i < count; i++)
                {
                    Vector3 spawnPosition = new Vector3(Random.Range(-5f, 5f), 0f, Random.Range(-5f, 5f)); // 随机生成位置
                    Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity); // 生成Prefab
                }

                spawningAllowed = false; // 禁止生成
                yield return new WaitForSeconds(spawnCooldown); // 等待冷却时间
                spawningAllowed = true; // 允许生成
            }
            yield return null; // 等待一帧
        }
    }
}
