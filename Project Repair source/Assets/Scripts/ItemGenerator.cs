using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    public GameObject toughItem;
    public GameObject speedUpItem;
    public GameObject stunItem;

    public GameObject[] tagObject;
    public float interval = 3.0f;
    private float timer = 0.0f;

    public float xrange_min;
    public float xrange_max;
    public float zrange_min;
    public float zrange_max;

    // 取り敢えず一旦5個
    public int distribute_piece = 5;


    // Start is called before the first frame update
    void Start()
    {
        // Itemをランダム生成
        for (int i = 0; i < this.distribute_piece; i++)
        {
            int rnd = Random.Range(0, 3);
            GameObject itemObject;
            if(rnd == 0)
            {
                itemObject = toughItem;
            }
            else if(rnd == 1)
            {
                itemObject = speedUpItem;
            }
            else
            {
                itemObject = stunItem;
            }

            Instantiate(itemObject, new Vector3(Random.Range(xrange_min, xrange_max), 0.7f, Random.Range(zrange_min, zrange_max)), Quaternion.identity);
        }

    }

    void Update()
    {
        timer += Time.deltaTime;

        // シーン上のオブジェクトの数を数える
        tagObject = GameObject.FindGameObjectsWithTag("Item");

        // 10個以下なら
        if (tagObject.Length <= 5)
        {
            if (timer >= interval)
            {
                int rnd = Random.Range(0, 3);
                GameObject itemObject;
                if (rnd == 0)
                {
                    itemObject = toughItem;
                }
                else if (rnd == 1)
                {
                    itemObject = speedUpItem;
                }
                else
                {
                    itemObject = stunItem;
                }

                // シーン上に生成
                Instantiate(itemObject, new Vector3(Random.Range(xrange_min, xrange_max), 0.7f, Random.Range(zrange_min, zrange_max)), Quaternion.identity);
                timer = 0.0f;
            }
        }
    }
}
