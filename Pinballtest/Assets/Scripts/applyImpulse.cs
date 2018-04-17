using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class applyImpulse : MonoBehaviour {

    private bool rightTrigger;
    private bool upTrigger;
    private bool downTrigger;
    private bool leftTrigger;
    private Rigidbody2D rb;
    //private float thrust = 1000f;
   // private Vector2 thrust;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();	
	}
	
	// Update is called once per frame
	void Update () {
		if(rightTrigger)
        {
            goRight();
            rightTrigger = false;
        }
        if(upTrigger)
        {
            goUp();
            upTrigger = false;
        }
        if(downTrigger)
        {
            goDown();
            downTrigger = false;
        }
        if (leftTrigger)
        {
            goLeft();
            leftTrigger = false;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        
        if(col.gameObject.tag == "rightTrigger")
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

    }

    void goRight()
    {
        rb.AddForce(new Vector2(100.0f, 0),ForceMode2D.Impulse );
    }
    void goUp()
    {
        rb.AddForce(new Vector2(0, 100.0f), ForceMode2D.Impulse);
    }
    void goLeft()
    {
        rb.AddForce(new Vector2(-100, 0), ForceMode2D.Impulse);
    }
    void goDown()
    {
        rb.AddForce(new Vector2(0, -100.0f), ForceMode2D.Impulse);
    }
}
