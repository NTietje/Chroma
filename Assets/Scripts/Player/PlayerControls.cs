﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

	public static PlayerControls instance;

	public int speed = 450;
	public float maxClimb = 0.2f;
	public int maxFallingHeight = 50;
	public float respawnWait = 0.5f;
	public AudioClip cubeSound;
	public AudioClip fallingSound;
	//public Material defaultPlayerMaterial;

	private AudioSource cubeSource;
	private AudioSource fallingSource;
	
    private GameObject pivot;
    //private Vector3 spawnPoint;
	private Vector3 rotAxis;
	private Vector3 direction;
	private float cubeRadius;

    
	//private bool touchAllowed;
	private bool resetting;
	private bool moving;
	private bool falling;
	private int lowerBound;
	private Renderer renderer; 
    //private Vector2 touchOrigin = -Vector2.one;


	void Awake () {
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
        //touchAllowed = true;
        cubeRadius = transform.lossyScale.x*0.5f;
		//spawnPoint = transform.position;
		pivot = new GameObject("Pivot");
		pivot.transform.SetParent (transform);

		renderer = gameObject.GetComponent<Renderer> ();
		if (GameManager.instance.CustomSpawn()) {
			renderer.material.color = LayerColors.FindLayerColor (gameObject.layer);
		} else {

			renderer.material.color = LayerColors.defaultColor;
		}
		GameManager.instance.SetSpawn(transform.position);
		AlignPosition ();
	}

    private void Update()
    {
		
    }

    public void TryMove(string moveDirection)
    {
        Debug.Log("TryMove");  
        if (!moving && !falling && !resetting)
        {
            AlignPosition(); 
            Invoke(moveDirection, 0f); //<<<<<<<<<<<<<<<<<<<<<< hier wird MoveToBottomLeft o.ä. aufgerufen, darin wird Move aufgerufen
            //pivot.transform.localPosition = rotPointOffset;
            //StartCoroutine(SequenzHandler(moveDirection));
            Debug.Log("SetNewDirection");
            cubeSource.PlayOneShot(cubeSound, 1F); //1parameter: audio clip 2paramenter: volume
            //<<<<<<<<<<<<<<<<<<<<<< Move() kann in TryMove() nur mit Coroutine ausgeführt werden, kann ich auch wieder dahin zurück ändern
        }
    }

    /*
    IEnumerator SequenzHandler(string moveDirection)
    {
        Debug.Log(moveDirection);
        yield return StartCoroutine(moveDirection);
        yield return StartCoroutine(Move());
    }
    */

    void MoveToBottomLeft()
    {
        Debug.Log("in MoveBottomLeft");
        //x-0.5, y-0.5 BottomLeft
        direction = Vector3.left;
        //rotPointOffset = new Vector3 (-0.5f, -0.5f, 0f);
        rotAxis = Vector3.forward;
        Move();
    }

    void MoveToBottomRight()
    {
        direction = Vector3.back;
        //rotPointOffset = new Vector3 (0f, -0.5f, -0.5f);
        rotAxis = Vector3.left;
        Move();
    }

    void MoveToTopLeft()
    {
        direction = Vector3.forward;
        //rotPointOffset = new Vector3 (0f, -0.5f, 0.5f);
        rotAxis = Vector3.right;
        Move();
    }

    void MoveToTopRight()
    {
        direction = Vector3.right;
        //rotPointOffset = new Vector3 (0.5f, -0.5f, 0f);
        rotAxis = Vector3.back;
        Move();
    }

    void Move()
    {
        if (direction != Vector3.zero)
        {
            pivot.transform.localPosition = new Vector3(cubeRadius * direction.x, -cubeRadius, cubeRadius * direction.z);
            if (TargetIsNegotiable(direction))
            {
                moving = true;
            }
			direction = Vector3.zero;
        } 
        //yield return null;
    }

    void FixedUpdate ()
    {
		//ground detection
		RaycastHit hit;
		if (Physics.Raycast (transform.position, Vector3.down, out hit, 0.51f))
        {
			falling = false;
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
			// player is free falling
		}
        if (falling)
        {
					
			if (transform.position.y < lowerBound && !resetting)
            {
				resetting = true;

				//reset to active checkpoint
				StartCoroutine (Reset());
			}
		//player interaction is not locked by a current falling or moving status
		}
	}
	//corrects the position of the game object to integers, sets all angles and velocity to zero 
	public void AlignPosition()
    {
		transform.localRotation = Quaternion.identity;
		transform.localPosition = new Vector3 (Mathf.Round (transform.localPosition.x), transform.localPosition.y, Mathf.Round (transform.localPosition.z));
		gameObject.GetComponent<Rigidbody> ().velocity = Vector3.zero;
		lowerBound = (int)transform.position.y - maxFallingHeight;
	}

	/*public void SetSpawnPoint(Vector3 spawnPoint)
    {
		this.spawnPoint = spawnPoint;
	}*/

	private bool TargetIsNegotiable(Vector3 direction)
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
		renderer.material.color = LayerColors.defaultColor;
		//GameManager.instance.SetActivePlayerLayer (gameObject.layer);
	}
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
