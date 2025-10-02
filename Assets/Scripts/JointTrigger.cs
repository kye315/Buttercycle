using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class JointTrigger : MonoBehaviour
{
    public UnityEvent EventToTrigger;

    private void OnJointBreak(float breakForce)
    {
        EventToTrigger.Invoke();
    }
}
