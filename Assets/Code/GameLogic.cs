using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameLogic : MonoBehaviour {

    public GameObject PLAYER;
    public GameObject DUDE;
    public GameObject DOGE;

    public GUIText OBJECTIVE;
    public GUIText TOP, NEXT, AFTER;
    public GUIText TIME;

    public GUIText DIRT, FUEL, HEALTH;

    public GUITexture GAMEOVERBG, DESCBG, BUTTONBG, SCOREBG; public GUIText GAMEOVERTEXT, DESCTEXT, SCORE;

    private Objective currentObjective;
    private System.Random r;
    private Dictionary<int, Vector2> locations = new Dictionary<int, Vector2>();
    private List<string> locationNames = new List<string>();

    private Stack<string> eventFeed = new Stack<string>();

    private float timer = 60;
    private bool gameOver = false;

    public int score = 0;
    private int timeBonus = 30;

    private List<GameObject> doges = new List<GameObject>();

	// Use this for initialization
	void Start () {

        r = new System.Random();

        generateLocations();

        float rand = r.Next(3);
        if (rand < 1)
        {
            currentObjective = new Location(this, r);
        }
        else if (rand < 2)
        {
            currentObjective = new Hit(this, r);
        }
        else if (rand < 3)
        {
            CarStatus cs = (CarStatus)PLAYER.GetComponent("CarStatus");
            currentObjective = new Taxi(this, r, cs, DUDE);

        }
        displayObjective(currentObjective.getDescription());
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
        if (gameOver)
        {
            foreach (GameObject g in doges) {
                g.transform.Rotate(new Vector3(0,0,1));
            }
            return;
        }

        timer =  Mathf.Max(timer - Time.deltaTime, 0);
        if (timer == 0)
        {
            GameOver("You ran out of time to complete the objective.");
        }

        int minutes = (Mathf.FloorToInt(timer)) / 60;
        int seconds = (Mathf.FloorToInt(timer)) % 60;
        string minZero =  "", minMin = "";
        if (minutes < 10)
        {
            minMin = "0";
        }
        if (seconds < 10)
        {
            minZero = "0";
        }
        TIME.text = "Time remaining: " + minMin + minutes + ":" + minZero + seconds;

        if (currentObjective == null)
        {
            float rand = r.Next(3);
            if (rand < 1)
            {
                currentObjective = new Location(this, r);
            }
            else if (rand < 2)
            {
                currentObjective = new Hit(this, r);
            }
            else if (rand < 3)
            {
                CarStatus cs = (CarStatus)PLAYER.GetComponent("CarStatus");
                currentObjective = new Taxi(this, r, cs, DUDE);
                
            }
            timer += timeBonus;
            addToQueue("Plus " + timeBonus + "s bonus time!");
            displayObjective(currentObjective.getDescription());
        }
        else
        {
            currentObjective.Update();
        }
	}

    public void completeObjective()
    {
        currentObjective = null;
        addToQueue("Objective Complete!");
        score++;
        timeBonus = Mathf.Max(timeBonus-1, 20);
        AudioHelper.Instance.MakeObjectiveSound();
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

    public void displayObjective(string text) {
        PopupText p = (PopupText)OBJECTIVE.gameObject.GetComponent("PopupText");
        p.reset();
        OBJECTIVE.text = text;
        p.shrink();
    }

    public void addToQueue(string e)
    {
        eventFeed.Push(e);
        updateFeed();
    }

    private void updateFeed()
    {
        string top = eventFeed.Pop();
        TOP.text = top;
        if (eventFeed.Count > 0)
        {
            string next = eventFeed.Pop();
            NEXT.text = next;
            if (eventFeed.Count > 0)
            {
                string after = eventFeed.Pop();
                AFTER.text = after;
                eventFeed.Push(after);
            }
            eventFeed.Push(next);
        }
        eventFeed.Push(top);
    }

    public void GameOver(string desc)
    {
        if (!gameOver)
        {
            GAMEOVERBG.enabled = true; GAMEOVERTEXT.enabled = true; DESCBG.enabled = true; DESCTEXT.enabled = true;
            BUTTONBG.enabled = true; SCOREBG.enabled = true; SCORE.enabled = true;
            DESCTEXT.text = desc;

            if (score == 0)
            {
                SCORE.text = "How did you fail to complete any objectives?!";
            }
            else if (score < 5)
            {
                SCORE.text = "At least you completed " + score + " objectives.";
            }
            else if (score < 10)
            {
                SCORE.text = "But, you completed " + score + " objectives!";
            }
            else if (score < 20)
            {
                SCORE.text = "Regardless, completing " + score + " objectives is a impressive!";
            }
            else
            {
                SCORE.text = "" + score + " objectives? SUCH OBJECTIVE, SO WOW!";
                for (int i = 0; i < 50; i++)
                {
                    GameObject go = (GameObject)Instantiate(DOGE);
                    go.transform.position = new Vector3(r.Next(-10, 10), r.Next(-10, 10), 0);
                    go.transform.Rotate(new Vector3(0,0,r.Next(360)));
                    doges.Add(go);
                }
            }

            DIRT.color = new Color(1,1,1);
            FUEL.color = new Color(1,1,1); HEALTH.color = new Color(1,1,1);
            TOP.color = new Color(1,1,1); NEXT.color = new Color(1,1,1); AFTER.color = new Color(1,1,1);
            TIME.color = new Color(1, 1, 1); OBJECTIVE.color = new Color(1, 1, 1);

            CarScript cs = (CarScript)PLAYER.GetComponent("CarScript");
            cs.GameOver();

            gameOver = true;
        }
       
    }

    void OnGUI()
    {
        if (gameOver)
        {
            if (GUI.Button(new Rect(Screen.width/2 - 75, 2 * Screen.height/3, 150, 60), "Back to the Menu"))
            {
                Application.LoadLevel("Menu");
            }
        }
    }
}
