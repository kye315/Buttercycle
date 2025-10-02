using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableCube : MonoBehaviour
{
    public GameObject piece;

    public float TickDown = 3;

    bool CrackingInProgress = false;

    public Material bMarked;

    // For best results, the object's scales should be multiples of 0.5

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wheel")
        {
            GetComponent<Renderer>().material = bMarked;
            CrackingInProgress = true;
        }
        if (collision.gameObject.tag == "Jumper")
        {
            shatter();
        }
             
    }

    private void Update()
    {
        if (CrackingInProgress)
        {
            TickDown -= Time.deltaTime;
            if (TickDown < 0)
            {
                shatter();
            }
        }
    }
    public void shatter()
    {
        // failsafe
        GetComponent<Renderer>().material = bMarked;

        //ensures prefab and main object don't collide
        GetComponent<BoxCollider>().enabled = false;

        // Calculate values
        float AmountOnXAxis = transform.localScale.x * 2;
        float AmountOnYAxis = transform.localScale.y * 2;
        float AmountOnZAxis = transform.localScale.z * 2;

        Vector3 piecePos = new Vector3( // formatted for readability
            transform.position.x - (transform.localScale.x / 2) + 0.25f, 
            transform.position.y - (transform.localScale.y / 2) + 0.25f, 
            transform.position.z - (transform.localScale.z / 2) + 0.25f
            );

        Vector3 piecePosBase = piecePos;

        Debug.Log(AmountOnXAxis);

        for (int z = 0; z < AmountOnZAxis; z++) // moves along the Z axis
        {
            for (int y = 0; y < AmountOnYAxis; y++) // moves along the Y axis
            {
                for (int x = 0; x < AmountOnXAxis; x++) // moves along the X axis
                {
                    GameObject piecerino = Instantiate(piece, piecePos, Quaternion.identity); // Instantiate piece
                    for (int i = 0; i < piecerino.transform.childCount; i++)
                    {
                        if (piecerino.transform.GetChild(i).GetComponent<MeshRenderer>())
                        {
                            piecerino.transform.GetChild(i).GetComponent<MeshRenderer>().material = transform.GetComponent<MeshRenderer>().material;
                        }
                    }
                    piecePos = new Vector3(piecePos.x + 0.5f, piecePos.y, piecePos.z); // Adjust, keep, keep
                }
                piecePos = new Vector3(piecePosBase.x, piecePos.y + 0.5f, piecePos.z); // Reset, adjust, keep
            }
            piecePos = new Vector3(piecePos.x, piecePosBase.y, piecePos.z + 0.5f); // Keep, reset, adjust
        }
        Destroy(gameObject);
    }
}
