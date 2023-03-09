using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 8f;
    public float maxVelocity = 4f;

    private Rigidbody2D myBody;
    private Animator anim;
    private bool isMoving = false;
    
    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        float forceX = 0f;
        float vel = Mathf.Abs(myBody.velocity.x);

        // zwraca -1 - lewo 0 - nic nie kliknieto 1 - prawo
        float keyFlag = Input.GetAxisRaw("Horizontal");

        // sklala x ujemna oznacza odwrocenie sprite a wiec i animacji
        Vector3 scale = transform.localScale;

        if (keyFlag > 0)
        {
            if (vel < maxVelocity)
                forceX = speed;

            scale.x = 1.3f;
            transform.localScale = scale;
            isMoving = true;
            anim.SetBool("Walk", true);
        }
        else if (keyFlag < 0)
        {
            if (vel < maxVelocity)
                forceX = -speed;

            scale.x = -1.3f;
            transform.localScale = scale;
            isMoving = true;
            anim.SetBool("Walk", true);
          
        }
        else
        {
            if (isMoving)
            {
                isMoving = false;
                anim.SetBool("Walk", false);
            }
        }
        myBody.AddForce(new Vector2(forceX, 0));
    }
}
