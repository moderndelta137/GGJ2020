using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public GameObject timeText;
    public GameObject finishText;

    public int MaxPicesCount = 5;

    private Text timeTextComponent;
    private Text finishTextComponent;
    private float time;
    private GameManager gameManager;

    private PlayerController player1;
    private PlayerController player2;

    void Start()
    {
        timeTextComponent = timeText.GetComponent<Text>();
        finishTextComponent = finishText.GetComponent<Text>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        time = gameManager.timeLimit;

        player1 = GameObject.Find("Player1").GetComponent<PlayerController>();
        player2 = GameObject.Find("Player2").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
        }
        else
        {
            timeText.SetActive(false);
            finishText.SetActive(true);
            time = 0;

            resultCheck();
        }

        timeTextComponent.text = "Time : " + time.ToString("f1");
    }

    void resultCheck()
    {
        // 二人とも破片がそろった場合
        if(player1.currentPiecesCount == MaxPicesCount && player2.currentPiecesCount == MaxPicesCount)
        {
            Debug.Log("隠しクリア！");
            finishTextComponent.text = "隠しクリア！";
        }
        else if(player1.currentPiecesCount == MaxPicesCount)
        {
            Debug.Log("Win Player1 !!");
            finishTextComponent.text = "Win Player1！";
        }
        else if (player2.currentPiecesCount == MaxPicesCount)
        {
            Debug.Log("Win Player2 !!");
            finishTextComponent.text = "Win Player2！";
        }
        else
        {
            Debug.Log("GameOver");
            finishTextComponent.text = "Game Over";
        }
    }
}
