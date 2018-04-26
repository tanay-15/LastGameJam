using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorChange : MonoBehaviour {
    Renderer rend;
    bool changeColor = false;
    public Material finalColor;
	// Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();
       
		
	}
	
	// Update is called once per frame
	void Update () {
		if(changeColor)
        {

            rend.sharedMaterial = finalColor;
            changeColor = false;
        }
	}
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("changing color");
            changeColor = true;

        }
    }
}
