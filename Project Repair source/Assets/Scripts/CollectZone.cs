using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectZone : MonoBehaviour
{
    public List<PieceID> pieces;

    void Start()
    {
        pieces = new List<PieceID>();
    }

    void Update()
    {
        for (int i = 0; i < pieces.Count; i++)
        {
            Debug.Log("piece:" + i + " " + pieces[i].id);
        }
    }
}
