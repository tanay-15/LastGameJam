﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PinBallScript : MonoBehaviour
{

    Rigidbody rb;
    float rot = 0;

    public AudioSource MusicSource;

    public AudioClip[] MusicClips;

    [SerializeField] GameObject coinParticlesPrefab;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position.y < -50)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (rb.velocity.magnitude < 12)
            rb.AddForce(-10 * transform.forward * Time.deltaTime * 60f);


        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            rot += 2f;
            //SoundChange(1);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            rot -= 2f;
            //SoundChange(1);
        }

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            rot -= 2f;
            //SoundChange(1);
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            rot += 2f;
            //SoundChange(1);
        }

        Vector3 eulerRotation = new Vector3(0, rot * Time.deltaTime * 60f, 0);
        transform.Rotate(eulerRotation);
        Quaternion rotation = Quaternion.Euler(eulerRotation);
        rb.velocity = rotation * rb.velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bumper")
        {
            rb.AddForce(new Vector3(rb.transform.forward.x, rb.transform.forward.y, rb.transform.forward.z) * 20, ForceMode.Impulse);
            collision.gameObject.transform.localScale = new Vector3(collision.gameObject.transform.localScale.x / 2, collision.gameObject.transform.localScale.y, collision.gameObject.transform.localScale.z / 2);
            //collision.gameObject.renderer.material.color = Color.white;
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Bumper")
        {
            collision.gameObject.transform.localScale = new Vector3(collision.gameObject.transform.localScale.x * 2, collision.gameObject.transform.localScale.y, collision.gameObject.transform.localScale.z * 2);
        }

    }
}
