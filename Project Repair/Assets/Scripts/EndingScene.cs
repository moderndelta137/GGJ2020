using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pickup1") || Input.GetButtonDown("Pickup2"))
        {
            SceneManager.LoadScene("TitleScene");
        }
    }
}
