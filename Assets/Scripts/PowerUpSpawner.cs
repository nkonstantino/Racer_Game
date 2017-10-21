using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour {

	public GameObject[] PowerUpPrefabs;
	private int lastObstacleIndex = 0;
	private GameController gc;

	// Use this for initialization
	void Start () {
		gc = GameObject.FindWithTag ("GameController").GetComponent<GameController> ();
		SpawnPowerUp ();
	}

	void SpawnPowerUp(){
		GameObject PowerUpToSpawn;
		PowerUpToSpawn = Instantiate (PowerUpPrefabs [RandomPrefabIndex ()]) as GameObject;
		PowerUpToSpawn.transform.SetParent (transform);
		PowerUpToSpawn.transform.position = transform.position;
	}

	private int RandomPrefabIndex(){
		// SET UP WAY TO RELIABLY SPAWN BOOST PADS!
		if (PowerUpPrefabs.Length <= 1) {
			return 0;
		}
		int randomIndex = lastObstacleIndex;
		while (randomIndex == lastObstacleIndex) {
			randomIndex = Random.Range (0, PowerUpPrefabs.Length);
		}

		if (gc.forceBoostTime < Time.time) {
			randomIndex = 0;
			gc.forceBoostTime = Time.time + 1.25f;
//			Debug.Log("Force spawn!");
		}

		lastObstacleIndex = randomIndex;

		return randomIndex;

	}

	// Update is called once per frame
	void Update () {

	}
}
