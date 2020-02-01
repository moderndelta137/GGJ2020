using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float timeLimit = 300f;
    public int piecesCountPlayer1 = 0;
    public int piecesCountPlayer2 = 0;

    private PlayerController player1;
    private PlayerController player2;
    // Start is called before the first frame update
    void Start()
    {
        player1 = GameObject.Find("Player1").GetComponent<PlayerController>();
        player2 = GameObject.Find("Player2").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
