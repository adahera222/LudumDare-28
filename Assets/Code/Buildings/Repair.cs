﻿using UnityEngine;
using System.Collections;

public class Repair : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            CarStatus cs = (CarStatus)coll.gameObject.GetComponent("CarStatus");
            cs.HEALTH = 100;
            Debug.Log("Car repaired! " + cs.HEALTH);
        }
    }
}