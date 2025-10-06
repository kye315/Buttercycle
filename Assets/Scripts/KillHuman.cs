using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillHuman : MonoBehaviour
{

    public string tag1 = "Wheel";

    public string tag2 = "Jumper";

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == tag1 || collision.gameObject.tag == tag2)
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
