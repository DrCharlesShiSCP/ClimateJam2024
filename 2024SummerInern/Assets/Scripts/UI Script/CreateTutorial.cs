using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTutorial : MonoBehaviour
{
    public GameObject tutorialpage;
    void Start()
    {
        tutorialpage.SetActive(false);
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tutorialpage.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    void OnTriggerExit(Collider other)
    {
        tutorialpage.SetActive(false);
    }


}
