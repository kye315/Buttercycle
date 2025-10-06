using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneCountdown : MonoBehaviour
    // activated by other scripts through UnityEvents
{
    private float ticker = 3;

    public bool Timed;

    public string Scene;
    
    public void ActivateTimer()
    {
        Debug.Log("activated");
        Timed = true;
    }


    // Update is called once per frame
    void Update()
    {
        {
            if (Timed)
            {
                ticker -= Time.deltaTime;
                if (ticker < 0)
                {
                    SceneManager.LoadScene(Scene);
                }
            }
        }
    }
}
