using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PickUpTrash : MonoBehaviour
{
    public int maxTrashCapacity = 5;
    public int currentTrash = 0;
    public int totalDumpedTrash = 0; // ��������¼һ�������˶�������
    public TMP_Text interactionText;
    public TMP_Text trashCountText;
    public TMP_Text totalDumpedTrashText; // ��������ʾһ�������˶���������TextMesh Pro����

    public int SaveAnimalNumber = 0; //
    public TMP_Text SaveAnimalText;
    public bool CanSaveFish;

    public GameObject currentTrashObject;
    public bool canInteractWithTrash = false;
    public bool canDumpTrash = false;

    void Start()
    {
        interactionText.gameObject.SetActive(false);
        UpdateTrashCountText();
        UpdateTotalDumpedTrashText(); // ��������ʼ��ʱ�����ܶ���������������ʾ


        CanSaveFish = false;
        SaveAnimalNumber = 0;
    }

    void Update()
    {
        if (canInteractWithTrash && Input.GetKeyDown(KeyCode.E))
        {
            PickupTrash();
        }
        else if (canDumpTrash && Input.GetKeyDown(KeyCode.E))
        {
            DumpTrash();
        }

        SaveAnimalText.text = "Number of animals rescued: " + SaveAnimalNumber;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trash"))
        {
            canInteractWithTrash = true;
            currentTrashObject = other.gameObject;
            interactionText.text = "Press E to pick up trash";
            interactionText.gameObject.SetActive(true);
        }
        else if (other.CompareTag("DumpLocation"))
        {
            canDumpTrash = true;
            interactionText.text = "Press E to dump trash";
            interactionText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Trash"))
        {
            canInteractWithTrash = false;
            currentTrashObject = null;
            interactionText.gameObject.SetActive(false);
        }
        else if (other.CompareTag("DumpLocation"))
        {
            canDumpTrash = false;
            interactionText.gameObject.SetActive(false);
        }
    }

    private void PickupTrash()
    {
        if (currentTrash < maxTrashCapacity)
        {
            Destroy(currentTrashObject);
            currentTrash++;
            canInteractWithTrash = false;
            UpdateTrashCountText();
            interactionText.gameObject.SetActive(false);
        }
        else
        {
            interactionText.text = "Trash capacity full! Go to dump location.";
        }
    }

    private void DumpTrash()
    {
        totalDumpedTrash += currentTrash; // �����������ܶ�����������
        currentTrash = 0;
        UpdateTrashCountText();
        UpdateTotalDumpedTrashText(); // �����������ܶ���������������ʾ
        interactionText.gameObject.SetActive(false);
    }

    private void UpdateTrashCountText()
    {
        trashCountText.text = "Trash: " + currentTrash + "/" + maxTrashCapacity;
    }

    private void UpdateTotalDumpedTrashText() // �����������ܶ���������������ʾ
    {
        totalDumpedTrashText.text = "Total Dumped Trash: " + totalDumpedTrash;
    }


}
