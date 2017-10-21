using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour {

	public GameObject[] ObstaclePrefabs;
	private int lastObstacleIndex = 0;

	// Use this for initialization
	void Start () {
		SpawnObstacle ();
	}

	void SpawnObstacle(){
		GameObject ObstacleToSpawn;
		ObstacleToSpawn = Instantiate (ObstaclePrefabs [RandomPrefabIndex ()]) as GameObject;
		ObstacleToSpawn.transform.SetParent (transform);
		ObstacleToSpawn.transform.position = transform.position;
	}

	private int RandomPrefabIndex(){
		if (ObstaclePrefabs.Length <= 1) {
			return 0;
		}
		int randomIndex = lastObstacleIndex;
		while (randomIndex == lastObstacleIndex) {
			randomIndex = Random.Range (0, ObstaclePrefabs.Length);
		}

		lastObstacleIndex = randomIndex;

		return randomIndex;

	}

	// Update is called once per frame
	void Update () {
		
	}
}
