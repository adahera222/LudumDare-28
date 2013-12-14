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

        // Attempt to stop sliding sideways
        Vector2 right = transform.rotation * new Vector2(0,-1);
        Vector2 current = r.velocity;
        float cr = right.x * current.x + right.y * current.y;

        r.AddForce(-cr * right);
 
        // Move
        float v = Input.GetAxis("Vertical");
        if (cs.FUEL >= 1)
        {
            r.AddForce(transform.rotation * new Vector2(v * VELOCITY, 0) * Time.deltaTime);
        }

        // Rotate
        float rot = Input.GetAxis("Horizontal");
        Vector2 forward = transform.rotation * new Vector2(1, 0);
        float dot = forward.x * r.velocity.x + forward.y * r.velocity.y;
        if (dot < 0)
        {
            rot *= -1;
        }
            float peakVelocity = 100 * Time.deltaTime;
            float rotationScalar = Mathf.Pow((r.velocity.magnitude / peakVelocity), (1 - r.velocity.magnitude / peakVelocity));
            transform.Rotate(0, 0, -rot * TURNSPEED * rotationScalar * Time.deltaTime);

        

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
