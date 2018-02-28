using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * This script handles anything relevant to the controllable player
 * It also instantiates as singleton to be accessable from anywhere
 * It controls the movement, but not the input (see inputController.cs)
 */
public class PlayerControls : MonoBehaviour {

	public static PlayerControls instance;


	public int speed = 450; //movement speed
	public float maxClimb = 0.2f; //max height the cube can climb
	public int maxFallingHeight = 50; //after falling this high, player will be reset
	public float respawnWait = 0.5f; //player reset will happen after this time has passed
	//Audio
	public AudioClip cubeSound; //movement sound
	public AudioClip fallingSound;
	AudioSource cubeSource;
	AudioSource fallingSource;
	
    Renderer rend; //this objects renderer
    GameObject pivot; //this is a point used to perform the cube movement around
	Vector3 rotAxis; //for player movement
	Vector3 direction; //movement direction
	float cubeRadius; //used for movement calculation (placing the pivot)

	//these booleans primarily lock the input and let the script finish certain states
	bool resetting;
	bool moving;
	bool falling;

	int lowerBound; //replace player after crossing this rock bottom

	void Awake () {
		//singleton pattern
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}
	}

    // Use this for initialization
    void Start ()
    {
		cubeSource = GetComponent<AudioSource>();
		fallingSource = GetComponent<AudioSource>();
        cubeRadius = transform.lossyScale.x*0.5f;
		pivot = new GameObject("Pivot");
		pivot.transform.SetParent (transform);
		rend = gameObject.GetComponent<Renderer> ();
		if (GameManager.instance.CustomSpawn()) {
			rend.material.color = LayerColors.FindLayerColor (gameObject.layer);
		} else {

			rend.material.color = LayerColors.defaultColor;
		}
		//Set the first spawnpoint to the inital location of the player prefab (as set in scene editor)
		GameManager.instance.SetSpawn(transform.position);
		AlignPosition ();
	}

    void Update()
    {
		//TODO: Lerp color
		if (rend.material.color != LayerColors.FindLayerColor(gameObject.layer)){
			rend.material.color = Color.Lerp (rend.material.color, LayerColors.FindLayerColor (gameObject.layer), 0.1f);
		}
    }
	/**
	 * check, if movement is not locked, if so, move into the desired direction 
	 */
    public void TryMove(string moveDirection)
    {
        if (!moving && !falling && !resetting)
        {
            AlignPosition(); 
            Invoke(moveDirection, 0f);
            Debug.Log("SetNewDirection");
        }
    }
	/**
	 * These methods convert the input directions to unity handled directions
	 * 
	 */
	void MoveToBottomLeft()
    {
        direction = Vector3.left;
        rotAxis = Vector3.forward;
        Move();
    }

    void MoveToBottomRight()
    {
        direction = Vector3.back;
        rotAxis = Vector3.left;
        Move();
    }

    void MoveToTopLeft()
    {
        direction = Vector3.forward;
        rotAxis = Vector3.right;
        Move();
    }

    void MoveToTopRight()
    {
        direction = Vector3.right;
        rotAxis = Vector3.back;
        Move();
    }

    void Move()
    {
        if (direction != Vector3.zero) //can be removed, was used when movement was part of Update
        {
            pivot.transform.localPosition = new Vector3(cubeRadius * direction.x, -cubeRadius, cubeRadius * direction.z);
            if (TargetIsNegotiable(direction))
            {
                moving = true;
                cubeSource.PlayOneShot(cubeSound);
            }
			direction = Vector3.zero;
        }
    }

    void FixedUpdate ()
    {
		//ground detection
		RaycastHit hit;
		if (Physics.Raycast (transform.position, Vector3.down, out hit, 0.51f))
        {
			falling = false;
			//set platform to the players parent, so player can use the local grid
			if (hit.transform.tag == "Platform")
            {
				transform.SetParent (hit.transform.parent);
			}
		}
        else
        {
			falling = true;
		}
		
		//player is within a movement
		if (moving)
        {
			//continue 90-degree-rotation for this frame
			transform.RotateAround (pivot.transform.position, rotAxis, speed * Time.deltaTime);
			if (transform.localPosition.y <= Mathf.Round (transform.localPosition.y))
            {
				moving = false;
				AlignPosition ();
			}
		}

		// player is free falling (ground detection see above)
        if (falling)
        {
			//check if lower bounds have been passed and reset (locked for only one start of the reset coroutine via bool resetting)
			if (transform.position.y < lowerBound && !resetting)
            {
				fallingSource.PlayOneShot(fallingSound, 1F);
				
				resetting = true;

				//reset to active checkpoint
				StartCoroutine (Reset());
			}
		//player interaction is not locked by a current falling or moving status
		}
	}
	/**
	 * corrects the position of the game object to integers, sets all angles and velocity to zero to make the grid-like movement behaviour
	 */
	public void AlignPosition()
    {
		transform.localRotation = Quaternion.identity;
		transform.localPosition = new Vector3 (Mathf.Round (transform.localPosition.x), transform.localPosition.y, Mathf.Round (transform.localPosition.z));
		gameObject.GetComponent<Rigidbody> ().velocity = Vector3.zero;
		lowerBound = (int)transform.position.y - maxFallingHeight;
	}
	/**
	 * This method checks, if the desired movement target does not contain any obstacles, which would block the movement
	 * Checks for any obstacles with a minimum height (float maxClimb) and on any physics layer, except the own colour-layer (if not default)
	 * returns false if movement is not possible
	 */
	bool TargetIsNegotiable(Vector3 direction)
    {
		Vector3 origin = new Vector3 (transform.position.x, (transform.position.y - cubeRadius) + maxClimb, transform.position.z);
		if (gameObject.layer == 0)
        {
			if (Physics.Raycast (origin, direction * 1.4f, 1f))
            {
				return false;
			}
		} else {
			LayerMask mask = ~(1 << gameObject.layer); //mask all layers except for the own (color) layer!
			if (Physics.Raycast (origin, direction * 1.4f, 1f, mask))
            {
				return false;
			}
		}
		return true;
	}

	public void ResetColor()
    {
		gameObject.layer = 0;
		//GetComponent<Renderer> ().material.color = defaultPlayerMaterial.color;
		rend.material.color = LayerColors.defaultColor;
		//GameManager.instance.SetActivePlayerLayer (gameObject.layer);
	}
	//resets the player
	public IEnumerator Reset(){
		//immediately stop the camera follow
		CameraFollow cam = Camera.main.GetComponent<CameraFollow> ();
		cam.enabled = false;

		//reset player and reenable camera follow
		yield return new WaitForSeconds (respawnWait);

		falling = false;
		cam.enabled = true;
		transform.position = GameManager.instance.GetSpawn();
		cam.LerpToTarget();
		moving = false;
		ResetColor ();
		AlignPosition();
		resetting = false;
	}

}
