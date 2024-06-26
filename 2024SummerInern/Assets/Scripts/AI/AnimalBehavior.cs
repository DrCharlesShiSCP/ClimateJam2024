using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // �������

public class AnimalBehavior : MonoBehaviour
{
    public GameObject[] foodObjects;   // ʳ���������
    public GameObject[] trashObjects;  // ������������
    public float stuckTime = 5f;       // ����ʱ��

    public GameObject target;         // ��ǰĿ��
    private NavMeshAgent agent;        // NavMesh����
    private bool isEating = false;     // �Ƿ����ڳ�

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

        agent.SetDestination(target.transform.position); // ����Ŀ��λ��
    }

    IEnumerator Eat()
    {
        isEating = true;
        float eatTime = (target.tag == "Food") ? 3f : 7f; // ��ͬ�ĳԵ�ʱ��
        yield return new WaitForSeconds(eatTime); // ��

        if (target.tag == "Trash")
        {
            StartCoroutine(StuckTimer());
        }
        else
        {
            ChooseTarget(); // Ѱ���µ�ʳ�������ζ�
        }

        Destroy(target); // ����ʳ�������
        isEating = false;
    }

    IEnumerator StuckTimer()
    {
        yield return new WaitForSeconds(stuckTime);

        Debug.Log("Animal died");
        Destroy(gameObject); // ��������
    }
}
