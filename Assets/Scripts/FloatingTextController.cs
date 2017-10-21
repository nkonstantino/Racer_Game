using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextController : MonoBehaviour {

    public GameObject ob;
	public static FloatingText popupText;
	private static GameObject canvas;

	public static void Initialize()
	{
		canvas = GameObject.Find ("Canvas");
        if(!popupText)
            popupText = Resources.Load<FloatingText> ("PopupTextParent");
	}

	public static void CreateFloatingText(string text, Transform location)
	{
		if (popupText) {
			FloatingText instance = Instantiate (popupText);
			instance.transform.SetParent (canvas.transform, false);
			//instance.SetText (text);
		} else {
			Debug.Log ("DAMMIT");
		}
	}
}
