﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager2 : MonoBehaviour {

	public GameObject[] tilePrefabs;

	private Transform playerTransform;
	private float spawnZ = -100.0f; //Distance at which another tile is spawned?
	private float tileLength = 100.0f; //How long a tile is
	private float safeZone = 100f; //How long until tiles start to spawn?
	private int totalTiles = 10; //Total tiles spawned at once
	private int lastTileIndex = 0; //Last tile (to prevent repeats)
	private int tiletype = 1; //No idea
	private List<GameObject> activeTiles; //Array of currently active tiles
	private int listSize;
	private bool gameOver;
	private float currentx;

	// Use this for initialization
	void Start () {
		activeTiles = new List<GameObject>();
		playerTransform = GameObject.FindGameObjectWithTag ("Player").transform;
		currentx = 0;
		for (int i = 0; i < totalTiles; i++) {

			if (i < 9) {
				SpawnTile (0);
			} else {
				SpawnTile ();
			}
		}

	}

	// Update is called once per frame
	void Update () {
		gameOver = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ().gameOver;
		if(!gameOver){
			if(playerTransform.position.z - safeZone > (spawnZ - totalTiles * tileLength)){
				//add more qualifiers?
				SpawnTile ();
				DeleteTile ();
			}
		}
	}

	void SpawnTile(int prefabIndex = -1){
		GameObject tileToSpawn;
		if (prefabIndex == -1) {
			tileToSpawn = Instantiate (tilePrefabs [RandomPrefabIndex ()]) as GameObject;
		} else {
			tileToSpawn = Instantiate(tilePrefabs[prefabIndex]) as GameObject;
		}


		//FIND QUAD ON PREVIOUS TILE TO DETERMINE X!
		//FIND PREVIOUS TILE:
		listSize = (activeTiles.Count-1);
//		Debug.Log (listSize);

		if (listSize >= 9) {
			GameObject prevTile = activeTiles [listSize];
			if (prevTile.transform.GetChild (1).CompareTag ("connector")) {
				float newx = prevTile.transform.GetChild (1).transform.position.x;
				currentx = newx;
				Debug.Log ("New x is:" + currentx);
			}
		}
		Vector3 tileMove = new Vector3 (currentx,0,spawnZ);
//		Debug.Log (tileMove);
		tileToSpawn.transform.SetParent (transform);
		tileToSpawn.transform.position = tileMove;
		spawnZ += tileLength;
		activeTiles.Add (tileToSpawn);
		tiletype += 1;
	}

	void DeleteTile(){
		Destroy (activeTiles [0]);
		activeTiles.RemoveAt (0);
	}

	private int RandomPrefabIndex(){
		if (tilePrefabs.Length <= 1) {
			return 0;
		}
		int randomIndex = lastTileIndex;
		while (randomIndex == lastTileIndex) {
			randomIndex = Random.Range (1, tilePrefabs.Length);
		}
		//if hardmode then random?

		lastTileIndex = randomIndex;

		return randomIndex;

	}
}
