using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin3D : MonoBehaviour
{
    public Vector3 Speed;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Speed * Time.deltaTime);
    }
}
