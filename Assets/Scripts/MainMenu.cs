using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public GameObject mainMenu;
    public GameObject credits;

    // Use this for initialization
    void Start () {
        //mainMenu = GameObject.Find("MainMenuContainer");
        //credits = GameObject.Find("CreditsContainer");
        credits.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ToGame(){
		SceneManager.LoadScene ("Game");
	}

    public void ToCredits()
    {
        mainMenu.SetActive(false);
        credits.SetActive(true);
    }
}
