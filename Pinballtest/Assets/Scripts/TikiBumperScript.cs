using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TikiBumperScript : MonoBehaviour {

    float rot = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        rot += 0.01f;
        transform.Rotate(new Vector3(0,0,rot));
	}
}
