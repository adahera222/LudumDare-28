using UnityEngine;
using System.Collections;

public class CarStatus : MonoBehaviour {

    public float HEALTH = 100;
    public float CLEAN = 100;
    public float FUEL = 100;

    public float DAMAGE = 5;
    public float DIRTRATE = 10;
    public float FUELRATE = 100;

    private float lastVelocity = 0;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        // Update speed for collisions
        lastVelocity = gameObject.rigidbody2D.velocity.magnitude;

        // Cleanliness deteriorates over time   
        CLEAN = Mathf.Max(CLEAN - Time.deltaTime / DIRTRATE, 0);

        // Fuel goes down while accelerating
        float v = Input.GetAxis("Vertical");
        FUEL = Mathf.Max(FUEL - Mathf.Abs(v) / FUELRATE, 0);
	}

    void OnGUI()
    {
		GUI.Box (new Rect (10,Screen.height - 60,100,50), "");
        GUI.Label(new Rect(20, Screen.height - 60, 100, 50), "Health: " + (int)HEALTH + "%");
        GUI.Label(new Rect(20, Screen.height - 50, 100, 50), "Fuel: " + (int)FUEL + "%");
        GUI.Label(new Rect(20, Screen.height - 40, 100, 50), "Dirt: " + (100-(int)CLEAN) + "%");
    }

    public void takeDamage()
    {
        HEALTH = Mathf.Max(HEALTH - (lastVelocity - gameObject.rigidbody2D.velocity.magnitude) * DAMAGE, 0);

        if (HEALTH == 0)
        {
            killCar();
        }
    }

    public void killCar()
    {
        // DO FANCY EXPLOSION

        Destroy(gameObject);

        // GO TO GAMEOVER
    }
}
