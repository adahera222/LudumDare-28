using UnityEngine;
using System.Collections;

public class CarScript : MonoBehaviour {
    public float VELOCITY = 50;
    public float TURNSPEED = 50;
    public ParticleSystem SMOKE;

    private Rigidbody2D r;
    private bool gameOver;

    private float audioCounter = 0;

	// Use this for initialization
	void Start () {
        r = gameObject.rigidbody2D;
	}
	
	// Update is called once per frame
	void Update () {
        // Mute Audio
        if (Input.GetKeyDown(KeyCode.M)) {
            AudioListener.pause = !AudioListener.pause;
        }
        

        // Get status of car
        CarStatus cs = (CarStatus)gameObject.GetComponent("CarStatus");

        // Attempt to stop sliding sideways
        Vector2 right = transform.rotation * new Vector2(0,-1);
        Vector2 current = r.velocity;
        float cr = right.x * current.x + right.y * current.y;

        r.AddForce(-cr * right);

        if (gameOver)
        {
            return;
        }
 
        // Move
        float v = Input.GetAxis("Vertical");
        if (cs.FUEL >= 1)
        {
            r.AddForce(transform.rotation * new Vector2(v * VELOCITY, 0) * Time.deltaTime);
            if (v != 0)
            {
                if (!audio.isPlaying)
                {
                    audio.Play();
                }
                else if (audioCounter < 1.1f) 
                {
                    audio.pitch += 0.01f;
                    audioCounter += Time.deltaTime;
                }
            }
            else if (r.velocity.magnitude > 0.75f)
            {
                if (audio.isPlaying && audioCounter >= 0)
               {
                    audio.pitch -= 0.01f;
                    audioCounter -= Time.deltaTime;
                }
            }
            else
            {
                audio.Stop();
                audio.pitch = 0.5f;
                audioCounter = 0;
            }
        }

        // Screech
        if (r.velocity.magnitude > 0)
        {
            float ang = Vector2.Angle(r.velocity, transform.rotation * new Vector2(1, 0));
            Debug.Log(ang);
            if (ang > 67.5 && ang < 112.5)
            {
                AudioHelper.Instance.MakeScreechSound();
            }
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

    public void GameOver()
    {
        audio.Stop();
        gameOver = true;
        SMOKE.enableEmission = false;
    }
}
