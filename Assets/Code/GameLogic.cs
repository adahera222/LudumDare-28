using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameLogic : MonoBehaviour {

    public GameObject PLAYER;
    public GameObject DUDE;

    private Objective currentObjective;
    private System.Random r;
    private Dictionary<int, Vector2> locations = new Dictionary<int, Vector2>();
    private List<string> locationNames = new List<string>();

    private float timer;

	// Use this for initialization
	void Start () {
        r = new System.Random();

        generateLocations();
	}

    private void generateLocations()
    {
        int i = 0;
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Location");
        foreach (GameObject go in objs)
        {
            locations.Add(i, new Vector2(go.transform.position.x, go.transform.position.y));
            locationNames.Add(go.name);
            i++;
        }
    }

	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;

        if (currentObjective == null)
        {
            float rand = r.Next(3);
            if (rand < 1)
            {
                currentObjective = new Location(this, r);
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
                CarStatus cs = (CarStatus)PLAYER.GetComponent("CarStatus");
                currentObjective = new Taxi(this, r, cs, DUDE);
                timer += 30;
                Debug.Log(currentObjective.getDescription());
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

    public Dictionary<int, Vector2> getLocations()
    {
        return locations;
    }

    public List<string> getLocationNames()
    {
        return locationNames;
    }
}
