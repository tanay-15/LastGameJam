using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class applyImpulse : MonoBehaviour {
    private bool rightTrigger;
    private bool upTrigger;
    private bool downTrigger;
    private bool leftTrigger;
    private bool jump;
    private Rigidbody rb;


    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (rightTrigger)
        {
            goRight();
            rightTrigger = false;
        }
        if (upTrigger)
        {
            goUp();
            upTrigger = false;
        }
        if (downTrigger)
        {
            goDown();
            downTrigger = false;
        }
        if (leftTrigger)
        {
            goLeft();
            leftTrigger = false;
        }
        if(jump)
        {
            Jump();
            jump = false;
        }
    }

    void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.tag == "rightTrigger")
        {
            Debug.Log("Triggering Right");
            rightTrigger = true;

        }
        if (col.gameObject.tag == "upTrigger")
        {
            Debug.Log("Triggering up");
            upTrigger = true;

        }

        if (col.gameObject.tag == "downTrigger")
        {
            Debug.Log("Triggering down");
            downTrigger = true;

        }

        if (col.gameObject.tag == "leftTrigger")
        {
            Debug.Log("Triggering left");
            leftTrigger = true;

        }

        if (col.gameObject.tag == "Jump")
        {
            Debug.Log("jumping");
            jump = true;

        }

    }

    void goRight()
    {
        rb.AddForce(new Vector3(-60.0f, 0, 0), ForceMode.Impulse);
    }
    void goUp()
    {
        rb.AddForce(new Vector3(0, 0, -60.0f), ForceMode.Impulse);
    }
    void goLeft()
    {
        rb.AddForce(new Vector3(60, 0 , 0), ForceMode.Impulse);
    }
    void goDown()
    {
        rb.AddForce(new Vector3(0, 0, 60.0f), ForceMode.Impulse);
    }
    void Jump()
    {
        rb.AddForce(new Vector3(0, 9.0f, 0), ForceMode.Impulse);
    }
}
