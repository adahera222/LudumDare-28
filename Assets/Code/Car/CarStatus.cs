using UnityEngine;
using System.Collections;

public class CarStatus : MonoBehaviour {

    public float HEALTH = 100;
    public float CLEAN = 100;
    public float FUEL = 100;
    public float[] TYRES = {100,100,100,100};
    public float DAMAGE = 5;

    private float lastVelocity = 0;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        // Update speed for collisions
        lastVelocity = gameObject.rigidbody2D.velocity.magnitude;

        // Cleanliness deteriorates over time   
        CLEAN -= Time.deltaTime;

        // Fuel goes down while accelerating
        float v = Input.GetAxis("Vertical");
        FUEL -= v;

        // Tyres wear while turning 
        float turn = Input.GetAxis("Horizontal");
        if (turn > 0) {
            TYRES[0] -= turn;
            TYRES[3] -= turn;
        } else if (turn < 0) {
            TYRES[1] -= turn;
            TYRES[2] -= turn;
        }
	}

    public void takeDamage()
    {
        HEALTH -= (lastVelocity - gameObject.rigidbody2D.velocity.magnitude) * DAMAGE;
        Debug.Log(HEALTH);
    }
}
