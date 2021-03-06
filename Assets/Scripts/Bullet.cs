﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    GameManagerScript GMS;
    // Use this for initialization
    void Start()
    {
        GMS = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GMS.AddAmmo();
            Destroy(this.gameObject);
        }
    }
}
