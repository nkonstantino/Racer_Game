using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.PostProcessing;

public class GameController : MonoBehaviour {
	//player related stuff
	public GameObject player; //player object
	private float playerSpeed; //player object's speed
	private Transform playerStartPosition; //player's start postition
	private float playerDistance; //distance player has traveled
	PlayerMotor playerController; //player controller script
	public float forceBoostTime; //time at which the game is forced to spawn a boost pad
    //UI related stuff
    private GameObject canvas;
	public Text ScoreDisplay;
	private Text ScoreDisplayText;
	private float score;
    private float topspeed;

    public GameObject pauseMenu;

	public Text GameOverDisplay;
	private Text GameOverDisplayText;

	public Text GOScoreDisplay;
	private Text GOScoreDisplayText;

	public Text RestartDisplay;
	private Text RestartDisplayText;

	public Text BonusDisplay;
	private Text BonusDisplayText;
    private multipliercolors BonusDisplayColor;
	public int boostMultiplier;

	public Text FuelDisplay; //WAS used for fuel, since converted to spedometer
	private Text FuelDisplayText;
    private float mphupdatetime;

    private float combocount;
    private float maxcombocount;

    public FloatingText popupTextObj;
    public GameObject pauseButton;

    public barscript boostBar;
    private GameObject bonusTimerBar;


	public bool gameOver;
	private bool restart;
    public bool isPaused;


	// Use this for initialization
	void Start () {
        //PlayerController2 playerController = player.GetComponent<PlayerController2> ();
        //playerSpeed = playerController.speed;
        canvas = GameObject.Find("Canvas");
        bonusTimerBar = GameObject.Find("BonusTimerBar");
		playerController = player.GetComponent<PlayerMotor> ();
		playerSpeed = playerController.speed;
		playerStartPosition = player.transform;
		ScoreDisplayText = ScoreDisplay.GetComponent<Text> ();
		GameOverDisplayText = GameOverDisplay.GetComponent<Text> ();
		RestartDisplayText = RestartDisplay.GetComponent<Text> ();
		BonusDisplayText = BonusDisplay.GetComponent<Text> ();
        BonusDisplayColor = BonusDisplay.GetComponent<multipliercolors>();
		GOScoreDisplayText = GOScoreDisplay.GetComponent<Text> ();
		FuelDisplayText = FuelDisplay.GetComponent<Text> ();
        pauseMenu.SetActive(false);
        pauseButton.SetActive(true);



        //set things for start
        gameOver = false;
		restart = false;
        isPaused = false;
        GameOverDisplayText.text = "";
		RestartDisplayText.text = "";
		ScoreDisplayText.text =  "";
		BonusDisplayText.text = "1x";
		GOScoreDisplayText.text = "";
		score = 0;
		boostMultiplier = 1;
		forceBoostTime = Time.time + 2f;
        combocount = 0;
        maxcombocount = 0; //On gameover, set Score count to display "Max Combo"
        mphupdatetime = 0f;
        topspeed = 200f;

    }

    // Update is called once per frame
    void Update(){

		if (!gameOver) {
           
            checkOffStage();
            addScore();
            if (mphupdatetime < Time.time)
            {
                mphupdatetime = Time.time + 0.25f;
                FuelDisplayText.text = playerController.speed + " mph"; //Should probably just make one "update ui" function
            }	
		}

		if (gameOver) {
            pauseButton.SetActive(false);
            FuelDisplayText.font = RestartDisplayText.font;
            FuelDisplayText.text = "Top Speed \n<color=#FFF42CFF>" + topspeed + "</color> mph";
			RestartDisplayText.text = "Press R to restart";
            bonusTimerBar.SetActive(false);
			DisplayScore ();
			restart = true;
		}

        if (Input.GetKeyDown(KeyCode.P))
        {
            pauseGame();
        }

		if (Input.GetKeyDown (KeyCode.R)) {
			if (restart) {
				SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
			}
		}

	}

    public void pauseGame()
    {
        if (!restart)
        {
            if (!isPaused)
            {
                Time.timeScale = 0;
                AudioListener.pause = true;
                pauseMenu.SetActive(true);
                isPaused = true;
            }
            else
            {
                Time.timeScale = 1;
                AudioListener.pause = false;
                pauseMenu.SetActive(false);
                isPaused = false;
            }
        }
    }

	private void checkOffStage(){
		if (GameObject.Find ("Player") != null) {
			if (player.transform.position.y < -15) {
				GameOver ();
				playerController.Death ();
				Destroy (player);
			}
		} else {
			Debug.Log ("Hello");
			GameOver ();
		}
	}

    private void createPopup(string text, Transform location) //CREATE BONUS POPUP!
    {
        FloatingText instance = Instantiate(popupTextObj);
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(location.position);
        screenPosition.x -= 75; //adjust x
        screenPosition.y += 75; //adjust y
        instance.transform.SetParent(canvas.transform, false);
        instance.transform.position = screenPosition;
        instance.SetText(text+"x");
    }

	public void SetMultiplier(int newValue = 1){
		if (newValue > 1) {
            playerSpeed = playerController.speed;
            Debug.Log(playerSpeed);
            boostMultiplier = newValue;
            createPopup(newValue.ToString(), player.transform);
            BonusDisplayColor.ChangeModifierColor(boostMultiplier - 1);
            boostBar.Value(Time.time+playerController.bonusTime, playerController.bonusTime);
            combocount += 1;
            if(combocount > maxcombocount)
            {
                maxcombocount = combocount;
            }
        } else {
            if(boostMultiplier > 1)
            {
                boostMultiplier = 1;
                BonusDisplayColor.resetModifierColor();
                combocount = 0;
            }
			
        }
        if (playerSpeed > topspeed)
        {
            topspeed = playerSpeed;
        }

        BonusDisplayText.text = string.Format ("{0:G}", boostMultiplier) + "x";


    }

    public void addScore()
    {
        if (!isPaused)
        {
            playerSpeed = playerController.speed;
            playerDistance = Vector3.Distance(playerStartPosition.position, transform.position);
            int newScoreValue = Mathf.CeilToInt(((playerSpeed + playerDistance) / 500) * boostMultiplier);
            score += newScoreValue;
            DisplayScore();

        }
    }

	void DisplayScore(){
		if (gameOver) {
			GOScoreDisplayText.text = "Score \n" + string.Format ("{0:G}", score);
			ScoreDisplayText.text =  "Max Combo \n" + maxcombocount;
			BonusDisplayText.text = "";
		} else {
			ScoreDisplayText.text = "Score \n" + string.Format("{0:G}",score);
		}
	}

	public void GameOver(){
		GameOverDisplayText.text = "";
		gameOver = true;
	}
}