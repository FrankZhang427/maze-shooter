using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Maze : MonoBehaviour {

    /*
     * Script to create an infinite maze using Eller's algorithm
     * 
     * Arrays of int and bool are used to keep track of current set and cell connections
     * 
     **/
    public GameObject verticalWall;
    public GameObject horizontalWall;
    public GameObject lastWall;
    public GameObject bullets;
    private bool firstRun = true;
    public int counter = 8;
    private int[] cur_set = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };
    private int[] prev_set = new int[] { 0, 0, 0, 0, 0, 0, 0, 0 };
    private bool[] has_vwals = new bool[] { true, true, true, true, true, true, true };
    private bool[] has_hwals = new bool[] { true, true, true, true, true, true, true, true };

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // helper funtion to find maxmium in an int array assuming the array is non-empty
    int MaxInt(int[] a)
    {
        int m = a[0];
        for(int i = 0; i < a.Length; i++)
        {
            if (m < a[i]) m = a[i];
        }
        return m;
    }

    public void CreateInnerVerticalWalls(Transform Spawnpoint)
    {
        // only create horizontal walls after first run
        if (!firstRun)
        {
            // do horizontal walls
            // use hashset to store the set info in the previous row
            HashSet<int> ht = new HashSet<int>(prev_set);
            // the indices that have a horizontal walls
            HashSet<int> hwall_indices = new HashSet<int>();
            // use 2 random number to create at least one connection
            // for each set in the previous row
            foreach (int i in ht)
            {
                HashSet<int> indices = new HashSet<int>();
                for (int j = 0; j < prev_set.Length; j++)
                {
                    if (prev_set[j] == i) indices.Add(j);
                }
                int[] indArray = indices.ToArray();
                int number_hwalls = Random.Range(1, indArray.Length);
                for (int k = 0; k < number_hwalls; k++)
                {
                    int take = Random.Range(k, indArray.Length);
                    int temp = indArray[take];
                    hwall_indices.Add(temp);

                    indArray[take] = indArray[k];
                    indArray[k] = temp;
                }
            }

            // set horizontal walls and set current set 
            // based on the previous row vertical connection
            foreach (int i in hwall_indices)
            {
                has_hwals[i] = false;
                cur_set[i] = prev_set[i];
            }

            // create horizontal walls in the scene
            for (int i = 0; i < has_hwals.Length; i++)
            {
                if (has_hwals[i])
                {
                    Instantiate(horizontalWall, Spawnpoint.position + new Vector3(5f, 0f, (has_hwals.Length - i - 4) * 10f - 5f), Spawnpoint.rotation);
                }
            }

            // fill the current set with the biggest number seen
            for (int i = 0; i < cur_set.Length; i++)
            {
                if (cur_set[i] == 0) cur_set[i] = ++counter;
            }

        }
        // randomly create cell connections in the current row
        for (int i = 0; i < cur_set.Length - 1; i++)
        {
            if (cur_set[i] != cur_set[i + 1] && Random.value > 0.5)
            {
                has_vwals[i] = false;
                cur_set[i + 1] = cur_set[i];
            }
        }
        counter = MaxInt(cur_set);
        // if first run, set wall to be indestructable
        if (firstRun)
        {
            verticalWall.GetComponentInChildren<DestructibleWall>().destructible = false;
        }
        // create vertical walls in the scene
        for (int i = 0; i < has_vwals.Length; i++)
        {
            if (has_vwals[i])
            {
                Instantiate(verticalWall, Spawnpoint.position + new Vector3(0f, 0f, (has_vwals.Length - i - 4) * 10f), Spawnpoint.rotation);
            }
        }
        // reset loop variables and wait for next row to be created
        System.Array.Copy(cur_set, prev_set, cur_set.Length);
        for (int i = 0; i < cur_set.Length; i++) cur_set[i] = 0;
        // set back to normal after first run
        if (firstRun)
        {
            verticalWall.GetComponentInChildren<DestructibleWall>().destructible = true;
            firstRun = false;
        }
        for (int i = 0; i < has_vwals.Length; i++) has_vwals[i] = true;
        for (int i = 0; i < has_hwals.Length; i++) has_hwals[i] = true;
    }

    // method to create the last row in the maze
    public void CreateLastRow(Transform Spawnpoint)
    {
        Instantiate(lastWall, Spawnpoint.position, Spawnpoint.rotation);
    }
}
