using UnityEngine;
using System.Collections;

public class SwipeController : MonoBehaviour
{

    [SerializeField] private float playerXMoveSpeed = 1f; // SPEED OF PLAYER TO SWIPE LEFT OR RIGHT
    [SerializeField] private float playerZMoveSpeed = 1f; // SPEED OF PLAYER IN FORWARD 
    [SerializeField] private float playerClampPos = 4.3f;


    private Vector2 fingerDown; // WHEN FINGER TOUCH THE SCREEN
    private Vector2 fingerUp; // WHEN FINGER UP FROM SCREEN

    public float swipeThreshold = 20f; // MIN SWIPE TO MOVE PLAYER

    // Update is called once per frame
    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            //WHEN FINGER TOUCH SCREEN
            if (touch.phase == TouchPhase.Began)
            {
                fingerUp = touch.position;
                fingerDown = touch.position;
            }

            //WHEN FINGER MOVING ON SCREEN
            if (touch.phase == TouchPhase.Moved)
            {
                fingerDown = touch.position;
                CheckSwipe();
            }

            //WHEN FINGER UP FROM SCREEN
            if (touch.phase == TouchPhase.Ended)
            {
                fingerDown = touch.position;
                CheckSwipe();
            }
        }
    }

    void CheckSwipe()
    {
        //GET DISTANCE BETWEEN FINGER DOWN AND UP POSITION AND IF IS BIGGER THEN SWIPETHRESHOLD
        if (Vector2.Distance(fingerDown, fingerUp) > swipeThreshold)
        {
            if (fingerDown.x - fingerUp.x > 0)//SWIPE RIGHT
            {
                transform.position += Vector3.right  * playerXMoveSpeed * Time.deltaTime;
            }
            else //SWIPE LEFT
            {
                transform.position -= Vector3.right * playerXMoveSpeed * Time.deltaTime;
            }

            //CLAMP POSITION ON X AXIS
            Vector3 clampedPosition = transform.position;
            clampedPosition.x = Mathf.Clamp(clampedPosition.x, -playerClampPos, playerClampPos);
            transform.position = clampedPosition;

            fingerUp = fingerDown;
        }
    }
}
