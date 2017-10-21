using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wooshsound : MonoBehaviour {

    public AudioClip woosh;
    private GameObject player;
    private AudioSource audiosource;
    private GameController gc;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        audiosource = GetComponent<AudioSource>();
        audiosource.playOnAwake = false;
        audiosource.clip = woosh;
        gc = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        //Debug.Log(this.transform.position.z);

    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(-this.transform.position.z + player.transform.position.z);
        if (!gc.gameOver)
        {
            if (-this.transform.position.z + player.transform.position.z < 3)
            {
                audiosource.Play();
                //Debug.Log("WOOSH!");
            }
        }
        
	}
}
