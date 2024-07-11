using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowLevel : MonoBehaviour
{
    public GameObject levelpage;
    void Start()
    {
        levelpage.SetActive(false);
    }

    // Update is called once per frame
    public void ShowLevelPage()
    {
        levelpage.SetActive(true);
    }
}
