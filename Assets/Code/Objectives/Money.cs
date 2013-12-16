using UnityEngine;
using System.Collections;

public class Money : MonoBehaviour {
    public GameLogic GL;
    private bool gathered = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (gathered)
        {
            Objective o = GL.getCurrentObjective();
            if (!(o is Bank))
            {
                gathered = false;
            }
        }
        else
        {
            Objective o = GL.getCurrentObjective();
            if (o is Bank)
            {
                renderer.enabled = true;
            }
        }
	}

    void OnTriggerEnter2D()
    {
        if (!gathered)
        {
            Objective o = GL.getCurrentObjective();
            if (o is Bank)
            {
                renderer.enabled = false;
                gathered = true;
                Bank b = (Bank)o;
                b.addMoney();
            }
            AudioHelper.Instance.MakeMoneySound();
        }
       
    }
}
