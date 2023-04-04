using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Transform theDestination;
    private bool keyCheck = false;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetButtonDown("E") && keyCheck == false)
        {
            keyCheck = true;
            GetComponent<MeshCollider>().enabled = false;
            GetComponent<Rigidbody>().useGravity = false;
            this.transform.position = theDestination.position;
            this.transform.parent = GameObject.Find("Destination").transform;


        }
        else if (Input.GetButtonDown("Q") && keyCheck == true) 
        {
            keyCheck = false;
            this.transform.parent = null;
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<MeshCollider>().enabled = true;

        }
    }  
}
