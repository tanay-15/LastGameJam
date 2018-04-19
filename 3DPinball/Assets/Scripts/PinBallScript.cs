using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinBallScript : MonoBehaviour
{

    Rigidbody rb2d;
    float rot = 0;

    public AudioSource MusicSource;

    public AudioClip[] MusicClips;

    [SerializeField] GameObject coinParticlesPrefab;

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        if(rb2d.velocity.magnitude < 12)
            rb2d.AddForce(-10 * transform.forward * Time.deltaTime * 60f);


        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            rot += 1f;
            //SoundChange(1);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            rot -= 1f;
            //SoundChange(1);
        }

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            rot -= 1f;
            //SoundChange(1);
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            rot += 1f;
            //SoundChange(1);
        }

        Vector3 eulerRotation = new Vector3(0, rot * Time.deltaTime * 60f, 0);
        transform.Rotate(eulerRotation);
        Quaternion rotation = Quaternion.Euler(eulerRotation);
        rb2d.velocity = rotation * rb2d.velocity;
    }
   
}
