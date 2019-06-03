using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineBehaviour : MonoBehaviour {
	GameObject submarineSprite;
	GameObject spotLight;
	GameObject life1;
	GameObject life2;
	GameObject life3;
	GameObject player;
	Quaternion rightSprite;
	Quaternion leftSprite;
	Quaternion rightRotation;
	Quaternion leftRotation;
	public float speed = 0.12f;
	public float smooth = 10;
	float waitForSecs;
	float progress = 0;
	float progress2 = 0;
	int lifeCount= 3;
	int trashCollected;
	int trashCollectedTotal;
	bool turnItBigger;
	bool bounce = false;
	bool softFall = false;
	//float rotationSmoothtly = -90;
	float pressedLongForceV, pressedLongForceH;
	void Awake(){
		player = this.gameObject;
		submarineSprite = player.transform.GetChild(0).gameObject;
		spotLight = player.transform.GetChild(1).gameObject;
		life1 = player.transform.GetChild(2).gameObject;
		life2 = player.transform.GetChild(3).gameObject;
		life3 = player.transform.GetChild(4).gameObject;

		rightRotation = spotLight.transform.rotation;
		leftRotation = new Quaternion(-0.7f, -0.1f, 0.7f, -0.1f);

		rightSprite = Quaternion.identity;
		leftSprite = new Quaternion(0,-1,0,0);
	}
	void FixedUpdate () {
		Movement();
		FlipChild();
		Life();
		//Debug.Log(submarineSprite.transform.rotation);
	}
	void Movement(){
		if((Input.GetAxis("Vertical") == -1 && pressedLongForceV >= -1 )||( Input.GetAxis("Vertical") == 1 && pressedLongForceV <= 1 ) && bounce == false){
			pressedLongForceV += Time.deltaTime * Input.GetAxis("Vertical");
		}
		else if((Input.GetAxis("Vertical") == -1  && pressedLongForceV < -1 )||( Input.GetAxis("Vertical") == 1 && pressedLongForceV > 1 && bounce == false)){
			pressedLongForceV = Input.GetAxis("Vertical");
		}
		else if(Input.GetAxis("Vertical") == 0 && pressedLongForceV > 0 && bounce == false){
			pressedLongForceV -= Time.deltaTime;
			if(pressedLongForceV < 0){ pressedLongForceV = 0;}
		}
		else if(Input.GetAxis("Vertical") == 0 && pressedLongForceV < 0 && bounce == false){
			pressedLongForceV += Time.deltaTime;
			if(pressedLongForceV > 0){ pressedLongForceV = 0;}
		}

		if((Input.GetAxis("Horizontal") == -1 && pressedLongForceH >= -1 )||( Input.GetAxis("Horizontal") == 1 && pressedLongForceH <= 1 ) && bounce == false){
			pressedLongForceH += Time.deltaTime * Input.GetAxis("Horizontal");
		}
		else if((Input.GetAxis("Horizontal") == -1  && pressedLongForceH < -1 )||( Input.GetAxis("Horizontal") == 1 && pressedLongForceH > 1 ) && bounce == false){
			pressedLongForceH = Input.GetAxis("Horizontal");
		}
		else if(Input.GetAxis("Horizontal") == 0 && pressedLongForceH > 0 && bounce == false){
			pressedLongForceH -= Time.deltaTime;
			if(pressedLongForceH < 0){ pressedLongForceH = 0;}
		}
		else if(Input.GetAxis("Horizontal") == 0 && pressedLongForceH < 0 && bounce == false){
			pressedLongForceH += Time.deltaTime;
			if(pressedLongForceH > 0){ pressedLongForceH = 0;}
		}
		if(transform.position.y >= 16){
			bounce = true;
		}
		if(bounce == true){
			if(softFall == false){
				pressedLongForceV += -Time.deltaTime;
			}
			if(pressedLongForceV <= -1){
				softFall = true;
			}
			if(softFall == true){
				pressedLongForceV += Time.deltaTime;

				if(pressedLongForceV >= 0){
					pressedLongForceV = 0;
					softFall = false;
					bounce = false;
				}
			}
		}
		if(transform.position.x >= 48){
			transform.position = new Vector3(48, transform.position.y, transform.position.z);
		}
		else if(transform.position.x <= -48){
			transform.position = new Vector3(-48, transform.position.y, transform.position.z);
		}
		else if(transform.position.y <= -30){
			transform.position = new Vector3(transform.position.x, -30, transform.position.z);
		}

		transform.Translate(pressedLongForceH * speed, 0, 0);
		transform.Translate(0,pressedLongForceV * speed, 0);
	}

	void FlipChild(){
		//Quaternion leftRotation = Quaternion.Euler(160,-270,0);
		//Quaternion rightRotation = Quaternion.Euler(160,-90,0);
		
		if(pressedLongForceH < 0 && progress <= 1 && spotLight.transform.rotation != leftRotation && submarineSprite.transform.rotation != leftSprite){
			//spotLight.transform.rotation = leftRotation;

			progress += smooth * Time.deltaTime;
			progress = 	Mathf.Clamp01(progress);

			submarineSprite.transform.rotation = Quaternion.Lerp(rightSprite, leftSprite, progress);
			spotLight.transform.rotation = Quaternion.Lerp(rightRotation, leftRotation, progress);
		}
		if(progress > 0){
			progress2 = 0;
		}
		if(pressedLongForceH > 0 && progress2 <=1 && spotLight.transform.rotation != rightRotation && submarineSprite.transform.rotation != rightSprite){
			//spotLight.transform.rotation = rightRotation;	

			progress2 += smooth * Time.deltaTime;
			progress2 = Mathf.Clamp01(progress2);

			submarineSprite.transform.rotation = Quaternion.Lerp(leftSprite, rightSprite, progress2);
			spotLight.transform.rotation = Quaternion.Lerp(leftRotation, rightRotation, progress2);
		} 
		if(progress2 > 0){
			progress = 0;
		}
	}
	void Life(){
		LifePopping();
	}
	void LifePopping(){
		waitForSecs += Time.deltaTime;
		if(waitForSecs >= 1 && turnItBigger == true){
			life1.transform.localScale = new Vector2(0.017f,0.017f);
			life2.transform.localScale = new Vector2(0.022f,0.022f);
			life3.transform.localScale = new Vector2(0.017f,0.017f);
			waitForSecs = 0;
			turnItBigger = false;
		}
		else if(waitForSecs >= 0.5f && turnItBigger == false){
			life1.transform.localScale = new Vector2(0.015f,0.015f);
			life2.transform.localScale = new Vector2(0.02f,0.02f);
			life3.transform.localScale = new Vector2(0.015f,0.015f);
			waitForSecs = 0;
			turnItBigger = true;
		}
	}
	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.tag == "Trash"){
			trashCollected ++;
			trashCollectedTotal++;
			if(trashCollected <= 3){
				trashCollected ++;
				trashCollectedTotal++;
				Destroy(col.gameObject);
			}
		}
		if(col.gameObject.tag == "TrashHole"){
			trashCollected = 0;
		}
	}
}
