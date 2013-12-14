using UnityEngine;
using System.Collections;

public class Hydrant : MonoBehaviour {

    public ParticleSystem WATER;
    public GameObject GAMELOGIC;

	// Use this for initialization
	void Start () {
        WATER.Stop();
	}
	
	// Update is called once per frame
	void Update () {

	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player" && WATER.isStopped)
        {
            WATER.Play();
            GameLogic gl = (GameLogic)GAMELOGIC.GetComponent("GameLogic");
            Objective o = gl.getCurrentObjective();
            if (o is Hit)
            {
                Hit h = (Hit)o;
                h.OnHit();
            }
        }
    }
}
