using UnityEngine;
using System.Collections;

public class Dude : MonoBehaviour {

    private GameObject player;
    private GameLogic GAMELOGIC;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 toPlayer = player.transform.position - transform.position;
        float distanceToPlayer = toPlayer.magnitude;
        if (distanceToPlayer < 0.1)
        {
            Destroy(gameObject);
            GameLogic gl = (GameLogic)GAMELOGIC.GetComponent("GameLogic");
            Objective o = gl.getCurrentObjective();
            if (o is Taxi)
            {
                Taxi t = (Taxi)o;
                t.OnGetIn();
            }
        }
        else if (player.rigidbody2D.velocity.magnitude < 1 && distanceToPlayer < 1)
        {
            transform.Translate((player.transform.position-transform.position) / 30);
        }
	}

    public void setGameLogic(GameLogic gl)
    {
        this.GAMELOGIC = gl;
    }
}
