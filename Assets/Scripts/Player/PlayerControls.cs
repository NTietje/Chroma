using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

	public int speed = 450;
	public int maxFallingHeight = 50;
	public float maxClimb = 0.2f;

	private GameObject pivot;
	
	public AudioClip cubeSound;
	private AudioSource source;

	private Vector3 spawnPoint;
	private Vector3 rotAxis;
	//private Vector3 rotPoint;
	//private Vector3 rotPointOffset;
	private Vector3 direction;

	private float cubeRadius;
	private bool moving;
	private bool falling;
	private int lowerBound;
    private Vector2 touchOrigin = -Vector2.one;

	void Awake (){
		source = GetComponent<AudioSource>();
	}
	
    // Use this for initialization
    void Start () {
		cubeRadius = transform.lossyScale.x*0.5f;
		spawnPoint = transform.position;
		pivot = new GameObject("Pivot");
		pivot.transform.SetParent (transform);
		AlignPosition ();
	}
	void Update (){
        direction = Vector3.zero;
		if (!moving && !falling) {
			AlignPosition ();

#if UNITY_STANDALONE
            //Bottom Left
            if (Input.GetKeyDown (KeyCode.S)) {
                MoveToBottomLeft();
			}
			//Bottom Right
			else if (Input.GetKeyDown (KeyCode.D)) {
                MoveToBottomRight();
			}
			//Top Left
			else if (Input.GetKeyDown (KeyCode.W)) {
                MoveToTopLeft();
			}
			//Top Right
			else if (Input.GetKeyDown (KeyCode.E)) {
                MoveToTopRight();
			}
#endif

#if UNITY_IOS || UNITY_ANDROID
            //Check if Input has registered more than zero touches
            
            if (Input.touchCount > 0)
            {
                
                //Store the first touch detected.
                Touch firstTouch = Input.touches[0];
            
                //Check if the phase of that touch equals Began
                if (firstTouch.phase == TouchPhase.Began)
                {
                    //If so, set touchOrigin to the position of that touch
                    touchOrigin = firstTouch.position;
                    Debug.Log("Touchorigin: " + touchOrigin);
             
                    int maxDistance = Screen.height / 2 - 5;
                    Vector2 bottomRight = new Vector2(Screen.width, 0);
                    Vector2 bottomLeft = new Vector2(0, 0);
                    Vector2 topRight = new Vector2(Screen.width, Screen.height);
                    Vector2 topLeft = new Vector2(0, Screen.height);
                    
                    if (Vector2.Distance(touchOrigin, bottomRight) <= maxDistance && touchOrigin.x >  Screen.width/2 + 5)
                    {
                        MoveToBottomRight();
                    }
                    else if (Vector2.Distance(touchOrigin, bottomLeft) <= maxDistance && touchOrigin.x < Screen.width / 2 - 5)
                    {
                        MoveToBottomLeft();
                    }
                    else if (Vector2.Distance(touchOrigin, topRight) <= maxDistance && touchOrigin.x > Screen.width / 2 + 5)
                    {
                        MoveToTopRight();
                    }
                    else if (Vector2.Distance(touchOrigin, topLeft) <= maxDistance && touchOrigin.x < Screen.width / 2 - 5)
                    {
                        MoveToTopLeft();
                    }
                    else
                    {
                        Debug.Log("Touch war nicht im gültigen Bereich");
                    }
                    
                }
                

                /*
                foreach(Touch touch in Input.touches)
                {
                    Debug.Log("Touch: " + touch.position);
                }
                */
            }
            
#endif

            //pivot.transform.localPosition = rotPointOffset;
            if (direction != Vector3.zero) {
				pivot.transform.localPosition = new Vector3 (cubeRadius * direction.x, -cubeRadius, cubeRadius * direction.z);
				if (TargetIsNegotiable(direction)){
					moving = true;
				}
			}
		}

    }

    void MoveToBottomLeft()
    {
        //x-0.5, y-0.5 BottomLeft
        direction = Vector3.left;
        //rotPointOffset = new Vector3 (-0.5f, -0.5f, 0f);
        rotAxis = Vector3.forward;
		
		source.PlayOneShot(cubeSound, 1F); //1parameter: audio clip 2paramenter: volume
    }

    void MoveToBottomRight()
    {
        direction = Vector3.back;
        //rotPointOffset = new Vector3 (0f, -0.5f, -0.5f);
        rotAxis = Vector3.left;
		
		source.PlayOneShot(cubeSound, 1F);
    }

    void MoveToTopLeft()
    {
        direction = Vector3.forward;
        //rotPointOffset = new Vector3 (0f, -0.5f, 0.5f);
        rotAxis = Vector3.right;
		
		source.PlayOneShot(cubeSound, 1F);
    }

    void MoveToTopRight ()
    {
        direction = Vector3.right;
        //rotPointOffset = new Vector3 (0.5f, -0.5f, 0f);
        rotAxis = Vector3.back;
		
		source.PlayOneShot(cubeSound, 1F);
    }

    void FixedUpdate () {
		//ground detection
		RaycastHit hit;
		if (Physics.Raycast (transform.position, Vector3.down, out hit, 0.51f)) {
			falling = false;
			if (hit.transform.tag == "Platform") {
				transform.SetParent (hit.transform.parent);
			}
		} else {
			falling = true;
		}
		
		//player is within a movement
		if (moving) {
			//continue 90-degree-rotation for this frame
			transform.RotateAround (pivot.transform.position, rotAxis, speed * Time.deltaTime);
			if (transform.localPosition.y <= Mathf.Round (transform.localPosition.y)) {
				moving = false;
				AlignPosition ();
			}
			// player is free falling
		} else {
			
		}
		if (falling) {
			if (transform.position.y < lowerBound) {
				//reset to active checkpoint
				transform.position = spawnPoint;
				AlignPosition();
				falling = false;
			}
		//player interaction is not locked by a current falling or moving status
		} 
	}
	//corrects the position of the game object to integers, sets all angles and velocity to zero 
	public void AlignPosition(){
		transform.localRotation = Quaternion.identity;
		transform.localPosition = new Vector3 (Mathf.Round (transform.localPosition.x), transform.localPosition.y, Mathf.Round (transform.localPosition.z));
		gameObject.GetComponent<Rigidbody> ().velocity = Vector3.zero;
		lowerBound = (int)transform.position.y - maxFallingHeight;
	}
	public void SetSpawnPoint(Vector3 spawnPoint){
		this.spawnPoint = spawnPoint;
	}
	private bool TargetIsNegotiable(Vector3 direction){
		Vector3 origin = new Vector3 (transform.position.x, (transform.position.y - cubeRadius) + maxClimb, transform.position.z);
		if (gameObject.layer == 0) {
			if (Physics.Raycast (origin, direction * 1.4f, 1f)) {
				return false;
			}
		} else {
			if (Physics.Raycast (origin, direction * 1.4f, 1f, gameObject.layer)) {
				return false;
			}
		}
		return true;
	}
}
