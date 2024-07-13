using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // µ¼º½×é¼þ
using TMPro;

public class GoldAnimalAI : MonoBehaviour
{
    public enum AnimalState
    {
       
        SearchingFood, 
        Roaming,
        ChasingFood,
        Eating,

        GoingToArea1,
        StayingInArea,
        GoingToNextArea,
        WaitingForPlayer,
        GameWin,
        Dead,


        
        
    }

    public Transform[] targetAreas; // Array of target areas the animal will visit
    public float[] areaStayTimes; // Array of stay times in each area
    public float moveSpeed = 5.0f; // Speed of animal movement
    public float gameWinDistanceThreshold = 1.0f; // Distance threshold to consider player reaching the final area
    public GameObject gameWinUI; // UI image for game win

    public AnimalState currentState;
    public int currentTargetIndex;
    public float stayTimer;
    public float areaCountdownTimer; // Countdown timer for each area


    public float eatingDuration = 5.0f; // Eating duration in seconds
    public float deathTimerDuration = 10.0f; // Death timer duration in seconds



    public float eatingTimer;
    public float deathTimer;
    public float goToTargetTimer;// Go to target duration in seconds (5 minutes)


    public GameObject currentTarget;

    public bool CanSave;
    public GameObject HelpSign;
    public PickUpTrash player;
    void Start()
    {
        SetState(AnimalState.GoingToArea1);

        areaCountdownTimer = 130;

       CanSave = false;
        HelpSign.SetActive(false);

        player = GameObject.FindWithTag("Player").GetComponent<PickUpTrash>();
    }

    void Update()
    {


        areaCountdownTimer -= Time.deltaTime;
        if (areaCountdownTimer <= 0)
        {
            SetState(AnimalState.GoingToNextArea);
        }

        switch (currentState)
        {
            case AnimalState.GoingToArea1:
                MoveTowardsTarget(targetAreas[0]);
                if (Vector3.Distance(transform.position, targetAreas[0].transform.position) < 0.1f)
                {   
                    SetState(AnimalState.SearchingFood);
                    areaCountdownTimer = 130;
                }
                break;


            
            case AnimalState.StayingInArea:
                //areaCountdownTimer -= Time.deltaTime;
                //if (areaCountdownTimer <= 0)
                //{
                //    SetState(AnimalState.GoingToNextArea);
                //}
                break;


            case AnimalState.GoingToNextArea:
                int playerPassedAreas = DeterminePlayerPassedAreas();
                currentTargetIndex = Mathf.Min(playerPassedAreas + 1, targetAreas.Length - 1);

                SetState(AnimalState.SearchingFood); // Start searching food in the new area
                break;

            case AnimalState.WaitingForPlayer:
                if (Vector3.Distance(transform.position, targetAreas[currentTargetIndex].position) <= gameWinDistanceThreshold)
                {
                    gameWinUI.SetActive(true); // Show game win UI image
                    SetState(AnimalState.GameWin);
                }
                break;


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

                if (areaCountdownTimer <= 0)
                {
                    SetState(AnimalState.GoingToNextArea);
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
                if (currentTarget == null)
                {
                    SetState(AnimalState.SearchingFood);
                }

                if (areaCountdownTimer <= 0)
                {
                    SetState(AnimalState.GoingToNextArea);
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

                if (areaCountdownTimer <= 0)
                {
                    SetState(AnimalState.GoingToNextArea);
                }
                break;





            case AnimalState.Dead:
                // Implement death behavior
                deathTimer -= Time.deltaTime;
                if (deathTimer <= 0)
                {
                    Destroy(gameObject); // Destroy the animal object
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

                if (areaCountdownTimer <= 0)
                {
                    SetState(AnimalState.GoingToNextArea);
                }
                break;

















            case AnimalState.GameWin:
                // Game win state; no further actions needed
                break;
           
        }
    }

    int DeterminePlayerPassedAreas()
    {
        int playerPassedAreas = 0;
        // Example logic to determine which areas player has passed
        // Placeholder for actual implementation based on player's position

        // For now, let's assume a simple example where player passed area based on current target index
        playerPassedAreas = currentTargetIndex;

        return playerPassedAreas;
    }

    void SetRandomState()
    {
        currentState = (AnimalState)Random.Range(0, 2); // Randomly select between SearchingFood and Roaming
    }
    void OnTriggerEnter(Collider other)
    {
        // Check if current state is Roaming and collided with food or trash object
        if (currentState == AnimalState.ChasingFood && (other.CompareTag("Food") || other.CompareTag("Trash")))
        {
            SetState(AnimalState.Eating);
        }

      
        if (currentState == AnimalState.Dead && (other.CompareTag("Player")))
        {
            CanSave = true;
        }
    }

    void MoveTowardsTarget(Transform target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }

    void SetState(AnimalState newState)
    {
        currentState = newState;

        switch (currentState)
        {
            case AnimalState.GoingToArea1:
                currentTargetIndex = 0;
                MoveTowardsTarget(targetAreas[currentTargetIndex]);
                break;
            case AnimalState.SearchingFood:
                // Initialize searching food state
                break;
            case AnimalState.StayingInArea:
                // Initialize staying in area state
                break;
            case AnimalState.GoingToNextArea:
                // Initialize going to next area state
                break;
            case AnimalState.WaitingForPlayer:
                // Initialize waiting for player state
                break;
            case AnimalState.GameWin:
                // Initialize game win state
                break;
            case AnimalState.Dead:
                // Initialize death state
                break;
        }
    }
}
