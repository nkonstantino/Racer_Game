using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour {

	private GameController gameController;

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
			PlayerMotor playerSpeed = other.GetComponent<PlayerMotor>();
            AudioSource audio = other.GetComponent<AudioSource>();
			playerSpeed.speed = playerSpeed.maxspeed;
            playerSpeed.boosting = true;
			playerSpeed.boostStart = Time.time;
			playerSpeed.shipSpeedup.Play ();
            audio.playOnAwake = false;
            audio.clip = playerSpeed.boostsfx;
            audio.Play();

			if (gameController.boostMultiplier < 10) {
				gameController.SetMultiplier (gameController.boostMultiplier + 1);
			} else {
				gameController.SetMultiplier (10);
			}

			Destroy (gameObject);
		}
	}

	// Update is called once per frame
	void Update () {

	}
}
