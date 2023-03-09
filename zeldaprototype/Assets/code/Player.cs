using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    Animator ani;
    public float speed;
    public Image[] hearts;
    public int maxHP;
    public int currentHP;
    public GameObject sword;
    public int thrust;
    public bool canMove,canAttack;
    public bool iniFrames;
    SpriteRenderer sr;
    public float iniTimer = 1f;
    private bool visible;

	// Use this for initialization
	void Start () {
        ani = GetComponent<Animator>();
        currentHP = maxHP;
        updateHP();
        canMove = true;
        canAttack = true;
        iniFrames = false;
        sr = GetComponent<SpriteRenderer>();
        visible = false;
    }

    void updateHP()
    {
        //set all false 
        for(int i=0;i<hearts.Length;i++)
        {
            hearts[i].gameObject.SetActive(false);
        }

        //set the required one's true
        for (int i = 0; i < currentHP; i++)
        {
            hearts[i].gameObject.SetActive(true);
        }
    }
		
	// Update is called once per frame
	void Update () {
        movement();
        updateHP();

        if (Input.GetKeyDown(KeyCode.Space))
            attack();

        if(iniFrames==true)
        {
            iniTimer -= Time.deltaTime;

            visible ^= true;

            if (!visible)
                sr.enabled = false;
            else
                sr.enabled = true;

            if(iniTimer<=0)
            {
                iniTimer = 1f;
                iniFrames = false;
                sr.enabled = true;
            }
        }
	}

    void attack()
    {
        if (!canAttack)
            return;

        canMove = false;
        canAttack = false;
        GameObject newSword = Instantiate(sword, transform.position, transform.rotation);
        newSword.GetComponent<Sword>().ranged = false;
        thrust = 500;

        if (currentHP==maxHP)
        {
            newSword.GetComponent<Sword>().ranged = true;
            canMove = true;
            thrust = 750;
        }
        
        //rotate sword
        int swordDir = ani.GetInteger("dir");
        ani.SetInteger("attackDir", swordDir);

        switch (swordDir)
        {
            case 0:
                newSword.transform.Rotate(0, 0, 0);
                newSword.GetComponent<Rigidbody2D>().AddForce(Vector2.up * thrust);
                break;

            case 1:
                newSword.transform.Rotate(0, 0, 180);
                newSword.GetComponent<Rigidbody2D>().AddForce(Vector2.down * thrust);
                break;

            case 2:
                newSword.transform.Rotate(0, 0, 90);
                newSword.GetComponent<Rigidbody2D>().AddForce(Vector2.left * thrust);
                break;

            case 3:
                newSword.transform.Rotate(0, 0, -90);
                newSword.GetComponent<Rigidbody2D>().AddForce(Vector2.right * thrust);
                break;

            default:
                break;
        }


    }

    void movement()
    {
        if (!canMove)
            return;

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0, speed * Time.deltaTime, 0);
            ani.SetInteger("dir", 0);
            ani.speed = 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0);
            ani.SetInteger("dir", 2);
            ani.speed = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, -speed * Time.deltaTime, 0);
            ani.SetInteger("dir", 1);
            ani.speed = 1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
            ani.SetInteger("dir", 3);
            ani.speed = 1;
        }
        else
        {
            ani.speed = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag =="EnemyBullet")
        {
            if (!iniFrames)
            {
                currentHP--;
                iniFrames = true;
            }

            col.gameObject.GetComponent<Bullet>().createParticle();
            Destroy(col.gameObject);
        }
    }
}
