using UnityEngine;
using System.Collections;

public class Hydrant : MonoBehaviour {

    public ParticleSystem p;
    public GameObject GAMELOGIC;
    public bool isHydrant;
    private float count = 0;

	// Use this for initialization
	void Start () {
        p.Stop();
	}
	
	// Update is called once per frame
	void Update () {

	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player" && p.isStopped)
        {
            AudioHelper.Instance.MakeHydrantSound();
            p.Play();
            if (!isHydrant)
            {
                CarStatus cs = (CarStatus)coll.GetComponent("CarStatus");
                cs.CLEAN -= 20;
            }

            GameLogic gl = (GameLogic)GAMELOGIC.GetComponent("GameLogic");
            Objective o = gl.getCurrentObjective();
            if (o is Hit)
            {
                Hit h = (Hit)o;
                h.OnHit(isHydrant);
            }
        }
    }
}
