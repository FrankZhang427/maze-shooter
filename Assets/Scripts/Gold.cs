using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour {

    /*
     *  Script for the goal in the opposite terrain
     *  
     *  The golden coin is rotating on its own.
     *  If the player reachs the coin, it gets destroyed and game over.
     *  
     **/
    private float rotateSpeed = 5f;
    GameManagerScript GMS;
    // Use this for initialization
    void Start () {
        GMS = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
    }
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.left * rotateSpeed);
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            GMS.GameOver();
        }
    }
}
