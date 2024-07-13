using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // ����TextMeshPro�����ռ�

public class Pause : MonoBehaviour
{
    public GameObject PauseMenu;
    void Start()
    {
        
        PauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0f;
            PauseMenu.SetActive(true);
        }

    }
}
