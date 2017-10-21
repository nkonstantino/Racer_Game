using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowMetaSpawn : MonoBehaviour {

	public GameObject PowSpawn;

	// Use this for initialization
	void Start () {
		SpawnSpawner ();
	}

	void SpawnSpawner(){
		GameObject newSpawner;
		newSpawner = Instantiate (PowSpawn) as GameObject;
		newSpawner.transform.position = transform.position;
		newSpawner.transform.SetParent (transform);
	}
}
