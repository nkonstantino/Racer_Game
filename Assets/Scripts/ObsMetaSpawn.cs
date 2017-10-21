using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsMetaSpawn : MonoBehaviour {

	public GameObject ObsSpawn;

	// Use this for initialization
	void Start () {
		SpawnSpawner ();
	}

	void SpawnSpawner(){
		GameObject newSpawner;
		newSpawner = Instantiate (ObsSpawn) as GameObject;
		newSpawner.transform.position = transform.position;
		newSpawner.transform.SetParent (transform);
	}
}
