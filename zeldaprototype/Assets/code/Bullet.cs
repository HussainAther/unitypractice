using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    // Use this for initialization
    public GameObject particles;
    public float timer = 2f;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            createParticle();
            Destroy(gameObject);
        }
	}

    public void createParticle()
    {
        Instantiate(particles, transform.position, transform.rotation);
    }
}
