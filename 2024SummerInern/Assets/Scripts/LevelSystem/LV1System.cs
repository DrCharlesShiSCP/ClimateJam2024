using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LV1System : MonoBehaviour
{
    public PickUpTrash player;
    

    public int PlayertotalDumpedTrash;
    public int TotalAnimal;

    public int WinTrashNumber;
    public int WinAnimalNumber;

    public GameObject WinImage;
    public GameObject SideQuestImage;
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PickUpTrash>();
       
        WinImage.gameObject.SetActive(false);
        SideQuestImage.gameObject.SetActive(false);


        PlayertotalDumpedTrash = 0;
        TotalAnimal = 0;

        PlayertotalDumpedTrash = player.totalDumpedTrash;
        TotalAnimal = player.SaveAnimalNumber;


    }

    // Update is called once per frame
    void Update()
    {
        PlayertotalDumpedTrash = player.totalDumpedTrash;
        TotalAnimal = player.SaveAnimalNumber;

        if (PlayertotalDumpedTrash >= WinTrashNumber)
        {
            
            Time.timeScale = 0f;
            WinImage.gameObject.SetActive(true);

            if(TotalAnimal >=  WinAnimalNumber)
            {
                SideQuestImage.gameObject.SetActive(true);
            }


        }
    }
}
