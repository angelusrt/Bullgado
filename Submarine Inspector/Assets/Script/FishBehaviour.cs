using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBehaviour : MonoBehaviour {

	GameObject corpo;
	GameObject calda;
	Quaternion corpoLeft = new Quaternion(0,-0.1f,0,1);
	Quaternion corpoRight = new Quaternion(0,0.1f,0,1);
	Quaternion caldaLeft = new Quaternion(0,0.3f,0,1f);
	Quaternion caldaRight = new Quaternion(0,-0.3f,0,1f);
	Quaternion leftRotate = Quaternion.identity;
	Quaternion rightRotate = new Quaternion (0,1,0,0);
	int countRange;
	int countRange2;
	int dice;
	int dice2;
	int dice3;
	int divisive;
	float x = 1;
	float y = 1;
	float smooth = 2;
	float directive;
	float noise;
	float caldaProgress;
	float caldaProgress2;
	int flip3;
	float progress;
	float progress2;
	bool control;
	bool flipChild = false;
	bool flip = false;
	bool choice = true;
	bool variant = true;
	bool terminator = true;

	void Awake () {
		corpo = transform.GetChild(0).gameObject;
		calda = transform.GetChild(1).gameObject;
	}
	void Update () {
		GenerateBehaviour();
		Flip();
		FlipCalda();
	}
	void GenerateBehaviour (){
		countRange = Random.Range(0, 700);
		countRange2 = Random.Range(0, 300);
		
		if(terminator == true){
			directive = 0;

			dice = Random.Range(1,5);
			choice = true;
			variant = true;
			terminator = false;
		}
		if(choice == true){
			dice2 = Random.Range(1,3);
			choice = false;
		}
		if(variant == true){
			dice3 = Random.Range(1,4);
			variant = false;
		}
		
		// The Fish goes horizontally to one side to another //Can be interrupted // Right or Left
		if(dice == 1 && terminator == false){
			transform.Translate(1 * Time.deltaTime,((y * 0.5f) + (noise * y)) * Time.deltaTime,0);

			if(dice2 == 1){
				if(transform.position.x > -30){
					flip = true;
				}
				else{
					terminator = true;
				}
			}
			else if(dice2 == 2){
				if(transform.position.x < 30){
					flip = false;
				}
				else{
					terminator = true;
				}
			}
			if(countRange == 1){
				terminator = true;
			}

			if(y >= 1){
				control = true;
				//flip3 = 0;
			}
			else if(y <= -1){
				control = false;
				//flip 3 = 3
			}
			if(control == true){
				y += -Time.deltaTime;
			}
			else if(control == false){
				y += Time.deltaTime;
			}
			if(y >= 0 && y <= 0.009){
				noise = Random.Range(0f, 0.3f);
			}
			else if(y <= 0 && y >= -0.05){
				noise = Random.Range(0f, 0.3f);
			}
		}
		
		// The Fish goes down //Can be interrupted //Right or Left
		if(dice == 2 && transform.position.y > -30 && terminator == false){
			transform.Translate(1 * Time.deltaTime,((y * 0.5f) + (noise * y) + (directive) + (noise * directive)) * Time.deltaTime,0);
			
			if(dice2 == 1){
				if(transform.position.x > -30 && flip == false){
					flip = true;
				}
				else if(transform.position.x <= -30){
					terminator = true;
				}
			}
			else if(dice2 == 2){
				if(transform.position.x < 30 && flip == true){
					flip = false;
				}
				else if(transform.position.x >= 30){
					terminator = true;
				}
			}
			if(countRange2 == 1){
				terminator = true;
			}

			if(dice3 == 1){
				divisive = 10;
			}
			else if(dice3 == 2){
				divisive = 7;
			}
			else if(dice3 == 3){
				divisive = 5;
			}

			if(directive > -2){
				directive += -Time.deltaTime/divisive;
			}
			if(directive <= -2){
				terminator = true;
			}
			
			if(y >= 1){
				control = true;
			}
			else if(y <= -1){
				control = false;
			}
			if(control == true){
				y += -Time.deltaTime;
			}
			else if(control == false){
				y += Time.deltaTime;
			}
			if(y >= 0 && y <= 0.009 || y <= 0 && y >= -0.05){
				noise = Random.Range(0f, 0.3f);
			}
		}
		else if(dice == 2 && transform.position.y <= -30){terminator = true;}
		
		// The Fish goes up //Can be interrupted //Right or Left
		if(dice == 3 && transform.position.y < 16 && terminator == false){
			transform.Translate(1 * Time.deltaTime,((y * 0.5f) + (noise * y) + (directive) + (noise * directive)) * Time.deltaTime,0);
			
			if(dice2 == 1){
				if(transform.position.x > -30){
					flip = true;
				}
				else{
					terminator = true;
				}
			}
			else if(dice2 == 2){
				if(transform.position.x < 30){
					flip = false;
				}
				else{
					terminator = true;
				}
			}
			if(countRange2 == 1){
				terminator = true;
			}

			if(dice3 == 1){
				divisive = 10;
			}
			else if(dice3 == 2){
				divisive = 7;
			}
			else if(dice3 == 3){
				divisive = 5;
			}

			if(directive < 2){
				directive += Time.deltaTime/divisive;
			}
			if(directive >= 2){
				terminator = true;
			}
			
			if(y >= 1){
				control = true;
			}
			else if(y <= -1){
				control = false;
			}
			if(control == true){
				y += -Time.deltaTime;
			}
			else if(control == false){
				y += Time.deltaTime;
			}
			if(y >= 0 && y <= 0.009 || y <= 0 && y >= -0.05){
				noise = Random.Range(0f, 0.3f);
			}
		}
		else if(dice == 3 && transform.position.y >= 16){terminator = true;}
		
		//The Fish jumps out of the ocean and comes back //Right or Left //Jump size is variable
		if(dice == 4 && transform.position.y >= 16 && terminator == false){
			transform.Translate(x * Time.deltaTime,(y * noise) * Time.deltaTime,0);
			control = false;
			y = 0;
			
			if(dice2 == 1){
				if(transform.position.x > -30){
					x = -1;
				}
				else{
					terminator = true;
				}
			}
			else if(dice2 == 2){
				if(transform.position.x < 30){
					x = 1;
				}
				else{
					terminator = true;
				}
			}

			if(dice3 == 1){
				noise = 0.5f;
			}
			else if(dice3 == 2){
				noise = 1;
			}
			else if(dice3 == 3){
				noise = 2;
			}

			if(control == true){
				y += -Time.deltaTime;
			}
			else{
				y += Time.deltaTime;
			}

			if(y >= 1){
				control = true;
			}
			else if(y <= 0){
				terminator = true;
			}
		}
		else if(dice == 4 && transform.position.y < 16){terminator = true;}
	}
	void Flip(){
		if(flip == true){
			progress += smooth * Time.deltaTime;
			progress = 	Mathf.Clamp01(progress);
	
			transform.rotation = Quaternion.Lerp(leftRotate, rightRotate, progress);
		}
		if(progress > 0){
			progress2 = 0;
		}
		if(flip == false){
			progress2 += smooth * Time.deltaTime;
			progress2 = Mathf.Clamp01(progress2);
	
			transform.rotation = Quaternion.Lerp(rightRotate, leftRotate, progress2);
		}
		if(progress2 > 0){
			progress = 0;
		}
	}
	void FlipCalda(){
		if(flipChild == true){
			caldaProgress += smooth * Time.deltaTime;
			caldaProgress = Mathf.Clamp01(caldaProgress);
	
			corpo.transform.localRotation = Quaternion.Lerp(corpoRight, corpoLeft, caldaProgress);
			calda.transform.localRotation = Quaternion.Lerp(caldaLeft, caldaRight, caldaProgress);

			if(caldaProgress >=1){
				flipChild = false;
			}
		}					
		if(caldaProgress > 0){
			caldaProgress2 = 0;
		}
		if(flipChild == false){
			caldaProgress2 += smooth * Time.deltaTime;
			caldaProgress2 = Mathf.Clamp01(caldaProgress2);

			corpo.transform.localRotation = Quaternion.Lerp(corpoLeft, corpoRight, caldaProgress2);
			calda.transform.localRotation = Quaternion.Lerp(caldaRight, caldaLeft, caldaProgress2);

			if(caldaProgress2 >= 1){
				flipChild = true;
			}
		}
		if(caldaProgress2 > 0){
			caldaProgress = 0;
		}
	}
}
