using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {
    
    private float t , t2 , t3 , t4 , t5 , t6 , t7 , t8;
    private bool left , right , up , down;
    private int countPressed , countPressed2 , countPressed3 , countPressed4;
    private bool isCountPossible = false;
    private bool isCountPossible2 = false;
    private bool isCountPossible3 = false;
    private bool isCountPossible4 = false;    
    private bool momentToDash = false;
    private bool momentToDash2 = false;
    private bool momentToDash3 = false;
    private bool momentToDash4 = false;
    int counterH = 1;
    int counterV = 0;
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
    void FixedUpdate () {
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
        left = Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A);
        right = Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D);
        up = Input.GetKeyDown(KeyCode.UpArrow) ||Input.GetKeyDown(KeyCode.W);
        down = Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S);

        if(axis == Axis.Horizontal){
            CountPressed(ref left, ref isCountPossible , ref countPressed);
            CountPressed(ref right, ref isCountPossible2 , ref countPressed2);

            if(counterH == 0){
                if(left){
                    counterH = 1;
                    counterV = 0;
                    return -1; 
                }
                if(right){ 
                    counterH = 1;
                    counterV = 0;
                    return 1; 
                }
            }
             
            CountPossibility(ref isCountPossible, ref t);
            Dash(ref t, ref countPressed , ref momentToDash, ref isCountPossible);
            if(momentToDash){
                t2 += Time.deltaTime;
                StopDash(ref t, ref t2 , ref isCountPossible ,ref countPressed , ref left, ref momentToDash);
                left = true;
                if(left){return -1;}
            }
            
            CountPossibility(ref isCountPossible2, ref t3);
            Dash(ref t3, ref countPressed2 , ref momentToDash2, ref isCountPossible2);
            if(momentToDash2){
                t4 += Time.deltaTime;
                StopDash(ref t3, ref t4 , ref isCountPossible2 ,ref countPressed2 , ref right, ref momentToDash2);
                right = true;
                if(right){return 1;}
            }
            
            return 0;
        }
        else if(axis == Axis.Vertical){
            CountPressed(ref up, ref isCountPossible3 , ref countPressed3);
            CountPressed(ref down, ref isCountPossible4 , ref countPressed4);

            if(counterV == 0){
                if(up){ 
                    counterH = 0;
                    counterV = 1;
                    return 1; 
                }
                if(down){
                    counterH = 0;
                    counterV = 1; 
                    return -1; 
                }
            }
            
            CountPossibility(ref isCountPossible3, ref t5);
            Dash(ref t5, ref countPressed3 , ref momentToDash3, ref isCountPossible3);
            if(momentToDash3){
                t6 += Time.deltaTime;
                StopDash(ref t5, ref t6 , ref isCountPossible3 ,ref countPressed3 , ref up, ref momentToDash3);
                up = true;
                if(up){return 1;}
            }
             
            CountPossibility(ref isCountPossible4, ref t7);
            Dash(ref t7, ref countPressed4 , ref momentToDash4, ref isCountPossible4);
            if(momentToDash4){
                t8 += Time.deltaTime;
                StopDash(ref t7, ref t8 , ref isCountPossible4 ,ref countPressed4 , ref down, ref momentToDash4);
                down = true;
                if(down){return -1;}
            }
            
            return 0;
        } 

        return 0;
    }
    void CountPressed(ref bool button, ref bool iCP , ref int cP){
        if(button == true){
            iCP = true;
            cP++;
        }
    }
    void CountPossibility(ref bool iCP, ref float tempo){
        if(iCP == true){tempo += Time.deltaTime;}
        else{tempo = 0f;}
    }
    void Dash(ref float tempo, ref int cP , ref bool mTD, ref bool iCP){

        if(tempo % 60f <= 0.3f && cP >= 2){mTD = true;}
        else if (tempo % 60f > 0.3f && cP < 2){
            cP = 0;
            iCP = false;
        }
    }
    void StopDash(ref float tempo, ref float tempo2 ,ref bool iCP ,ref int cP ,ref bool button ,ref bool mTD){
        if(tempo2 % 60f >= 0.1f){
            iCP = false;
            cP = 0;
            button = false;
            tempo = 0f;
            tempo2 = 0f;
            mTD = false;
        }
    }

}

