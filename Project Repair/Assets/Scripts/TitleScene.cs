using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    public Animator Player1;
    public Animator Player2;
    public Animator Key;
    public ParticleSystem Key_particle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pickup1") || Input.GetButtonDown("Pickup2"))
        {
            Key.SetBool("PressStart", true);
            Player1.SetBool("PressStart", true);
            Player2.SetBool("PressStart", true);
            Key_particle.Play();

        }
    }

    public void Loadmaingame()
    {
        SceneManager.LoadScene("GameMasterScene_mori");
    }

}
