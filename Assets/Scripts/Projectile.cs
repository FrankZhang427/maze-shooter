using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    /*
     * Script for bullets shooted from the player
     * 
     * On collision with any other objects, it will destroy itself
     * 
     **/
     
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision collision)
    {
        // chech if other is not the player, as the bullet is from the inside of player rigidbody
        if (collision.collider.gameObject.tag != "Player")
        {
            Destroy(gameObject);
        }
    }
}
