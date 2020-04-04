using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour {

    /*
     * Script for the game manager
     * 
     * Game manager has control over :
     * (1) the maze, which can generate infinte maze using Eller's algorithm
     * (2) the player, which is equipped with a first person perspective camera
     * (3) the game canvas, which will appear if the player wins
     * 
     **/
    public Maze maze;
    public PlayerMovement player;
    public GameObject gameOverPanel;
    public Text gameOverText;

    void Awake()
    {
        // Hide the canvas at start
        gameOverPanel.SetActive(false);
    }

    // Use this for initialization
    void Start() {
        maze = GameObject.Find("Maze").GetComponent<Maze>();
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    /* 
     * method to create the inner walls inside the maze
     **/
    public void CreateMazeInnerWalls(GameObject Prefab, Transform Spawnpoint)
    {
        // create the outer walls
        Instantiate(Prefab, Spawnpoint.position, Spawnpoint.rotation);
        // create bullets on each cell
        Instantiate(maze.bullets, Spawnpoint.position, Spawnpoint.rotation);
        // create the inner walls
        maze.CreateInnerVerticalWalls(Spawnpoint);
    }
    /* 
     * method to create the last row inside the maze
     **/
    public void CreateMazeLastRow(GameObject Prefab, Transform Spawnpoint)
    {
        maze.CreateLastRow(Spawnpoint);
    }

    /* 
    * method to increase the projectile that the player has
    **/
    public void AddAmmo()
    {
        player.AddAmmo();
    }

    /* 
    * method to finish the game
    **/
    public void GameOver()
    {
        // show the canvas
        gameOverPanel.SetActive(true);
        gameOverText.text = "You Win!";
        // freeze the player by seting win flag
        player.win = true;
        // quit the application
        Application.Quit();
    }
}
