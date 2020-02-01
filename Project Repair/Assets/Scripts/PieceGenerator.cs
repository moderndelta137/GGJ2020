using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceGenerator : MonoBehaviour
{
    public GameObject pieceObject;
    // 取り敢えず一旦10個
    public int distribute_piece = 10;

    // Start is called before the first frame update
    void Start()
    {

        // Pieceをランダム生成
        for (int i = 0; i < this.distribute_piece; i++)
        {
            Instantiate(pieceObject, new Vector3(Random.Range(-15.0f, 15.0f), 0.7f, Random.Range(-15.0f, 10.0f)), Quaternion.identity);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
