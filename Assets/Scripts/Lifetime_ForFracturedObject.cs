using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifetime_ForFracturedObject : MonoBehaviour
{
    public float Lifetime = 4;
    bool Activated;

    private void OnJointBreak(float breakForce)
    {
        Debug.Log("!");
        Activated = true;
    }

    void Update()
    {
        if (Activated)
        {
            Lifetime -= Time.deltaTime;
            if (Lifetime < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
