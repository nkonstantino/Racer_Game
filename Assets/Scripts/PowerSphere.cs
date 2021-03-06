﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSphere : MonoBehaviour {

    public int BoostPower;
    public AudioClip CollectSound;

    void OnTriggerEnter(Collider other){
		if (other.tag == "Player") {
            AudioSource audio = other.GetComponent<AudioSource>();
            PlayerMotor playerSpeed = other.GetComponent<PlayerMotor>();
            GameController gc = GameObject.FindWithTag("GameController").GetComponent<GameController>();
            gc.bonusScore(100f);
            //Code for back when this did other stuff
            //playerSpeed.maxspeed -= BoostPower;
            //playerSpeed.speed -= BoostPower;
            //Debug.Log("Reduced speed to " + playerSpeed.speed);
            //Once upon a time there was a fuel system!
            //playerSpeed.updateFuel (true);
            //Debug.Log ("SPEED UP!");
            audio.PlayOneShot(CollectSound, 0.7f);
            playerSpeed.PowerSphereEffect.Play();
			Destroy (gameObject);
		}
	}

}
