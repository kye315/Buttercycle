using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MoveUpDown : MonoBehaviour
{

    private bool active;

    // Start is called before the first frame update
    void Start()
    { 
        
    }

    private void OnCollisionEnter(Collision collision)
    {
            if (!active && collision.transform.tag == "Player")
            {
                active = true;
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            }
    }

    // Update is called once per frame
    void Update()
    {
        if(active)
        {
            transform.position += new Vector3(0,3*Time.deltaTime,0);
        }
    }
}
