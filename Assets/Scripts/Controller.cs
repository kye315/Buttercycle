using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Events;


public class Controller : MonoBehaviour
{
    public GameObject wheel;
    private Rigidbody wRB;

    public Canvas canvas;
    public GameObject Pause;

    public GameObject BGM1;
    public GameObject BGM2;

    public GameObject GameOverScreen;

    public GameObject jumper;
    private Vector3 jScale; // scale at startup
    private ConfigurableJoint jCJ;

    public float jumpSpeed = 4;

    public float runSpeed = 12;

    private bool jumping = false;

    // accessible by others
    public bool dead = false;

    public bool JumpLock = false; 

    // neck break countdown
    private bool neckCD = false;
    private float neckTicker = 10;
    public GameObject PreDeathGUI;
     
    private GameObject PreDeathGUIInstance;

    private TMP_Text txt;

    // finale countdown
    private bool inFinale;
    private float finaleTicker = 5;

    public GameObject fade;

    // grading information

    public bool RightArmGone = false;
    public bool LeftArmGone = false;
    public bool RightLegGone = false;
    public bool LeftLegGone = false;
    public bool HatGone = false;
    public bool NeckGone = false;

    public UnityEvent TriggerArmBreak;

    public GameObject grader;

    public UnityEvent TriggerLegBreak;

    // Start is called before the first frame update
    void Start()
    {
        jScale = jumper.transform.localScale;
        wRB = wheel.GetComponent<Rigidbody>();
        jCJ = jumper.GetComponent<ConfigurableJoint>();
        Time.timeScale = 1;
    }

    public void TriggerPause()
    {
        Pause.SetActive(true);
        Time.timeScale = 0;
    }
    
    public void TriggerUnPause()
    {
        Pause.SetActive(false);
        Time.timeScale = 1;
    }

    public void armBreak(int LeftOrRight)
    {
        switch (LeftOrRight) {
            case 0: // left
                LeftArmGone = true;
                break;
            case 1: // right
                RightArmGone = true;
                break;
        }
        if (RightArmGone && LeftArmGone)
        {
            TriggerArmBreak.Invoke();
        }
    }

    public void legBreak(int LeftOrRight)
    {
        switch (LeftOrRight)
        {
            case 0: // left
                LeftLegGone = true;
                break;
            case 1: // right
                RightLegGone = true;
                break;
        }
        if (LeftLegGone && RightLegGone)
        {
            TriggerLegBreak.Invoke(); // incapacitated
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).GetComponent<Rigidbody>())
                {
                    transform.GetChild(i).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                }
            }
            runSpeed = 0;
            JumpLock = true;
        }
    }

    public void headBreak()
    {
        if (neckCD == false && !dead)
        {
            neckCD = true;
            PreDeathGUIInstance = Instantiate(PreDeathGUI, canvas.transform);
            txt = PreDeathGUIInstance.transform.GetChild(0).GetComponent<TMP_Text>();
        }
        NeckGone = true;
    }

    public void lockJump()
    {
        JumpLock = true;
    }

    public void finale()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<Rigidbody>())
            {
                transform.GetChild(i).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                transform.GetChild(i).GetComponent<Rigidbody>().useGravity = false;
            }
        }
        jumping = false;
        jumper.transform.localScale = jScale;
        jumping = true;
        neckCD = false;
        runSpeed = 0;
        inFinale = true;

        grader.GetComponent<Grader>().Grades = (new bool[] { LeftArmGone,LeftLegGone,RightArmGone,RightLegGone,HatGone,NeckGone});
         
        DontDestroyOnLoad(grader.gameObject);
    }

    public void kill()
    {
        Debug.Log("Kill!");
        Instantiate(GameOverScreen, canvas.transform);
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<Joint>())
            {
                Destroy(transform.GetChild(i).GetComponent<Joint>());
            }
            if (transform.GetChild(i).GetComponent<Rigidbody>())
            {
                transform.GetChild(i).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            }
        }
        dead = true;
    }

    public void loseHat()
    {
        HatGone = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (inFinale)
        {
            finaleTicker -= Time.deltaTime;
            BGM1.GetComponent<AudioSource>().volume -= Time.deltaTime;
            BGM2.GetComponent<AudioSource>().volume -= Time.deltaTime;
            fade.GetComponent<Image>().color = new Color(1, 1, 1, (1 - (finaleTicker / 10)));
            if (finaleTicker <= 0)
            {
                SceneManager.LoadScene("Ending1");
            }
        }
        if (!dead)
        {
            if (jumping)
            {
                jumper.transform.localScale += new Vector3(0, jumpSpeed * Time.deltaTime, 0);
                if (jumper.transform.localScale.y >= 3)
                {
                    jumping = false;
                    jumper.transform.localScale = jScale;
                }
            }

            if (Input.GetKey(KeyCode.A))
            {
                wRB.AddForce(0, 0, runSpeed);
            }
            if (Input.GetKey(KeyCode.D))
            {
                wRB.AddForce(0, 0, -runSpeed);
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (!jumping && !JumpLock)
                {
                    jumping = true;
                }
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (Pause.activeSelf  == true) {
                    TriggerUnPause();
                } else {
                    TriggerPause();
                }
            }

            if (neckCD)
            {
                neckTicker -= Time.deltaTime;
                PreDeathGUIInstance.GetComponent<CanvasRenderer>().SetColor(new Color(1 - (neckTicker / 10), 0, 0));
                txt.text = neckTicker.ToString();
                if (neckTicker < 0)
                {
                    neckCD = false;
                    Destroy(PreDeathGUIInstance);
                    kill();
                }
            }
        }
        else
        {
            BGM1.GetComponent<AudioSource>().volume -= Time.deltaTime;
            BGM2.GetComponent<AudioSource>().volume -= Time.deltaTime;
            if (Time.timeScale > 0)
            {
                Time.timeScale -= 0.5f * Time.deltaTime;
            }
            else
            {
                Time.timeScale = 0;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Time.timeScale = 1;
                SceneManager.LoadScene("SampleScene");
            }
        }
    }
}
