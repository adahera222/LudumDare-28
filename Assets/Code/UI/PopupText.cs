using UnityEngine;
using System.Collections;

public class PopupText : MonoBehaviour {

    private bool isShrinking = false;
    private GUIText gui;
    private float timer;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (isShrinking)
        {
            if (timer <= 0)
            {
                Vector3 toGoal = new Vector3(0.5f, 0.95f, 0) - transform.position;
                float distanceToGoal = toGoal.magnitude;
                if (distanceToGoal > 0.01f)
                {

                    transform.position += (toGoal) / (60 * distanceToGoal);
                    if (gui.fontSize > 16)
                    {
                        gui.fontSize--;
                    }
                }
            }
            else
            {
                timer -= Time.deltaTime;
            }
            
        }
	}

    public void reset()
    {
        timer = 1.5f;
        transform.position = new Vector3(0.5f, 0.5f, 0);
        gui = (GUIText)GetComponent("GUIText");
        gui.text = "";
        gui.fontSize = 72;
    }

    public void shrink()
    {
        isShrinking = true;
    }
}
