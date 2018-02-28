using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

    public PlayerControls playerController;

    Vector2 touchOrigin = -Vector2.one;
	
	// Update is called once per frame
	void Update () {

#if UNITY_STANDALONE
        //Bottom Left
        if (Input.GetKeyDown (KeyCode.S)) {
            playerController.TryMove("MoveToBottomLeft");
		}
		//Bottom Right
		else if (Input.GetKeyDown (KeyCode.D)) {
            playerController.TryMove("MoveToBottomRight");
		}
		//Top Left
		else if (Input.GetKeyDown (KeyCode.W)) {
            playerController.TryMove("MoveToTopLeft");
		}
		//Top Right
		else if (Input.GetKeyDown (KeyCode.E)) {
            playerController.TryMove("MoveToTopRight");
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
                Vector2 topRight = new Vector2(Screen.width, Screen.height);
               
                if (touchOrigin.y < Screen.height / 2 && touchOrigin.x > Screen.width / 2)
                {
                    playerController.TryMove("MoveToBottomRight");
                }       
                else if (touchOrigin.y < Screen.height / 2 && touchOrigin.x < Screen.width / 2)
                {
                    playerController.TryMove("MoveToBottomLeft");
                }
                else if (touchOrigin.y > Screen.height / 2 && touchOrigin.x > Screen.width / 2 && Vector2.Distance(touchOrigin, topRight) >= Screen.height / 8)
                {
                    playerController.TryMove("MoveToTopRight");
                }
                else if (touchOrigin.y > Screen.height / 2 && touchOrigin.x < Screen.width / 2)
                {
                    playerController.TryMove("MoveToTopLeft");
                }
                else
                {
                    Debug.Log("Touch war nicht im gültigen Bereich");
                }
            }

        }
#endif

    }

}
