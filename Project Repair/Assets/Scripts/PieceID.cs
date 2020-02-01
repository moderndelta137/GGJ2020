using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceID : MonoBehaviour
{
    public int ID;
    // Whose piece
    public int whichPiece = 1;

    // Whether this is collected
    public bool isCollected = false;

    private PlayerController player1;
    private PlayerController player2;
    void Start()
    {
        player1 = GameObject.Find("Player1").GetComponent<PlayerController>();
        player2 = GameObject.Find("Player2").GetComponent<PlayerController>();
    }

    private void Update()
    {
        Debug.Log("player1 : " + player1.currentPiecesCount);
        Debug.Log("player2 : " + player2.currentPiecesCount);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CollectZone")
        {
            // 複数回呼び出されるのを防ぐ
            if (isCollected) return;
            this.isCollected = true;

            CollectZone collectZone = other.GetComponent<CollectZone>();

            // 相手の収集ゾーンの場合は無視
            if (collectZone.whichPlayer != whichPiece) return;

            // 集めた破片の数をプラス１
            if (whichPiece == 1)
            {
                player2.currentPiecesCount++;
            }
            else
            {
                player1.currentPiecesCount++;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "CollectZone")
        {
            // 複数回呼び出されるのを防ぐ
            if (!isCollected) return;
            this.isCollected = false;

            CollectZone collectZone = other.GetComponent<CollectZone>();

            // 相手の収集ゾーンの場合は無視
            if (collectZone.whichPlayer != whichPiece) return;

            // 集めた破片の数をマイナス１
            if (whichPiece == 1)
            {
                player2.currentPiecesCount--;
            }
            else
            {
                player1.currentPiecesCount--;
            }
        }
    }
}
