using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // ����TextMeshPro�����ռ�

public class CountdownTimer : MonoBehaviour
{
    public float countdownTime; // ����ʱʱ�䣬����Ϸ���ʦ��Inspector������
    public TMP_Text countdownText; // ��ʾ����ʱ��TextMeshPro�ı�
    public Image gameOverImage; // ��Ϸ����ʱ��ʾ��ͼƬ

    public bool isCountingDown = false; // ����ʱ�Ƿ������

    public TMP_Text FinalTrashPick;
    public int FinalTrashPickNumber;
    public PickUpTrash player;

    void Start()
    {
        // ��ʼ��ʱ������Ϸ����ͼƬ
        gameOverImage.gameObject.SetActive(false);
        player = GameObject.FindWithTag("Player").GetComponent<PickUpTrash>();

        // ��鵹��ʱʱ���Ƿ���Ч
        if (countdownTime > 0)
        {
            isCountingDown = true;
        }
        else
        {
            Debug.LogError("����Inspector������һ����Ч�ĵ���ʱʱ��");
        }
    }

    void Update()
    {

        FinalTrashPickNumber = player.totalDumpedTrash;

        if (isCountingDown)
        {
            if (countdownTime > 0)
            {
                countdownTime -= Time.deltaTime;
                UpdateCountdownText(countdownTime);
            }
            else
            {
                isCountingDown = false;
                countdownTime = 0;
                UpdateCountdownText(countdownTime);
                ShowGameOverUI();
            }
        }
    }

    void UpdateCountdownText(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60F);
        int seconds = Mathf.FloorToInt(time % 60F);
        int milliseconds = Mathf.FloorToInt((time * 100F) % 100F);
        countdownText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    }

    void ShowGameOverUI()
    {
        gameOverImage.gameObject.SetActive(true);

        FinalTrashPick.text = "Total Trash you pick: " + FinalTrashPickNumber;
    }
}

