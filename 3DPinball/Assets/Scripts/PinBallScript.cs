using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PinBallScript : MonoBehaviour
{
    public static float mouseSensitivity = 6f;
    public static bool invertedMouse = false;
    Rigidbody rb;
    float rot = 0;

    public AudioSource MusicSource;

    public AudioClip[] MusicClips;

    [SerializeField] GameObject coinParticlesPrefab;

    static PinBallScript()
    {
        mouseSensitivity = 6f;
        invertedMouse = false;
    }

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        rot = 0;
        if (transform.position.y < -50)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (rb.velocity.magnitude < 12)
            rb.AddForce(-10 * transform.forward * Time.deltaTime * 60f);


        rot = -Input.GetAxis("Mouse X") * mouseSensitivity * ((invertedMouse) ? -1 : 1);
        //if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    rot = 2f;
        //    //SoundChange(1);
        //}
        //if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    rot = -2f;
        //    //SoundChange(1);
        //}

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("TitleScreen");
        }

        Vector3 eulerRotation = new Vector3(0, rot * Time.deltaTime * 60f, 0);
        //transform.Rotate(eulerRotation);
        Quaternion rotation = Quaternion.Euler(eulerRotation);
        //rb.rotation = rotation * rb.rotation;
        rb.MoveRotation(rb.rotation * rotation);
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
