using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class multipliercolors : MonoBehaviour {

    public Color[] textColors;
    public Text bonusDisplay;
    private Animator animator;
    public AudioClip[] multiplierTone;
    private AudioSource audiosource;

    void Start () {
        bonusDisplay = GetComponent<Text>();
        animator = GetComponent<Animator>();
        audiosource = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
	}

    public void ChangeModifierColor (int bonusNum) {
        animator.StopPlayback();
        if(bonusNum > 0)
        {
            animator.Play("textbump");
        }
        
        bonusDisplay.color = textColors[bonusNum];
	}

    public void resetModifierColor()
    {
        audiosource.PlayOneShot(multiplierTone[0], 0.4f);
        bonusDisplay.color = textColors[0];
    }
}
