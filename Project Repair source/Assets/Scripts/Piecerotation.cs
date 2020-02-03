using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piecerotation : MonoBehaviour
{
    public float rotationspeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       this.gameObject.transform.Rotate( rotationspeed,0, 0,Space.Self);
    }
}
