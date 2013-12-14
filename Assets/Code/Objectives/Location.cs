using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Location : Objective {

    private GameObject GOAL;
    private Dictionary<int, Vector2> d = new Dictionary<int,Vector2>();
    private string[] locationNames = { "Hospital", "Car Park" };
    private string location;
    private Vector2 destination;
    private GameLogic gl;
    private GameObject g;
    private System.Random r;

    public Location(GameLogic gl, GameObject GOAL, System.Random r)
    {
        this.gl = gl;
        this.GOAL = GOAL;

        d.Add(0, new Vector2(0, 4));
        d.Add(1, new Vector2(3, 4));

        this.r = r;

        int i = Mathf.FloorToInt(r.Next(2));
        destination = d[i];
        location = locationNames[i];

        g = (GameObject)Object.Instantiate(GOAL);
        g.transform.position = new Vector3(destination.x, destination.y, -2);
    }
	
	// Update is called once per frame
	override
    public void Update () {
        Vector2 distanceToGoal = gl.getPlayerPosition() - destination;
        if (distanceToGoal.magnitude < 0.5f) 
        {
            gl.completeObjective();
            Object.Destroy(g);
        }
	}

    override
    public string getDescription()
    {
        return "Get to the " + location + " at " + destination.ToString();;
    }
}
