using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Location : Objective {

    private Dictionary<int, Vector2> d = new Dictionary<int,Vector2>();
    private string[] locationNames = { "Hospital", "Car Park" };
    private string location;
    private Vector2 destination;
    private GameLogic gl;

    public Location(GameLogic gl)
    {
        this.gl = gl;

        d.Add(0, new Vector2(2, 2));
        d.Add(1, new Vector2(4, 4));

        System.Random r = new System.Random();

        int i = Mathf.FloorToInt(r.Next(2));
        destination = d[i];
        location = locationNames[i];
    }
	
	// Update is called once per frame
	override
    public void Update () {
        Vector2 distanceToGoal = gl.getPlayerPosition() - destination;
        if (distanceToGoal.magnitude < 1) 
        {
            gl.completeObjective();
        }
	}

    override
    public string getDescription()
    {
        return "Get to the " + location + " at " + destination.ToString();;
    }
}
