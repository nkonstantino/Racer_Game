using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class barscript : MonoBehaviour {
    [SerializeField]
    private float fillAmount; //current fill level (0 to 1)
    [SerializeField]
    private Image content;

    private PlayerMotor player;
    private float bonusStartTime;
    private float bonusTime;
    private float bonusEndTime;

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player").GetComponent<PlayerMotor>();
	}
	
	// Update is called once per frame
	void Update () {
        bonusEndTime = bonusStartTime + bonusTime;
        if (Time.time < bonusEndTime)
        {
            Value(bonusEndTime, bonusTime);
        }
        HandleBar();
	}

    public void Value(float BonusEnd, float BonusTime)
    {
        bonusStartTime = player.boostStart;
        bonusTime = player.bonusTime;
        float TimeRemaining;
        TimeRemaining = BonusEnd - Time.time;
        fillAmount = TimeRemaining / BonusTime;
        if (fillAmount < 0)
        {
            fillAmount = 0;
        }
    }

    private void HandleBar()
    {
        if(fillAmount != content.fillAmount)
        {
            content.fillAmount = fillAmount;
        }
    }
}
