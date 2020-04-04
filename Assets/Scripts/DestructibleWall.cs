using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleWall : MonoBehaviour {

    /*
     * Script for inner walls in the maze
     * 
     * Each wall has 3 hit points. Walls can be destructible or not.
     * The vertical walls in the first roll are indestructible.
     * 
     **/
    public int hp = 3;
    public bool destructible = true;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet" && destructible)
        {
            hp--;
            //Destroy(collision.collider.gameObject);
            if (hp <= 0)
            {
                Destroy(transform.parent.gameObject);
            }
        }
    }

}
