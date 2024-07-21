using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PickUpTrash : MonoBehaviour
{
    public int maxTrashCapacity = 5;
    public int currentTrash = 0;
    public int totalDumpedTrash = 0; // 新增：记录一共丢弃了多少垃圾
    public TMP_Text interactionText;
    public TMP_Text trashCountText;
    public TMP_Text totalDumpedTrashText; // 新增：显示一共丢弃了多少垃圾的TextMesh Pro对象

    public int SaveAnimalNumber = 0; //
    public TMP_Text SaveAnimalText;
    public bool CanSaveFish;

    public GameObject currentTrashObject;
    public bool canInteractWithTrash = false;
    public bool canDumpTrash = false;

    [Header("sound")]
    public AudioClip pickUpSound; // 修改：拾取垃圾的音效文件
    public AudioClip dumpSound; // 修改：倒垃圾的音效文件
    private AudioSource audioSource; // 修改：音频源
    void Start()
    {
        interactionText.gameObject.SetActive(false);
        UpdateTrashCountText();
        UpdateTotalDumpedTrashText(); // 新增：初始化时更新总丢弃垃圾数量的显示


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
                audioSource.PlayOneShot(pickUpSound); // 播放拾取垃圾的音效
            }// 播放拾取垃圾的音效
            PickupTrash();
        }
        else if (canDumpTrash && Input.GetKeyDown(KeyCode.E))
        {
            if (dumpSound != null)
            {
                audioSource.PlayOneShot(dumpSound); // 播放倒垃圾的音效
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
        totalDumpedTrash += currentTrash; // 新增：更新总丢弃垃圾数量
        currentTrash = 0;
        UpdateTrashCountText();
        UpdateTotalDumpedTrashText(); // 新增：更新总丢弃垃圾数量的显示
        interactionText.gameObject.SetActive(false);
    }

    private void UpdateTrashCountText()
    {
        trashCountText.text = "Trash: " + currentTrash + "/" + maxTrashCapacity;
    }

    private void UpdateTotalDumpedTrashText() // 新增：更新总丢弃垃圾数量的显示
    {
        totalDumpedTrashText.text = "Total Recycled Trash: " + totalDumpedTrash;
    }


}
