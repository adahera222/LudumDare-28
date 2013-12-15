using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Location : Objective {

    private Dictionary<int, Vector2> locations;
    private List<string> locationNames;
    private string location;
    private Vector2 destination;
    private GameLogic gl;
    private GameObject g;

    public Location(GameLogic gl, System.Random r)
    {
        this.gl = gl;

        this.locations = gl.getLocations();
        this.locationNames = gl.getLocationNames();

        int i = Mathf.FloorToInt(r.Next(locationNames.Count));
        destination = locations[i];
        location = locationNames[i];

        setDestination();
    }

    private void setDestination()
    {
        g = null;
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Location");

        foreach (GameObject go in objs)
        {
            if (go.transform.position == new Vector3(destination.x, destination.y, 1))
            {
                g = go;
                break;
            }
        }
        SpriteRenderer sr = (SpriteRenderer)g.GetComponent("SpriteRenderer");
        sr.color = new Color(0, 1, 0);
    }
	
	// Update is called once per frame
	override
    public void Update () {
        Vector2 distanceToGoal = gl.getPlayerPosition() - destination;
        if (distanceToGoal.magnitude < 0.5f) 
        {
            gl.completeObjective();
            SpriteRenderer sr = (SpriteRenderer)g.GetComponent("SpriteRenderer");
            sr.color = new Color(1, 1, 1);
        }
	}

    override
    public string getDescription()
    {
        return "Get to the " + location + "!";
    }
}
