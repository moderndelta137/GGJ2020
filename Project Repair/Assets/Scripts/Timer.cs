using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    private int maxPieceCount;

    private CollectZone collectzone1;
    private CollectZone collectzone2;

    private ClearType clearType;

    void Start()
    {
        timeTextComponent = timeText.GetComponent<Text>();
        finishTextComponent = finishText.GetComponent<Text>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        time = gameManager.timeLimit;

        player1 = GameObject.Find("Player1").GetComponent<PlayerController>();
        player2 = GameObject.Find("Player2").GetComponent<PlayerController>();

        // PieceGenerator
        maxPieceCount = GameObject.Find("PieceGenerator").GetComponent<PieceGenerator>().distribute_piece;

        // CollectZone
        collectzone1 = GameObject.Find("CollectZone1").GetComponent<CollectZone>();
        collectzone2 = GameObject.Find("CollectZone2").GetComponent<CollectZone>();
    }

    // Update is called once per frame
    void Update()
    {
        resultCheck();

        if (time > 0)
        {
            time -= Time.deltaTime;
        }
        else
        {
            timeText.SetActive(false);
            finishText.SetActive(true);
            time = 0;

            timeup();
        }

        timeTextComponent.text = "Time : " + time.ToString("f1");
    }

    enum ClearType
    {
        TRUE,
        BAD,
        TIMEOUT
    }

    ClearType resultCheck()
    {
        clearType = ClearType.TIMEOUT;

        // check collectzone1

        List<PieceID> pieces1 = collectzone1.pieces;

        // 全て集まっているかどうか
        if (pieces1.Count == maxPieceCount)
        {
            // ２人のプレイヤーが関わっているかどうか
            bool player1 = false;
            bool player2 = false;
            for (int i = 0; i < pieces1.Count; i++)
            {
                if (pieces1[i].lastTouchPlayer == 1) player1 = true;
                if (pieces1[i].lastTouchPlayer == 2) player2 = true;
            }

            if (player1 && player2)
            {
                clearType = ClearType.TRUE;
                SceneManager.LoadScene("TrueEndingScene");
            }
            else
            {
                clearType = ClearType.BAD;
            }
        }

        // check collectzone1

        List<PieceID> pieces2 = collectzone2.pieces;

        // 全て集まっているかどうか
        if (pieces2.Count == maxPieceCount)
        {
            // ２人のプレイヤーが関わっているかどうか
            bool player1 = false;
            bool player2 = false;
            for (int i = 0; i < pieces2.Count; i++)
            {
                if (pieces2[i].lastTouchPlayer == 1) player1 = true;
                if (pieces2[i].lastTouchPlayer == 2) player2 = true;
            }

            if (player1 && player2)
            {
                clearType = ClearType.TRUE;
                SceneManager.LoadScene("TrueEndingScene");
            }
            else
            {
                clearType = ClearType.BAD;
            }

        }

        return clearType;
    }

    void timeup()
    {

        if (clearType == ClearType.TRUE)
        {
            SceneManager.LoadScene("TrueEndingScene");
            //finishTextComponent.text = "隠しクリア！";
        }
        else if(clearType == ClearType.BAD)
        {
            //時間切れ、片方しか集めてない場合BadEnd
            SceneManager.LoadScene("BadEnding");
        }
        else
        {
            SceneManager.LoadScene("Timeout");
            //finishTextComponent.text = "Game Over";
        }


    }
}
