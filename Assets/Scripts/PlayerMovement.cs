using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    /*
     * Script for player controller
     * 
     * An epslon parameter is used to control whether or not the player is jumping
     * by checking if current y position is in the range of [y0 - epslon, y0 + epslon]
     * 
     * The bullet prefab is also stored in the controller
     * 
     **/
    public float moveSpeed = 5f;
    private float epslon = 0.5f;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public int cur_bullets = 0;
    public bool win;

    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        win = false;
    }

    // Update is called once per frame
    void Update () {
        if (win) return;

        // control the movement by WASD
        float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float moveZ = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        // move
        if (this.gameObject.GetComponent<Rigidbody>().position.y < 5f + epslon && this.gameObject.GetComponent<Rigidbody>().position.y > 5f - epslon)
        {
            transform.Translate(moveX, 0f, moveZ);
        }

        // control jumping by Space
        if (Input.GetKeyDown("space") && this.gameObject.GetComponent<Rigidbody>().position.y < 5f + epslon && this.gameObject.GetComponent<Rigidbody>().position.y > 5f - epslon)
        {
            this.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 1000f);
        }

        // control the fire by F
        if (Input.GetKeyDown(KeyCode.F))
        {
            Fire();
        }
    }

    // method to fire a bullet
    void Fire()
    {
        if (win) return;
        // check if the player has bullets and is not jumping
        if (cur_bullets <= 0 || !(this.gameObject.GetComponent<Rigidbody>().position.y < 5f + epslon && this.gameObject.GetComponent<Rigidbody>().position.y > 5f - epslon)) return;
        else
        {
            // Create the Bullet from the Bullet Prefab
            var bullet = (GameObject)Instantiate(
                bulletPrefab,
                bulletSpawn.position,
                bulletSpawn.rotation);

            // Add velocity to the bullet
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 30;

            // Destroy the bullet after 2 seconds for testing purpose
            // Destroy(bullet, 2.0f);
            cur_bullets--;
        }
    }

    // method to increase the number of bullets
    public void AddAmmo()
    {
        if (win) return;
        cur_bullets++;
    }
}
