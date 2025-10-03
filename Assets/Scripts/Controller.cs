using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Events;


public class Controller : MonoBehaviour
{
    public GameObject wheel;
    private Rigidbody wRB;

    public Canvas canvas;

    public GameObject GameOverScreen;

    public GameObject jumper;
    private Vector3 jScale; // scale at startup
    private ConfigurableJoint jCJ;

    public float jumpSpeed = 4;

    public float runSpeed = 12;

    private bool jumping = false;

    // accessible by others
    public bool dead = false;

    // neck break countdown
    private bool neckCD = false;
    private float neckTicker = 10;
    public GameObject PreDeathGUI;
     
    private GameObject PreDeathGUIInstance;

    private TMP_Text txt;

    // grading information

    public bool RightArmGone = false;
    public bool LeftArmGone = false;
    public bool RightLegGone = false;
    public bool LeftLegGone = false;

    public UnityEvent TriggerArmBreak;

    public UnityEvent TriggerLegBreak;

    // Start is called before the first frame update
    void Start()
    {
        jScale = jumper.transform.localScale;
        wRB = wheel.GetComponent<Rigidbody>();
        jCJ = jumper.GetComponent<ConfigurableJoint>();
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

    // Update is called once per frame
    void Update()
    {
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
                if (!jumping)
                {
                    jumping = true;
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
