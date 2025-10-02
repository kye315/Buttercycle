using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreLifetime : MonoBehaviour
{
    public float lifetime = 10;

    // Update is called once per frame

    void Start()
    {
        lifetime = Random.Range(lifetime - 2, lifetime + 2);
    }
    void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime < 0)
        {
            Destroy(gameObject);
        }
    }
}
