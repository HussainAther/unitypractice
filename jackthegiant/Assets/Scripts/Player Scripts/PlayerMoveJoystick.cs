using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveJoystick : MonoBehaviour
{
    public float speed = 8f;
    public float maxVelocity = 4f;

    private Rigidbody2D myBody;
    private Animator anim;
    private bool moveLeft, moveRight;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (moveLeft)
            MoveLeft();
        else if (moveRight)
            MoveRight();
    }

    public void SetMoveLeft(bool moveLeft)
    {
        this.moveLeft = moveLeft;
        this.moveRight = !moveLeft;
    }

    public void StopMoving()
    {
        moveLeft = moveRight = false;
        anim.SetBool("Walk", false);
    }

    private void MoveLeft()
    {
        float forceX = 0f;
        float vel = Mathf.Abs(myBody.velocity.x);
        if (vel < maxVelocity)
            forceX = -speed;

        Vector3 scale = transform.localScale;
        scale.x = -1.3f;
        transform.localScale = scale;

        anim.SetBool("Walk", true);
        myBody.AddForce(new Vector2(forceX, 0));
    }

    private void MoveRight()
    {
        float forceX = 0f;
        float vel = Mathf.Abs(myBody.velocity.x);
        if (vel < maxVelocity)
            forceX = speed;

        Vector3 scale = transform.localScale;
        scale.x = 1.3f;
        transform.localScale = scale;

        anim.SetBool("Walk", true);
        myBody.AddForce(new Vector2(forceX, 0));
    }
}
