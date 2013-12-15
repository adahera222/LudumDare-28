using UnityEngine;
using System.Collections;

public class Hit : Objective {
    private int number;
    private GameLogic gl;
    private int n = 0;
    private bool isHydrant;

    public Hit(GameLogic gl, System.Random r)
    {
        this.gl = gl;
        int b = r.Next(2);
        if (b < 1)
        {
            isHydrant = true;
        }
        else
        {
            isHydrant = false;
        }

        number = r.Next(1, 5);
    }

    // Update is called once per frame
    override
    public void Update()
    {
        if (n >= number)
        {
            gl.completeObjective();
        }
    }

    override
    public string getDescription()
    {
        if (isHydrant)
        {
            return "Hit " + number + " fire hydrants!";
        }
        else
        {
            return "Hit " + number + " trash cans!";
        }
        
    }

    public void OnHit(bool isHydrant)
    {
        if (this.isHydrant == isHydrant)
        {
           n++;
        }
       
    }
}
