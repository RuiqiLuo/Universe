using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beginningspeed : MonoBehaviour
{
    public Vector3 bspeed;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().velocity = bspeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
