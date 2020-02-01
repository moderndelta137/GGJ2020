using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceGenerator : MonoBehaviour
{
    public GameObject pieceObject;
    private GameObject instantiated_piece;
    private PieceID pieceid;
    // 取り敢えず一旦10個
    public int distribute_piece;
    public Vector3 distribute_range;
    public Material Player1_piece_mat;
    public Material Player2_piece_mat;
    // Start is called before the first frame update
    void Start()
    {

        // Pieceをランダム生成
        for (int i = 0; i < distribute_piece; i++)
        {
            instantiated_piece = Instantiate(pieceObject, new Vector3(Random.Range(-distribute_range.x, distribute_range.x), distribute_range.y, Random.Range(-distribute_range.z, distribute_range.z)), Quaternion.identity);
            pieceid = instantiated_piece.GetComponent<PieceID>();
            if(i< distribute_piece / 2)
            {
                pieceid.whichPiece = 1;
                pieceid.gameObject.GetComponent<MeshRenderer>().material = Player1_piece_mat;
                Debug.Log("Player1 Piece");
            }
            else
            {
                pieceid.whichPiece = 2;
                pieceid.gameObject.GetComponent<MeshRenderer>().material = Player2_piece_mat;
                Debug.Log("Player2 Piece");
            }

        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
