using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour {

	private GameController gameController;
    private bool collected;
    private PlayerMotor player;

    // Use this for initialization
    void Start () {
        collected = false;
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
        player = GameObject.FindWithTag("Player").GetComponent<PlayerMotor>();
        if (gameControllerObject != null){
			gameController = gameControllerObject.GetComponent<GameController> ();
		}
		if (gameControllerObject == null) {
			Debug.Log ("Cannot find GameController");
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Player") {
            collected = true;
			PlayerMotor playerSpeed = other.GetComponent<PlayerMotor>();
            AudioSource audio = other.GetComponent<AudioSource>();
            playerSpeed.consecutiveBoost = playerSpeed.consecutiveBoost + 1;
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

            if(playerSpeed.consecutiveBoost > 4)
            {
                playerSpeed.SetShield(true);
                playerSpeed.consecutiveBoost = 0;
            }

			Destroy (gameObject);
		}
	}

    // Update is called once per frame
    void OnDestroy() {
        if (player)
        {
            if (!collected)
            {
                player.consecutiveBoost = 0;
            }
        }
        
	}
}
