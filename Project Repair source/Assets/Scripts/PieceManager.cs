using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceManager : MonoBehaviour
{
    public GameObject[] Pieces;
    public Vector3[] Goal_pattern_position;
    public Vector3[] Goal_pattern_rotation;
    public float Pattern_position_tolerance;
    public float Pattern_rotation_tolerance;
    public GameObject Last_moved_piece;

    // Start is called before the first frame update
    void Start()
    {


        Pieces = GameObject.FindGameObjectsWithTag("Piece");



    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Checkpattern()
    {

       // Last_moved_piece.transform.position
    }
}
