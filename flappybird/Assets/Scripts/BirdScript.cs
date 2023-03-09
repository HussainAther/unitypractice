using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirdScript : MonoBehaviour
{
    public static BirdScript instance;
    
    [SerializeField] private Rigidbody2D rigitBody;
    [SerializeField] private Animator anim;

    private float forwardSpeed = 3f;
    private float bounceSpeed = 4f;

    private bool didFlap;
    public bool isAlive;
    public bool gameStarted = false;

    private Button flapButton;
    public int score = 0;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip flapClip, pointClip, diedClip;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        isAlive = true;
        flapButton = GameObject.FindGameObjectWithTag("FlapButton").GetComponent<Button>();
        flapButton.onClick.AddListener(() => FlapTheBird());
        SetCamerasXOffset();
        //SetRigidbodyType(RigidbodyType2D.Kinematic);
    }

    private void FixedUpdate()
    {
        if(isAlive && gameStarted)
        {
            Vector3 temp = transform.position;
            temp.x += forwardSpeed * Time.deltaTime;
            transform.position = temp;

            if(didFlap)
            {
                didFlap = false;
                rigitBody.velocity = new Vector2(0, bounceSpeed);
                audioSource.PlayOneShot(flapClip);
                anim.SetTrigger("Flap");
            }

            if(rigitBody.velocity.y >= 0)
                transform.rotation = Quaternion.Euler(0, 0, 0);
            else
            {
                float angle = 0;
                angle = Mathf.Lerp(0, -90, -rigitBody.velocity.y / 10);
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }
    }

    public void SetCamerasXOffset()
    {
        CameraScript.offsetX = (Camera.main.transform.position.x - transform.position.x) -1f;
    }

    public float GetXPosition()
    {
        return transform.position.x;
    }

    public void FlapTheBird()
    {
        //Debug.Log("Flap");
        didFlap = true;
    }

    /*
    public void SetRigidbodyType(RigidbodyType2D type)
    {
        rigitBody.bodyType = type;
    }
    */

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Ground" ||
            target.tag == "Pipe")
        {
            //Debug.Log("Pipe");
            if (isAlive)
            {
                isAlive = false;
                anim.SetTrigger("Bird died");
                audioSource.PlayOneShot(diedClip);
                GameplayController.instance.PlayerDiedShowScore(score);
            }
        }
        else if (target.tag == "PipeHolder")
        {
            score++;
            audioSource.PlayOneShot(pointClip);
            GameplayController.instance.SetScore(score);
        }
    }
}
