using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public GameObject player;

	public GameObject gameControllerObject; 
	private GameController gameController;

    private Vector3 offset;

	// Use this for initialization
	void Start () {
		offset = transform.position - player.transform.position;
		gameController = gameControllerObject.GetComponent<GameController> ();
	}

	// Update is called once per frame
	void Update(){
        
	}

	void LateUpdate () {
		if (!gameController.gameOver) {
			transform.position = player.transform.position + offset;
		}

    }
}
