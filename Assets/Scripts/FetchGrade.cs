using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using TMPro;
using UnityEditor;
using UnityEngine.SceneManagement;

public class FetchGrade : MonoBehaviour
{
    public GameObject zgrade;

    private Grader Grader;

    public GameObject txt_flav;

    public GameObject txt_rank;

    public string scenetoloadnext;

    [System.Serializable] 
    public struct GradeText
    {
        public bool[] GradeCase; // left arm, left leg, right arm, right leg, hat gone, head gone
        public string FlavorText;
        public string RankText;
    }

    public GradeText[] GradeTexts;

    // Start is called before the first frame update
    void Start()
    {
        zgrade = GameObject.Find("Z_Grader");
        Grader = zgrade.GetComponent<Grader>();
        if (zgrade != null && Grader != null)
        {
            Debug.Log("Found");
            CalcText();
            Destroy(zgrade);
        }
        else
        {
            Debug.Log("Null");
            Destroy(gameObject);
        }
    }

    void CalcText()
    {
        string FlavTxtToSet = "Error handling message";
        string RankTextToSet = "Error handling message";
        if (Grader.Grades[4] == false) // hatted
        {
            FlavTxtToSet = "Holy hell, you kept your hat. Despite literally everything thrown at you, you managed to keep your hat. I don't even know what to write here. Some guy came up to me before the stunt and said he'd make a bet that you'd keep it the whole time and, like, he was so arrogant about it that I tried to take advantage of that by betting a really high number, and now I owe him 300,000 dollars. I'm probably going to have to change my name. You have thoroughly ruined my entire life. You win, by the way.";
            RankTextToSet = "ENDING: Hatted hubris";
        }
        else if (Grader.Grades[5] == true) // headless
        {
            FlavTxtToSet = "Despite quite literally losing your head, you managed to complete the stunt before bleeding out. If terrified screams count as cheers, then the crowd gave you a standing ovation. As you lay there on the cold, metal floor, you thought about all the possibilities available to you now that you had truly made it as a stuntwoman, but then you remembered you had no head and died a few seconds later.";
            RankTextToSet = "ENDING: Bitter Blood, Sweet Victory";
        }
        else
        {
            bool setText = false;
            for (int i = 0; i < GradeTexts.Length; i++)
            {
                Debug.Log("------------------------");
                for (int j = 0; j < GradeTexts[i].GradeCase.Length; j++)
                {
                    Debug.Log(GradeTexts[i].RankText);
                    Debug.Log(j + "=" + GradeTexts[i].GradeCase[j]);
                    Debug.Log(j + "=" + Grader.Grades[j]);
                    if (GradeTexts[i].GradeCase[j] == Grader.Grades[j])
                    {
                        Debug.Log("Match");
                        setText = true;
                    }
                    else
                    {
                        Debug.Log("Loop FAILED with errors");
                        setText = false;
                        break;
                    }

                    // loop completes
                    Debug.Log("Loop completed with no errors");
                }
                if (setText)
                {
                    Debug.Log("TEXT WAS SET!");
                    Debug.Log(GradeTexts[i].FlavorText);
                    FlavTxtToSet = GradeTexts[i].FlavorText;
                    RankTextToSet = GradeTexts[i].RankText;
                    if (i == 0)
                    {
                        scenetoloadnext = ("SampleScene");
                    }
                }
            }
        }

        txt_flav.GetComponent<TMP_Text>().text = FlavTxtToSet;
        txt_rank.GetComponent<TMP_Text>().text = RankTextToSet;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(scenetoloadnext);
        }
    }
}
