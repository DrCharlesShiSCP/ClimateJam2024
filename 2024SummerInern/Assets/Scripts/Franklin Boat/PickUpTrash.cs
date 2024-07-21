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

    [Header("sound")]
    public AudioClip pickUpSound; // �޸ģ�ʰȡ��������Ч�ļ�
    public AudioClip dumpSound; // �޸ģ�����������Ч�ļ�
    private AudioSource audioSource; // �޸ģ���ƵԴ
    void Start()
    {
        interactionText.gameObject.SetActive(false);
        UpdateTrashCountText();
        UpdateTotalDumpedTrashText(); // ��������ʼ��ʱ�����ܶ���������������ʾ


        CanSaveFish = false;
        SaveAnimalNumber = 0;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

    }

    void Update()
    {
        if (canInteractWithTrash && Input.GetKeyDown(KeyCode.E))
        {
            if (pickUpSound != null)
            {
                audioSource.PlayOneShot(pickUpSound); // ����ʰȡ��������Ч
            }// ����ʰȡ��������Ч
            PickupTrash();
        }
        else if (canDumpTrash && Input.GetKeyDown(KeyCode.E))
        {
            if (dumpSound != null)
            {
                audioSource.PlayOneShot(dumpSound); // ���ŵ���������Ч
            }
            DumpTrash();
        }

        SaveAnimalText.text = "Number of Animals Rescued: " + SaveAnimalNumber;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trash"))
        {
            canInteractWithTrash = true;
            currentTrashObject = other.gameObject;
            interactionText.text = "Press E to Pick Up Trash";
            interactionText.gameObject.SetActive(true);
        }
        else if (other.CompareTag("DumpLocation"))
        {
            canDumpTrash = true;
            interactionText.text = "Press E to Recycle Trash";
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
            interactionText.text = "Trash Capacity Full! Go to Recycle Location.";
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
        totalDumpedTrashText.text = "Total Recycled Trash: " + totalDumpedTrash;
    }


}
