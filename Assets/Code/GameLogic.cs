using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour {

    public GameObject player;

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
            float rand = r.Next(4);
            if (rand < 1)
            {
                currentObjective = new Location(this);
                timer += 30;
                Debug.Log(currentObjective.getDescription());
            }
            else if (rand < 2)
            {

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
        return new Vector2(player.transform.position.x, player.transform.position.y);
    }
}
