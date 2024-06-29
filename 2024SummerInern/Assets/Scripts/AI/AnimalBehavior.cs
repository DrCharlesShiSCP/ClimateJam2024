using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // �������

public class AnimalBehavior : MonoBehaviour
{
    public enum AnimalState
    {
        SearchingFood,
        ChasingFood,
        Eating,
        Roaming,
        Dead
    }

    public float eatingDuration = 5.0f; // Eating duration in seconds
    public float moveSpeed = 5.0f; // Speed of animal movement
    public float deathTimerDuration = 10.0f; // Death timer duration in seconds

    public AnimalState currentState;
    public float eatingTimer;
    public float deathTimer;
    public GameObject currentTarget;

    void Start()
    {
        SetState(AnimalState.SearchingFood);
    }

    void Update()
    {
        switch (currentState)
        {
            case AnimalState.SearchingFood:
                // Implement searching food behavior
                GameObject[] foodObjects = GameObject.FindGameObjectsWithTag("Food");
                GameObject[] trashObjects = GameObject.FindGameObjectsWithTag("Trash");

                if (foodObjects.Length >= 0 || trashObjects.Length >= 0)
                {
                    GameObject[] possibleTargets = new GameObject[foodObjects.Length + trashObjects.Length];
                    foodObjects.CopyTo(possibleTargets, 0);
                    trashObjects.CopyTo(possibleTargets, foodObjects.Length);

                    currentTarget = possibleTargets[Random.Range(0, possibleTargets.Length)];

                    SetState(AnimalState.ChasingFood);
                }
                break;




            case AnimalState.ChasingFood:
                // Implement chasing food behavior
                if (currentTarget != null)
                {
                    transform.position = Vector3.MoveTowards(transform.position, currentTarget.transform.position, moveSpeed * Time.deltaTime);

                    if (Vector3.Distance(transform.position, currentTarget.transform.position) < 1.0f)
                    {
                        SetState(AnimalState.Eating);
                    }
                }
                if(currentTarget == null)
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
                }
                if(deathTimer >0 )
                {   
                    if(currentTarget == null)
                    {
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
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if current state is Roaming and collided with food or trash object
        if (currentState == AnimalState.ChasingFood && (other.CompareTag("Food") || other.CompareTag("Trash")))
        {
            SetState(AnimalState.Eating);
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
        }
    }

    void SetRandomState()
    {
        currentState = (AnimalState)Random.Range(0, 2); // Randomly select between SearchingFood and Roaming
    }
}
