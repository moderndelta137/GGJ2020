using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceController : MonoBehaviour
{
    public GameObject[] Pieces;

    // Start is called before the first frame update
    void Start()
    {

        Pieces = GameObject.FindGameObjectsWithTag("Piece");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
