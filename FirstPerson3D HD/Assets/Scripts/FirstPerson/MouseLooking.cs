using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLooking : MonoBehaviour{

	public float mouseSensitivity = 80f;
	public Transform playerBody;

    float xRotation = 0f;

    public bool cursorLocked = true;

    // Start is called before the first frame update
    void Start(){
        LockCursor();
    }

    // Update is called once per frame
    void Update(){
        if (!GameManager.gameManager.CurrentlyInteracting())
        {
            //we can look around
            LookAround();
            //and the cursor is locked
            LockCursor();
        }
        else
        {
            //unlock cursor to inspect stuff and  when game is paused
            UnlockCursor();
        }
        //If Game is not paused and ... 
        //if (!GameManager.gameManager.GameIsOnPause()){
        //    //... if nothing is inspected
        //    if (!GameManager.gameManager.InspectorOpen()){
        //        //we can look around
        //        LookAround();

        //        //and the cursor is locked
        //        LockCursor();
        //    }
        //    else{
        //        //unlock cursor to inspect stuff
        //        UnlockCursor();
        //    }
        //}
        //else{
        //    //unlock cursor when game is paused
        //    UnlockCursor();
        //}

    }

    void LockCursor(){
        cursorLocked = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void UnlockCursor(){
        cursorLocked = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void LookAround(){
        //Get Rotation around X or Y Axis, multiplied with the sensitivity and Time.deltaTime to be independet from the framerate
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //keep track of xRotation to clamp rotation (so the camera does not over-rotate
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //Quaternion is responsible for rotation
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
