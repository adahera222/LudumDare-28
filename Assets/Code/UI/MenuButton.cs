using UnityEngine;
using System.Collections;

public class MenuButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI() {
        if (GUI.Button(new Rect(Screen.width/2 - 75, 3*Screen.height/4, 150, 60), "Play!")) {
            Application.LoadLevel("City");
        }
    }
}
