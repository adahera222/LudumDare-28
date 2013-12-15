using UnityEngine;
using System.Collections;

public class Repair : MonoBehaviour {
    private GameLogic GAMELOGIC;

	// Use this for initialization
	void Start () {
        GameObject gl = GameObject.FindGameObjectWithTag("GameLogic");
        GAMELOGIC = (GameLogic)gl.GetComponent("GameLogic");
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
            GAMELOGIC.addToQueue("Car repaired!");
        }
    }
}
