using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    public GameObject itemObject;
    public GameObject[] tagObject;
    public float interval = 10.0f;
    private float timer = 0.0f;

    // 取り敢えず一旦5個
    public int distribute_piece = 5;


    // Start is called before the first frame update
    void Start()
    {
        // Itemをランダム生成
        for (int i = 0; i < this.distribute_piece; i++)
        {
            Instantiate(itemObject, new Vector3(Random.Range(-15.0f, 15.0f), 0.7f, Random.Range(-15.0f, 10.0f)), Quaternion.identity);
        }

    }

    void Update()
    {
        timer += Time.deltaTime;

        // シーン上のオブジェクトの数を数える
        tagObject = GameObject.FindGameObjectsWithTag("Item");

        // 10個以下なら
        if (tagObject.Length <= 10)
        {
            if (timer >= interval)
            {
                // シーン上に生成
                Instantiate(itemObject, new Vector3(Random.Range(-15.0f, 15.0f), 0.7f, Random.Range(-15.0f, 10.0f)), Quaternion.identity);
                timer = 0.0f;
            }
        }
    }
}
