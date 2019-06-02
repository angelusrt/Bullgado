using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBehaviour : MonoBehaviour {
	
	float y;
	float rotatingY;
	float rotatingZ;
	bool floatBack = false;
	bool rotateYBack = false;
	bool rotateZBack = false;

	void Awake(){
		transform.Rotate(0,-30,-20);
	}
	void Update () {
		FloatTrash();	
		RotatingTrash();
	}
	void FloatTrash(){
		transform.Translate(new Vector3(0,y * Time.deltaTime,0));

		if(y >= 0.6f){
			floatBack = true;
		}
		if(floatBack == true){
			y -= Time.deltaTime/1.5f;
		}
		if(y <= -0.6f){
			floatBack = false;
		}
		if(floatBack == false){
			y += Time.deltaTime/1.5f;
		}
	}
	void RotatingTrash(){
		transform.Rotate(new Vector3(0, rotatingY, 0));
		transform.Rotate(new Vector3(0, 0, rotatingZ));

		if(rotatingY >= 0.8f){
			rotateYBack = true;
		}
		if(rotateYBack == true){
			rotatingY -= Time.deltaTime/1.7f;
		}
		if(rotatingY <= -0.8f){
			rotateYBack = false;
		}
		if(rotateYBack == false){
			rotatingY += Time.deltaTime/1.7f;
		}

		if(rotatingZ >= 0.6f){
			rotateZBack = true;
		}
		if(rotateZBack == true){
			rotatingZ -= Time.deltaTime/1.5f;
		}
		if(rotatingZ <= -0.6f){
			rotateZBack = false;
		}
		if(rotateZBack == false){
			rotatingZ += Time.deltaTime/1.5f;
		}
	}
}
