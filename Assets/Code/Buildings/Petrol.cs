using UnityEngine;
using System.Collections;

public class Petrol : MonoBehaviour {

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
            cs.FUEL = 100;
            Debug.Log("Fuel refilled! " + cs.FUEL);
        }
    }
}
