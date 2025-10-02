using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    public UnityEvent EventToTrigger;

    public string TriggerTag;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == TriggerTag)
        {
            EventToTrigger.Invoke();
            Destroy(gameObject);
        }
        
    }
}
