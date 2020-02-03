using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigTimer : MonoBehaviour
{
    public float Timer_interval;
    public Animator Pig_animator;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Playanimation", Random.Range(15f, 20f), Random.Range(15f, 20f));
    }

    // Update is called once per frame
    void Update()
    {
     
    }
    public void Playanimation()
    {
        Debug.Log("Pig");
        Pig_animator.SetInteger("Piganim", Random.Range(1, 4));
    }

    public void ResetPig()
    {
        Pig_animator.SetInteger("Piganim", 0);
    }

}
