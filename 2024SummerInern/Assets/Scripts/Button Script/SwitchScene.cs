using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SwitchScene : MonoBehaviour
{
    public int sceneNumber;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void ChangeGameScene()
    {
        SceneManager.LoadScene(sceneNumber);
    }
}
