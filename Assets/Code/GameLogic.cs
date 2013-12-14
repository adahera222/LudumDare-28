using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour {

    public GameObject PLAYER;
    public GameObject GOAL;

    private Objective currentObjective;
    private System.Random r;

    private float timer;

	// Use this for initialization
	void Start () {
        r = new System.Random();
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;

        if (currentObjective == null)
        {
            float rand = r.Next(2);
            if (rand < 1)
            {
                currentObjective = new Location(this, GOAL, r);
                timer += 30;
                Debug.Log(currentObjective.getDescription());
            }
            else if (rand < 2)
            {
                currentObjective = new Hit(this, r);
                timer += 30;
                Debug.Log(currentObjective.getDescription());
            }
            else if (rand < 3)
            {

            }
            else if (rand < 4)
            {

            }
        }
        else
        {
            currentObjective.Update();
        }
	}

    public void completeObjective()
    {
        currentObjective = null;
        Debug.Log("Objective Complete!");
    }

    public Vector2 getPlayerPosition()
    {
        return new Vector2(PLAYER.transform.position.x, PLAYER.transform.position.y);
    }

    public Objective getCurrentObjective()
    {
        return currentObjective;
    }
}
