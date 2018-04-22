using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {

    // holds door to open
    // can change to array if we want it to affect multiple objects
    public GameObject target;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            target.gameObject.GetComponent<Collider>().enabled = false;
            target.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }

    }
}
