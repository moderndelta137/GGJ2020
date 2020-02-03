using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trueending : MonoBehaviour
{
    public ParticleSystem Starbeam1;
    public ParticleSystem Starbeam2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayFX()
    {
        Starbeam1.gameObject.SetActive(true);
        Starbeam1.Play();
        Starbeam2.gameObject.SetActive(true);
        Starbeam2.Play();
    }
}
