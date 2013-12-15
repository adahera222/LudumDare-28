using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Taxi : Objective {
    private Dictionary<int, Vector2> locations;
    private List<string> locationNames;
    private string location;
    private Vector2 destination;
    private System.Random r;
    private GameObject g;
    private GameLogic gl;
    private bool VIPCollected = false;
    private int i = 0;
    private CarStatus cs;
    private GameObject DUDE;
    private bool dudeCreated = false;

    public Taxi(GameLogic gl, System.Random r, CarStatus cs, GameObject DUDE)
    {
        this.gl = gl;
        this.locations = gl.getLocations();
        this.locationNames = gl.getLocationNames();
        this.r = r;
        this.cs = cs;
        this.DUDE = DUDE;

        i = Mathf.FloorToInt(r.Next(locationNames.Count));
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
        sr.color = new Color(1, 0.75f, 0);
    }

	// Update is called once per frame
    override
	public void Update () {
        if (VIPCollected)
        {
            Vector2 distanceToGoal = gl.getPlayerPosition() - destination;
            if (distanceToGoal.magnitude < 0.5f)
            {
                if (!dudeCreated)
                {
                    dudeCreated = true;
                    GameObject d = (GameObject)Object.Instantiate(DUDE);
                    Dude script = (Dude)d.GetComponent("Dude");
                    script.setGetOut(new Vector3(destination.x, destination.y - 0.5f, 0));
                    script.setGameLogic(gl);
                    d.transform.position = gl.getPlayerPosition();
                }
            }
        }
        else
        {
            Vector2 distanceToGoal = gl.getPlayerPosition() - destination;
            if (distanceToGoal.magnitude < 0.5f)
            {
                if (cs.CLEAN < 50)
                {
                    gl.addToQueue("Car too dirty!");
                }
                else if (!dudeCreated)
                {
                    dudeCreated = true;
                    GameObject d = (GameObject)Object.Instantiate(DUDE);
                    Dude script = (Dude)d.GetComponent("Dude");
                    script.setGameLogic(gl);
                    d.transform.position = new Vector3(destination.x, destination.y-0.5f, 0);
                    
                }
                
            }
        }
        
	}

    public void OnGetIn()
    {
        SpriteRenderer sr = (SpriteRenderer)g.GetComponent("SpriteRenderer");
        sr.color = new Color(1, 1, 1);
        dudeCreated = false;
        collectVIP();
        gl.displayObjective(getDescription());
    }

    public void OnGetOut()
    {
        gl.completeObjective();
        dudeCreated = false;
        SpriteRenderer sr = (SpriteRenderer)g.GetComponent("SpriteRenderer");
        sr.color = new Color(1, 1, 1);
    }


    private void collectVIP()
    {
        int j = Mathf.FloorToInt(r.Next(locationNames.Count));
        while (i == j)
        {
            j = Mathf.FloorToInt(r.Next(locationNames.Count));
        }

        destination = locations[j];
        location = locationNames[j];

        setDestination();

        VIPCollected = true;
    }

    override
    public string getDescription()
    {
        if (VIPCollected)
        {
            return "Take the VIP to the " + location + "!";
        }
        else
        {
            return "Pick up the VIP from the " + location + "!";
        }
    }
}
