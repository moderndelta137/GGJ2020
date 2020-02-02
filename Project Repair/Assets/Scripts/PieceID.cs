﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceID : MonoBehaviour
{
    // id
    public int id;

    // 最後に触れたプレイヤーを記録(0:まだ誰も触っていない。1:プレイヤー1 2: プレイヤー2)
    public int lastTouchPlayer;

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

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CollectZone")
        {
            // 複数回呼び出されるのを防ぐ
            if (isCollected) return;
            this.isCollected = true;

            // CollectZoneのPiecesリストに自分自身を追加
            CollectZone collectZone = other.GetComponent<CollectZone>();
            collectZone.pieces.Add(this);

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "CollectZone")
        {
            // 複数回呼び出されるのを防ぐ
            if (!isCollected) return;
            this.isCollected = false;

            // 自分を削除
            CollectZone collectZone = other.GetComponent<CollectZone>();
            for (int i = 0; i < collectZone.pieces.Count; i++)
            {
                if(collectZone.pieces[i].id == this.id)
                {
                    collectZone.pieces.RemoveAt(i);
                }
            }
        }
    }
}
