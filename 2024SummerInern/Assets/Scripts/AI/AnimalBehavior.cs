using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // 导航组件

public class AnimalBehavior : MonoBehaviour
{
    public GameObject[] foodObjects;   // 食物对象数组
    public GameObject[] trashObjects;  // 垃圾对象数组
    public float stuckTime = 5f;       // 卡壳时间

    public GameObject target;         // 当前目标
    private NavMeshAgent agent;        // NavMesh代理
    private bool isEating = false;     // 是否正在吃

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        ChooseTarget();
    }

    void Update()
    {
        if (target && !isEating && Vector3.Distance(transform.position, target.transform.position) < 1f)
        {
            StartCoroutine(Eat());
        }
    }

    void ChooseTarget()
    {
        GameObject[] targetArray = (Random.value > 0.5f) ? foodObjects : trashObjects;
        float closestDistance = float.MaxValue;

        foreach (GameObject obj in targetArray)
        {
            float distance = Vector3.Distance(transform.position, obj.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                target = obj;
            }
        }

        agent.SetDestination(target.transform.position); // 设置目标位置
    }

    IEnumerator Eat()
    {
        isEating = true;
        float eatTime = (target.tag == "Food") ? 3f : 7f; // 不同的吃的时间
        yield return new WaitForSeconds(eatTime); // 吃

        if (target.tag == "Trash")
        {
            StartCoroutine(StuckTimer());
        }
        else
        {
            ChooseTarget(); // 寻找新的食物或随机游动
        }

        Destroy(target); // 销毁食物或垃圾
        isEating = false;
    }

    IEnumerator StuckTimer()
    {
        yield return new WaitForSeconds(stuckTime);

        Debug.Log("Animal died");
        Destroy(gameObject); // 动物死亡
    }
}
