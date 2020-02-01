using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceGenerator : MonoBehaviour
{
    public GameObject pieceObject;
    // 取り敢えず一旦10個
    public int distribute_piece = 10;
    public Vector3 distribute_range;
    // Start is called before the first frame update
    void Start()
    {

        // Pieceをランダム生成
        for (int i = 0; i < this.distribute_piece; i++)
        {
            Instantiate(pieceObject, new Vector3(Random.Range(-distribute_range.x, distribute_range.x), distribute_range.y, Random.Range(-distribute_range.z, distribute_range.z)), Quaternion.identity);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
