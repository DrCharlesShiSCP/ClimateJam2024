using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // 引入TextMeshPro命名空间

public class CountdownTimer : MonoBehaviour
{
    public float countdownTime; // 倒计时时间，由游戏设计师在Inspector中设置
    public TMP_Text countdownText; // 显示倒计时的TextMeshPro文本
    public Image gameOverImage; // 游戏结束时显示的图片

    public bool isCountingDown = false; // 倒计时是否进行中

    public TMP_Text FinalTrashPick;
    public int FinalTrashPickNumber;
    public PickUpTrash player;

    void Start()
    {
        // 初始化时隐藏游戏结束图片
        gameOverImage.gameObject.SetActive(false);
        player = GameObject.FindWithTag("Player").GetComponent<PickUpTrash>();

        // 检查倒计时时间是否有效
        if (countdownTime > 0)
        {
            isCountingDown = true;
        }
        else
        {
            Debug.LogError("请在Inspector中设置一个有效的倒计时时间");
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

