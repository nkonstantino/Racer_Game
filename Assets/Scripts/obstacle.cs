using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacle : MonoBehaviour {

	private GameController gameController;

	private PlayerMotor player;

	// Use this for initialization
	void Start () {
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if(gameControllerObject != null){
			gameController = gameControllerObject.GetComponent<GameController> ();
		}
		if (gameControllerObject == null) {
			Debug.Log ("Cannot find GameController");
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Player") {
			Debug.Log ("GAMEOVER");
			player = other.GetComponent<PlayerMotor> ();
			gameController.GameOver ();
			//Destroy (other.gameObject);
			player.Death();
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
