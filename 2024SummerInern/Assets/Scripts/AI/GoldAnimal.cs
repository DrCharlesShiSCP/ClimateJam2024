using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // �������
using TMPro;

public class GoldAnimal : MonoBehaviour
{
    public enum AnimalState
    {
        SearchingFood,
        ChasingFood,
        Eating,
        Roaming,
        Dead,

        GoingToTarget
    }

    public float eatingDuration = 5.0f; // Eating duration in seconds
    public float moveSpeed = 5.0f; // Speed of animal movement
    public float deathTimerDuration = 10.0f; // Death timer duration in seconds
    public float detectionRange = 10.0f;

    public GameObject targetObject; // Target object to go to

    [Header("AnimalState")]
    public AnimalState currentState;
    public float eatingTimer;
    public float deathTimer;
    public float goToTargetTimer;// Go to target duration in seconds (5 minutes)


    public GameObject currentTarget;

    public bool CanSave;
    public bool CanEat;
    public GameObject HelpSign;

    public PickUpTrash player;

    [Header("Die")]

    public GameObject GameEnd;
    void Start()
    {
        SetState(AnimalState.SearchingFood);
        CanSave = false;
        CanEat = false;
        HelpSign.SetActive(false);

        player = GameObject.FindWithTag("Player").GetComponent<PickUpTrash>();

    }

    void Update()
    {

        //player.CanSaveFish = CanSave;

        switch (currentState)
        {
            case AnimalState.SearchingFood:
                // Implement searching food behavior
                GameObject[] foodObjects = GameObject.FindGameObjectsWithTag("Food");
                GameObject[] trashObjects = GameObject.FindGameObjectsWithTag("Trash");

                //if (foodObjects.Length >= 0 || trashObjects.Length >= 0)
                //{
                //    GameObject[] possibleTargets = new GameObject[foodObjects.Length + trashObjects.Length];
                //    foodObjects.CopyTo(possibleTargets, 0);
                //    trashObjects.CopyTo(possibleTargets, foodObjects.Length);

                //    currentTarget = possibleTargets[Random.Range(0, possibleTargets.Length)];

                //    SetState(AnimalState.ChasingFood);
                //}



                GameObject nearestTarget = GetNearestTarget(foodObjects, trashObjects);

                if (nearestTarget != null)
                {
                    currentTarget = nearestTarget;
                    SetState(AnimalState.ChasingFood);
                }
                break;




            case AnimalState.ChasingFood:
                // Implement chasing food behavior
                if (currentTarget != null)
                {
                    transform.position = Vector3.MoveTowards(transform.position, currentTarget.transform.position, moveSpeed * Time.deltaTime);

                    if (Vector3.Distance(transform.position, currentTarget.transform.position) <= 0f)
                    {

                        SetState(AnimalState.Eating);


                    }
                }
                if (currentTarget == null)
                {
                    SetState(AnimalState.SearchingFood);
                }

                break;




            case AnimalState.Eating:
                // Implement eating behavior
                eatingTimer -= Time.deltaTime;
                if (eatingTimer <= 0)
                {
                    if (currentTarget.CompareTag("Trash"))
                    {
                        SetState(AnimalState.Dead);
                    }
                    else
                    {
                        Destroy(currentTarget);
                        SetRandomState();
                    }
                }

                if (currentTarget == null)
                {
                    SetState(AnimalState.SearchingFood);
                }
                break;





            case AnimalState.Dead:
                // Implement death behavior
                deathTimer -= Time.deltaTime;
                if (deathTimer <= 0)
                {   
                    Destroy(gameObject); // Destroy the animal object
                    GameEnd.SetActive(true);
                    Time.timeScale = 0f;
                }
                if (deathTimer > 0)
                {
                    HelpSign.SetActive(true);


                    if (currentTarget == null)
                    {

                        //if (CanSave == true && Input.GetKeyDown(KeyCode.E))
                        //{
                        //    player.SaveAnimalNumber += 1;
                        //}
                        HelpSign.SetActive(false);
                        player.SaveAnimalNumber += 1;
                        SetRandomState();
                    }

                }
                break;




            case AnimalState.Roaming:
                // Implement roaming behavior
                if (Random.Range(0, 100) < 5) // 5% chance to switch to SearchingFood state
                {
                    SetState(AnimalState.SearchingFood);
                }
                break;


            case AnimalState.GoingToTarget:

                transform.position = Vector3.MoveTowards(transform.position, targetObject.transform.position, moveSpeed * Time.deltaTime);

                break;

        }

        //��ʼ����ʱ���ж��Ƿ�ǰ��Ŀ�ĵ�
        goToTargetTimer -= Time.deltaTime;
        if (goToTargetTimer <= 0)
        {
            SetState(AnimalState.GoingToTarget);
        }



    }

    void OnTriggerEnter(Collider other)
    {
        // Check if current state is Roaming and collided with food or trash object
        if (currentState == AnimalState.ChasingFood && (other.CompareTag("Food") || other.CompareTag("Trash")))
        {
            CanEat = true;
            SetState(AnimalState.Eating);
        }

        if (currentState == AnimalState.GoingToTarget && other.CompareTag("DeathTarget"))
        {
            Destroy(gameObject); // Destroy the animal object when reaching target
        }
        if (currentState == AnimalState.Dead && (other.CompareTag("Player")))
        {
            CanSave = true;
        }

    }

    void SetState(AnimalState newState)
    {
        currentState = newState;

        switch (currentState)
        {
            case AnimalState.SearchingFood:
                // Initialize searching food state
                break;
            case AnimalState.ChasingFood:
                // Initialize chasing food state
                break;
            case AnimalState.Eating:
                eatingTimer = eatingDuration;
                break;
            case AnimalState.Dead:
                deathTimer = deathTimerDuration;
                break;
            case AnimalState.Roaming:
                // Initialize roaming state
                break;
            case AnimalState.GoingToTarget:
                // Initialize going to target state
                break;
        }
    }

    void SetRandomState()
    {
        currentState = (AnimalState)Random.Range(0, 2); // Randomly select between SearchingFood and Roaming
    }

    GameObject GetNearestTarget(GameObject[] foodObjects, GameObject[] trashObjects)
    {
        GameObject nearestTarget = null;
        float nearestDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject food in foodObjects)
        {
            float distance = Vector3.Distance(currentPosition, food.transform.position);
            if (distance < nearestDistance)
            {
                nearestTarget = food;
                nearestDistance = distance;
            }
        }

        foreach (GameObject trash in trashObjects)
        {
            float distance = Vector3.Distance(currentPosition, trash.transform.position);
            if (distance < nearestDistance)
            {
                nearestTarget = trash;
                nearestDistance = distance;
            }
        }

        return nearestTarget;
    }

}
