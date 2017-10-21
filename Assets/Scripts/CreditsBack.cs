using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsBack : MonoBehaviour {

    public GameObject mainMenu;
    public GameObject credits;

	// Use this for initialization
	void Start () {
        //mainMenu = GameObject.Find("MainMenuContainer");
        //credits = GameObject.Find("CreditsContainer");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void backToMain()
    {
        mainMenu.SetActive(true);
        credits.SetActive(false);
    }
}
