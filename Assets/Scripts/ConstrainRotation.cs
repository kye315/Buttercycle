using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstrainRotation : MonoBehaviour
{
    public float min = -40f;

    public float max = 40f;

    Quaternion start;

    // Start is called before the first frame update
    void Start()
    {
        start = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(GetComponent<Rigidbody>().rotation.x);


        //Debug.Log(transform.localRotation.x);
        //float rotX = Mathf.Clamp(start.x + transform.localRotation.x, min, max);

        //Debug.Log(rotX);

        //var rotation = Quaternion.AngleAxis(rotX, new Vector3(1,0,0));

        //transform.localRotation = Quaternion.Euler(rotX,0,0);
    }
}
