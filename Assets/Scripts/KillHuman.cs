using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillHuman : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wheel" || collision.gameObject.tag == "Jumper")
        {
            Controller gcip = collision.transform.GetComponentInParent<Controller>();
            if (collision.transform.GetComponentsInParent<Controller>() != null) {
                if (gcip.dead == false)
                {
                    gcip.kill();
                }
            }
        }
    }
}
