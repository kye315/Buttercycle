using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// To maintain continuity with the previous level this script destroys parts of rigs that were lost in-game

// yes i know its a singletonnnnnnnn blech

public class RigDestruction : MonoBehaviour
{
    // Grader
    private GameObject grader;

    private Grader gr;

    // rig refs bc its just easier if I drag and drop them

    public GameObject[] head;

    public GameObject[] RightArm;

    public GameObject[] LeftArm;

    public GameObject[] hat;

    public GameObject[] RightLeg;

    public GameObject[] LeftLeg;

    void RigBreak()
    {
        if (gr.Grades[0] == true)
        {
            for (int i = 0; i < LeftArm.Length; i++)
            {
                Destroy(LeftArm[i]);
            }
        }
        if (gr.Grades[1] == true)
        {
            for (int i = 0; i < LeftLeg.Length; i++)
            {
                Destroy(LeftLeg[i]);
            }
        }
        if (gr.Grades[2] == true)
        {
            for (int i = 0; i < RightArm.Length; i++)
            {
                Destroy(RightArm[i]);
            }
        }
        if (gr.Grades[3] == true)
        {
            for (int i = 0; i < RightLeg.Length; i++)
            {
                Destroy(RightLeg[i]);
            }
        }
        if (gr.Grades[4] == true)
        {
            for (int i = 0; i < hat.Length; i++)
            {
                Destroy(hat[i]);
            }
        }
        if (gr.Grades[5] == true)
        {
            for (int i = 0; i < head.Length; i++)
            {
                Destroy(head[i]);
            }
        }
    }

    void Start()
    {
        grader = GameObject.FindWithTag("Grader");
        if (grader == null)
        {
            // guess ill die
            Debug.Log("Invalid!");
        }
        else
        {
            gr = grader.GetComponent<Grader>();
            RigBreak();
        }

        Destroy(gameObject);
    }

}
