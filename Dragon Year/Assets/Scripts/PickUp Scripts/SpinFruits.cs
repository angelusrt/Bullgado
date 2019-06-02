using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinFruits : MonoBehaviour {

	public float speed = 1;
    
	void Update () {

		if(speed <= 1 ){speed += Time.deltaTime;}
		else if(speed >= 10){speed -= Time.deltaTime;}
		transform.Rotate(Vector3.up, speed * Time.deltaTime);
	}
}
