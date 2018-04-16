using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour {

    Rigidbody2D rb2d;
    float rot = 0;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update () {

        rb2d.AddForce(-10 * transform.up);


        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            rot+= 1f;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            rot-= 1f;
        }

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            rot -= 1f;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            rot += 1f;
        }

        transform.Rotate(new Vector3(0, 0, rot));
    }
}
