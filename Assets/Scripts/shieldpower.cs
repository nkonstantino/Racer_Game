using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shieldpower : MonoBehaviour {

    public AudioClip CollectSound;
    private Animator animator;

    void Start()
    {
        //animator.Play("backforth");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            AudioSource audio = other.GetComponent<AudioSource>();
            PlayerMotor player = other.GetComponent<PlayerMotor>();
            audio.PlayOneShot(CollectSound, 1f);
            player.SetShield(true);
            player.ShieldEffect.Play();
            Debug.Log("Shield Up!");
            Destroy(gameObject);
        }
    }

    

}
