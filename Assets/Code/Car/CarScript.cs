using UnityEngine;
using System.Collections;

public class CarScript : MonoBehaviour {
    public float VELOCITY = 50;
    public float TURNSPEED = 50;
    public ParticleSystem SMOKE;

    private Rigidbody2D r;

	// Use this for initialization
	void Start () {
        r = gameObject.rigidbody2D;
	}
	
	// Update is called once per frame
	void Update () {
        // Get status of car
        CarStatus cs = (CarStatus)gameObject.GetComponent("CarStatus");

        // Move
        float v = Input.GetAxis("Vertical");
        if (cs.FUEL >= 1)
        {
            r.AddForce(transform.rotation * new Vector2(v * VELOCITY, 0) * Time.deltaTime);
        }

        // Rotate
        float rot = Input.GetAxis("Horizontal");
        if (v < 0)
        {
            rot *= -1;
        }
        transform.Rotate(0,0, -rot * TURNSPEED * r.velocity.magnitude * Time.deltaTime);

        

        if (r.velocity.magnitude > 1)
        {
            SMOKE.enableEmission = true;
        }
        else
        {
            SMOKE.enableEmission = false;
        }

	}
}
