using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitTutotrial : MonoBehaviour
{
    public GameObject tutorialpage;
    void Start()
    {
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TurnOffTutorial()
    {
        tutorialpage.SetActive(false);
        Time.timeScale = 1f;
    }
}
