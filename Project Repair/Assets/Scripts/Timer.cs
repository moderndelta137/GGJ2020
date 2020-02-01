using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public GameObject timeText;
    public GameObject finishText;

    private Text timeTextComponent;
    private Text finishTextComponent;
    private float time;
    private GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        timeTextComponent = timeText.GetComponent<Text>();
        finishTextComponent = finishText.GetComponent<Text>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        time = gameManager.timeLimit;
    }

    // Update is called once per frame
    void Update()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
        }
        else
        {
            timeText.SetActive(false);
            finishText.SetActive(true);
            time = 0;
        }

        timeTextComponent.text = "Time : " + time.ToString("f1");
    }
}
