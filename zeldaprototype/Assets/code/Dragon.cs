using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour {

    Animator anim;
    public float speed;
    int dir;
    float timer = 0.8f;
    public float health;
    public GameObject particles;
    bool canAttack;
    public float attackTimer = 2f;
    public GameObject projectile;
    public float thrust;

    // Use this for initialization
    void Start() {
        anim = GetComponent<Animator>();
        dir = Random.Range(0, 4);
        canAttack = false;
    }

    void movement()
    {
        if(dir==0)
        {
        transform.Translate(0,speed* Time.deltaTime,0); 
        anim.SetInteger("dir",0);
        }
        else if(dir==1)
        {
        transform.Translate(0,-speed* Time.deltaTime,0);
        anim.SetInteger("dir",1);
        }
        else if(dir==2)
        {
        transform.Translate(-speed* Time.deltaTime,0,0);
        anim.SetInteger("dir",2);
        }
        else
        {
        transform.Translate(speed* Time.deltaTime,0,0);
        anim.SetInteger("dir",3);
        }

    }

	// Update is called once per frame
	void Update () {
    movement();

        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            timer = 0.7f;
            dir = Random.Range(0, 4);
        }

        attackTimer -= Time.deltaTime;
        if(attackTimer<=0)
        {
            attackTimer = 2f;
            canAttack = true;
        }

        attack();
	}

    void attack()
    {
        if (!canAttack)
            return;

        canAttack = false;
        GameObject project = Instantiate(projectile, transform.position, transform.rotation);
        if(dir==0)
        {
            project.GetComponent<Rigidbody2D>().AddForce(Vector2.up *thrust);
        }
        else if (dir == 1)
        {
            project.GetComponent<Rigidbody2D>().AddForce(-Vector2.up * thrust);
        }
        else if (dir == 2)
        {
            project.GetComponent<Rigidbody2D>().AddForce(-Vector2.right * thrust);
        }
        else
        {
            project.GetComponent<Rigidbody2D>().AddForce(Vector2.right * thrust);
        }

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Sword")
        {
            health--;
            if (health <= 0)
            {
                Instantiate(particles, transform.position, transform.rotation);
                Destroy(gameObject);
            }
            col.GetComponent<Sword>().createParticle();
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().canAttack = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().canMove = true;

            Destroy(col.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (!col.gameObject.GetComponent<Player>().iniFrames)
            {
                col.gameObject.GetComponent<Player>().currentHP--;
                col.gameObject.GetComponent<Player>().iniFrames = true;
            }
            health--;

            if (health <= 0)
            {
                Instantiate(particles, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }

        if (col.gameObject.tag == "Wall")
        {
            Random.Range(0, 3);
        }
    }
}
