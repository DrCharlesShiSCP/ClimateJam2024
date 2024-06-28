using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PickUpTrash : MonoBehaviour
{
    public int maxTrashCapacity = 5;
    private int currentTrash = 0;
    public TMP_Text interactionText;
    public TMP_Text trashCountText;

    private GameObject currentTrashObject;
    public bool canInteractWithTrash = false;
    public bool canDumpTrash = false;

    void Start()
    {
        interactionText.gameObject.SetActive(false);
        UpdateTrashCountText();
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
            UpdateTrashCountText();
            canInteractWithTrash = false;
            interactionText.gameObject.SetActive(false);
        }
        else
        {
            interactionText.text = "Trash capacity full! Go to dump location.";
        }
    }

    private void DumpTrash()
    {
        currentTrash = 0;
        UpdateTrashCountText();
        interactionText.gameObject.SetActive(false);
    }

    private void UpdateTrashCountText()
    {
        trashCountText.text = "Trash: " + currentTrash + "/" + maxTrashCapacity;
    }
}
