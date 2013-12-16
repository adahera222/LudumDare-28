using UnityEngine;
using System.Collections;

public class Bank : Objective {

    private int money = 0;
    private bool moneyCollected = false;
    private Vector2 bank = new Vector2(4, -2);
    private GameLogic gl;
    private GameObject g;

    public Bank(GameLogic gl)
    {
        this.gl = gl;
    }
	
	// Update is called once per frame
    override
	public void Update () {

        if (moneyCollected)
        {
            Vector2 distanceToGoal = gl.getPlayerPosition() - bank;
            if (distanceToGoal.magnitude < 0.5f)
            {
                gl.completeObjective();
                SpriteRenderer sr = (SpriteRenderer)g.GetComponent("SpriteRenderer");
                sr.color = new Color(1, 1, 1);
            }
        }
        else
        {
            if (money == 3)
            {
                moneyCollected = true;
                setDestination();
                gl.displayObjective(getDescription());
            }
        } 
	}

    public void addMoney()
    {
        money++;
    }

    override
    public string getDescription()
    {
        if (moneyCollected)
        {
            return "Take the money to the bank!";
        }
        else
        {
            return "Pick up all the money!";
        }
        
    }

    private void setDestination()
    {
        g = null;
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Location");

        foreach (GameObject go in objs)
        {
            if (go.transform.position == new Vector3(bank.x, bank.y, 1))
            {
                g = go;
                break;
            }
        }
        SpriteRenderer sr = (SpriteRenderer)g.GetComponent("SpriteRenderer");
        sr.color = new Color(0, 1, 0);
    }
}
