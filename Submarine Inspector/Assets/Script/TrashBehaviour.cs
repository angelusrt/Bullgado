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
		transform.Rotate(0,-25,-5);
	}
	void Update () {
		FloatTrash();	
		RotatingTrash();
	}
	void FloatTrash(){
		transform.Translate(new Vector3(0,y * Time.deltaTime,0));

		if(y >= 0.5f){
			floatBack = true;
		}
		if(floatBack == true){
			y -= Time.deltaTime/2f;
		}
		if(y <= -0.5f){
			floatBack = false;
		}
		if(floatBack == false){
			y += Time.deltaTime/2f;
		}
	}
	void RotatingTrash(){
		transform.Rotate(new Vector3(0, rotatingY, 0));
		transform.Rotate(new Vector3(0, 0, rotatingZ));

		if(rotatingY >= 0.6f){
			rotateYBack = true;
		}
		if(rotateYBack == true){
			rotatingY -= Time.deltaTime/2;
		}
		if(rotatingY <= -0.6f){
			rotateYBack = false;
		}
		if(rotateYBack == false){
			rotatingY += Time.deltaTime/2;
		}

		if(rotatingZ >= 0.3f){
			rotateZBack = true;
		}
		if(rotateZBack == true){
			rotatingZ -= Time.deltaTime/3;
		}
		if(rotatingZ <= -0.3f){
			rotateZBack = false;
		}
		if(rotateZBack == false){
			rotatingZ += Time.deltaTime/3;
		}
	}
}
