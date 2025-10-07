using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title_Start : MonoBehaviour
{
    float ticker = 1.5f; // match w sound

    bool started;

    private Camera cam;

    public GameObject audioSource;

    public GameObject canvas;

    void Start() 
    {
        Time.timeScale = 1;
    }

    void Update()
    {
        if (!started) // wait for input
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                started = true;
                audioSource.GetComponent<AudioSource>().Play();
                cam = GetComponent<Camera>();
                cam.transform.position = new Vector3(0, 6.25f, -3.19f);
                Destroy(canvas);
            }
        }
        else // open scene flourish
        {
            ticker -= Time.deltaTime;
            if (ticker < 0)
            {
                SceneManager.LoadScene("SampleScene");
            }
        }
    }
}
