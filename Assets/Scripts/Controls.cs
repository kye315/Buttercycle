using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    public GameObject wheel;
    private Rigidbody wRB;

    public GameObject jumper;
    private ConfigurableJoint jCJ;

    private bool jumping = false;

    // Start is called before the first frame update
    void Start()
    {
        wRB = wheel.GetComponent<Rigidbody>();
        jCJ = jumper.GetComponent<ConfigurableJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        if (jumping)
        {
            jumper.transform.localScale += new Vector3(0,1*Time.deltaTime,0);
        }

        if (Input.GetKey(KeyCode.A))
        {
            wRB.AddForce(0,0,3);
        }
        if (Input.GetKey(KeyCode.D))
        {
            wRB.AddForce(0, 0, -3);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (!jumping) {
                jumping = true;
            }
        }
    }
}
