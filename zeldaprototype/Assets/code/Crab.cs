using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crab : MonoBehaviour {

    public int health;
    public GameObject particles;
    public Sprite up, down, left, right;
    SpriteRenderer spriteRender;
    int dir;
    float timer = 2f;
    public float speed;

	// Use this for initialization
	void Start () {
        spriteRender = GetComponent<SpriteRenderer>();
        dir = Random.Range(0,4);
    }
	
    void movement()
    {
        Debug.Log(dir);
        if(dir==0)
        {
            transform.Translate(0, speed * Time.deltaTime, 0);
            spriteRender.sprite = up;
        }
        else if (dir == 1)
        {
            transform.Translate(0, -speed * Time.deltaTime, 0);
            spriteRender.sprite = down;
        }
        else if (dir == 2)
        {
            transform.Translate(-speed * Time.deltaTime, 0,0);
            spriteRender.sprite = left;
        }
        else
        {
            transform.Translate(speed * Time.deltaTime,0, 0);
            spriteRender.sprite = right;
        }
    }
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
            
        if(timer<=0)
        {
            timer = 1.5f;
            dir = Random.Range(0, 3);
        }
        movement();
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

            Destroy(col.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag=="Player")
        {
            if (!col.gameObject.GetComponent<Player>().iniFrames)
            {
                col.gameObject.GetComponent<Player>().currentHP--;
                col.gameObject.GetComponent<Player>().iniFrames = true;
            }
            health--;
            
            if(health<=0)
            {
                Instantiate(particles, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }

        if(col.gameObject.tag=="Wall")
        {
            Random.Range(0, 3);
        }
    }
}
