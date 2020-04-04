using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMaze : MonoBehaviour {

    /*
     * Script for the "air walls" that triggers the next row in the maze
     * 
     **/
    public Transform Spawnpoint;
    public GameObject Prefab;
    GameManagerScript GMS;

    // Initialization
    void Start()
    {
        GMS = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
    }

    void OnTriggerEnter(Collider other)
    {
        // if the player passes throught the air wall
        // create a new row in the maze
        if (other.gameObject.tag == "Player")
        {
            GMS.CreateMazeInnerWalls(Prefab, Spawnpoint);
            Destroy(this.gameObject);
        }
        // if a bullet passes throught the air wall
        // create the last row in the maze
        else if (other.gameObject.tag == "Bullet")
        {
            // Destroy(other.gameObject);
            GMS.CreateMazeLastRow(Prefab, Spawnpoint);
            Destroy(this.gameObject);
        }
    }
}
