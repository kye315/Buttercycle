using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM_main : MonoBehaviour
{
    private AudioSource mainSource;

    public AudioSource Next;

    void Start()
    {
        mainSource = GetComponent<AudioSource>();
        mainSource.Play();
        Next.PlayDelayed(mainSource.clip.length);
    }
}
