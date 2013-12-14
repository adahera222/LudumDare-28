using UnityEngine;
using System.Collections;

public class Hit : Objective {
    private int number;
    private GameLogic gl;
    private int n = 0;

    public Hit(GameLogic gl, System.Random r)
    {
        this.gl = gl;
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
        return "Hit " + number + " fire hydrants!";
    }

    public void OnHit()
    {
        n++;
    }
}
