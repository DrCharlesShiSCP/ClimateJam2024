using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    public GameObject[] animalPrefabs;  // 动物Prefab数组
    public int numberToSpawn;           // 要生成的动物数量

    void Start()
    {
        SpawnAnimals();
    }

    void SpawnAnimals()
    {
        for (int i = 0; i < numberToSpawn; i++)
        {
            GameObject animal = animalPrefabs[Random.Range(0, animalPrefabs.Length)];
            Instantiate(animal, transform.position, Quaternion.identity); // 使用当前GameObject的位置
        }
    }
}
