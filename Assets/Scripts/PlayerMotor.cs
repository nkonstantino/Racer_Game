using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class PlayerMotor : MonoBehaviour {

	private CharacterController controller;
	private Rigidbody rb;
    
    public float speed = 250f; //current speed
    public float normalSpeed; //essentially min speed?
    public float maxspeed = 350f; //speed when hitting boost
	public float turnspeed = 75f;
	public float tilt = 10f;

    private bool hasShield;
	
	public float fuel = 100f;
	private float fueldelay = 0.5f;
    private float speedTime = 3f;

	private float verticalVelocity = 0.0f;
	private float gravity = 100.0f;
	private Vector3 moveVector;

	private Camera playerCam;
    private PostProcessingBehaviour camerapostprocess;
    public PostProcessingProfile processingProfile;
    private GameController gc;

	public bool boosting; //Not used for anything lol
	public float boostStart; //time.time when boost started
    public float boostTime; //amount of time boost will last at full power
    public float bonusTime; //amount of time bonus will last

	public ParticleSystem jetMain;
    private ParticleSystem.MainModule jetEngine;
    public ParticleSystem speedEffect;
	public ParticleSystem shipSpeedup;
    public ParticleSystem PowerSphereEffect;
    public ParticleSystem ShieldEffect;
    //public GameObject shield;
    public Material normalMat;
    public Material shieldedMat;
    public GameObject PlayerMesh;
    private Renderer rend;

    private AudioSource audio;
    public AudioClip boostsfx;
    public AudioClip shieldHit;

    private Animator anim;

	// Use this for initialization
	void Start () {
		jetEngine = jetMain.main;
		normalSpeed = speed;
		controller = GetComponent<CharacterController> ();
		playerCam = GameObject.FindWithTag ("MainCamera").GetComponent<Camera> ();
        camerapostprocess = GameObject.FindWithTag("MainCamera").GetComponent<PostProcessingBehaviour>();
        camerapostprocess.profile.chromaticAberration.enabled = false;
		rb = GetComponent<Rigidbody> ();
		gc = GameObject.FindWithTag ("GameController").GetComponent<GameController> ();
        anim = GetComponent<Animator>();
        hasShield = false;
        //shield.SetActive(false);
        rend = PlayerMesh.GetComponent<Renderer>();
        audio = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update () {
        speedup();
		checkSpeed ();
		movePlayer ();
		//updateFuel ();
	}

	private void OnControllerColliderHit(ControllerColliderHit hit){
		if (hit.gameObject.tag == "Obstacle") {
            if (hasShield)
            {
                Debug.Log("Destroy shield!");
                Destroy(hit.gameObject);
                SetShield(false);
            } else
            {
                Death();
            }
			
		}
	}

	public void updateFuel(bool x = false){ //fuel system, currently disabled
		if (x) {
			fuel = Mathf.Clamp (fuel+= 5f, 0f, 100f);
		} else {
			if (Time.time > fueldelay) {
				fuel = Mathf.Clamp (fuel-= 1f, 0f, 100f);
				fueldelay = Time.time + 0.25f;
			}

		}

		if (fuel == 0) {
			Death ();
			gc.GameOver ();
		}

	}

    public void SetShield(bool ShieldStatus)
    {
        hasShield = ShieldStatus;
        if (hasShield)
        {
            //shield.SetActive(true);
            rend.material = shieldedMat;
        } else
        {
            //shield.SetActive(false);
            audio.PlayOneShot(shieldHit, 0.7f);
            rend.material = normalMat;
        }
    }

	private void movePlayer(){
		moveVector = Vector3.zero;
		//x
		moveVector.x = Input.GetAxisRaw("Horizontal") * turnspeed;
        
		if (Input.GetMouseButton (0)) {
            //holding click or touch on the right?
            if (Input.mousePosition.y < 570) //so you can't turn when pressing pause
            {
			    if (Input.mousePosition.x > Screen.width / 2) {
                    anim.enabled = false;
                    moveVector.x = turnspeed;
			    } else {
                    anim.enabled = false;
                    moveVector.x = -turnspeed;
			    }
            }
        }
		//y
		if (controller.isGrounded) {
			verticalVelocity = -0.5f;
		} else {
			verticalVelocity -= gravity * Time.deltaTime;
		}
		moveVector.y = verticalVelocity;
		//z
		moveVector.z = speed;

		controller.Move (moveVector * Time.deltaTime);
		rb.rotation = Quaternion.Euler(0.0f, 0.0f, moveVector.x * -tilt);

	}

    void speedup()
    {
        if (speedTime + 1 <= Time.time)
        {
            speedTime = Time.time;
            normalSpeed += 2f;
            maxspeed += 2f;
            Debug.Log("Normal Speed is now: " + normalSpeed);
        }
    }

	void checkSpeed(){ //what to do during a boost;
		if (speed > normalSpeed) {
			if (!speedEffect.isPlaying) {
				speedEffect.Play ();
                //Debug.Log("ROLL!");
                //anim.StopPlayback();
                //anim.Play("barrellroll");
                jetEngine.startSpeed = 48;
                jetEngine.startSize = 8;
                boosting = true;
                camerapostprocess.profile.chromaticAberration.enabled = true;
                normalSpeed -= 10f;
                maxspeed -= 10f;
                Debug.Log("Reduced Speed to: " + normalSpeed);
            }

            if (boostStart + boostTime > Time.time) { //if boost time is over, slow down and zoom back in
				playerCam.fieldOfView += 1f;
				playerCam.fieldOfView = Mathf.Clamp (playerCam.fieldOfView, 60, 75);
			} else {
				speed -= 2f;
			}

		} 

		if (speed <= normalSpeed) {
            speed = normalSpeed; //make sure it sticks to the minimum
			if (playerCam.fieldOfView > 60) {
				playerCam.fieldOfView -= 0.25f; 
				Mathf.Clamp (playerCam.fieldOfView, 60, 75);
				speedEffect.Stop ();
				jetEngine.startSpeed = 16;
				jetEngine.startSize = 1;
                boosting = false;
                camerapostprocess.profile.chromaticAberration.enabled = false;
            }
	
			if (boostStart + bonusTime < Time.time) {
				gc.SetMultiplier(1);
				speedEffect.Stop ();
				jetEngine.startSpeed = 16;
				jetEngine.startSize = 1;
			}
		}
	}

    public void Death(){
		speedEffect.Stop ();
		gameObject.SetActive(false);
	}

}
