using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour {
	//destroy text when animation is complete
	public Animator animator;
	public Text bonusText;


	void Start()
	{
		AnimatorClipInfo[] clipinfo = animator.GetCurrentAnimatorClipInfo (0);
		Destroy (gameObject, clipinfo[0].clip.length);
	}

	public void SetText(string text)
    { 
		bonusText.text = text;
	}

}
