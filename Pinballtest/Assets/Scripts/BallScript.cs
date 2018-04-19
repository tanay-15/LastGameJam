using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour {

    Rigidbody2D rb2d;
    float rot = 0;

	public AudioSource MusicSource;

	public AudioClip[] MusicClips;

	[SerializeField] GameObject coinParticlesPrefab;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update () {

        rb2d.AddForce(-10 * transform.up * Time.deltaTime * 60f);


        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            rot+= 1f;
			SoundChange (1);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            rot-= 1f;
			SoundChange (1);
        }

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            rot -= 1f;
			SoundChange (1);
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            rot += 1f;
			SoundChange (1);
        }

        Vector3 eulerRotation = new Vector3(0, 0, rot * Time.deltaTime * 60f);
        transform.Rotate(eulerRotation);
        Quaternion rotation = Quaternion.Euler(eulerRotation);
        rb2d.velocity = rotation * rb2d.velocity;
    }

	void SoundChange(int temp)
	{
		MusicSource.clip = MusicClips [temp];

		MusicSource.Play ();
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Obstacle") 
		{
			SoundChange (2);
			Debug.Log ("Sound Changed");
		}

		if (other.tag == "Coin") 
		{
			SoundChange (3);
			Debug.Log ("Sound Changed");
            //Destroying the ball and generating particles are handled in Ball.cs
			//Destroy (other.gameObject);
			//Instantiate(coinParticlesPrefab, other.gameObject.transform.position, other.gameObject.transform.rotation);
		}
	}
}
