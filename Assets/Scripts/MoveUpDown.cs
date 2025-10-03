using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class MoveUpDown : MonoBehaviour
{

    public float speed = 6;

    public float max = 99999999999;

    private bool active;

    public UnityEvent onBeginMovement;

    // Start is called before the first frame update
    void Start()
    { 
        
    }

    private void OnCollisionEnter(Collision collision)
    {
            if (!active && collision.transform.tag == "Wheel")
            {
                active = true;
                onBeginMovement.Invoke();
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            }
    }

    // Update is called once per frame
    void Update()
    {
        if(active)
        {
            transform.position += new Vector3(0,speed*Time.deltaTime,0);
        }
        if (transform.position.y >= max)
        {
            Destroy(gameObject);
        }
    }
}
