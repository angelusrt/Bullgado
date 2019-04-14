using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    public float timer;
    public int inSecs;
    public bool left;
    public bool right;
    public bool up;
    public bool down;
    private PlayerController playerController;

    private int horizontal = 0, vertical = 0;

    public enum Axis {
        Horizontal,
        Vertical
    }

    // Use this for initialization
    void Awake() {
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update () {
        horizontal = 0;
        vertical = 0;

        GetKeyboardInput();
        SetMovement();
		
	}

    void GetKeyboardInput() {
        //horizontal = (int)Input.GetAxisRaw("Horizontal");
        //vertical = (int)Input.GetAxisRaw("Vertical");

        horizontal = GetAxisRaw(Axis.Horizontal);
        vertical = GetAxisRaw(Axis.Vertical);

        if (horizontal != 0) {
            vertical = 0;
        }

    }

    void SetMovement() {
        if (vertical != 0) {
            playerController.SetInputDirection((vertical == 1) ? PlayerDirection.UP : PlayerDirection.DOWN);
        }
        else if (horizontal != 0)
        {
            playerController.SetInputDirection((horizontal == 1) ? PlayerDirection.RIGHT : PlayerDirection.LEFT);
        }
    }
    public int GetAxisRaw(Axis axis){
        if(axis == Axis.Horizontal){

            left = Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A);
            right = Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D);

            if(left){ return -1; }
            if(right){ return 1; }

            /* if(Input.GetKey(KeyCode.LeftArrow) == true){
                timer += Time.deltaTime;
                inSecs = (int)timer % 60;
                if(Input.GetKey(KeyCode.LeftArrow) == false){
                    inSecs = 0;
                    return 0;
                }
                else if(Input.GetKey(KeyCode.LeftArrow) == true || inSecs >= 10){
                    left = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
                    if(left){return -1;}
                    inSecs = 0;
                }
            }*/
            return 0;
        }
        else if(axis == Axis.Vertical){
            up = Input.GetKeyDown(KeyCode.UpArrow) ||Input.GetKeyDown(KeyCode.W);
            down = Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S);

            if(up){ return 1; }
            if(down){ return -1; }

            return 0;
        } 
        return 0;
    }
}
