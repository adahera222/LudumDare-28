using UnityEngine;
using System.Collections;

public class CarStatus : MonoBehaviour {

    public float HEALTH = 100;
    public float CLEAN = 100;
    public float FUEL = 100;

    public GUIText h;
    public GUIText f;
    public GUIText d;

    public ParticleSystem explosion;
    public GameLogic gl;

    public float DAMAGE = 5;
    public float DIRTRATE = 10;
    public float FUELRATE = 100;

    private float lastVelocity = 0;
    private bool gameOver = false;

	// Use this for initialization
	void Start () {
        explosion.Stop();
	}
	
	// Update is called once per frame
	void Update () {
        if (!gameOver) {
            // Update speed for collisions
            lastVelocity = gameObject.rigidbody2D.velocity.magnitude;

            // Cleanliness deteriorates over time   
            CLEAN = Mathf.Max(CLEAN - Time.deltaTime / DIRTRATE, 0);

            // Fuel goes down while accelerating
            float v = Input.GetAxis("Vertical");
            FUEL = Mathf.Max(FUEL - Mathf.Abs(v) / FUELRATE, 0);
            if (FUEL < 1)
            {
                gl.GameOver("You ran out of fuel.");
            }

            h.text = "Health: " + (int)HEALTH + "%";
            f.text = "Fuel: " + (int)FUEL + "%";
            d.text = "Dirt: " + (100-(int)CLEAN) + "%";
        } else if (explosion.isStopped) {
            renderer.enabled = false;
            gl.GameOver("Your car exploded, you might have noticed."); ;
        }

        
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
        rigidbody2D.isKinematic = true;
        AudioHelper.Instance.MakeExplosionSound();
        explosion.Play();
        gameOver = true;
    }
}
