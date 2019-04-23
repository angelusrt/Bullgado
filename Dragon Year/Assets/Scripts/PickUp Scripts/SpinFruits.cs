using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinFruits : MonoBehaviour {

	private float speed = -3;
	private float speed2 = -0.0055f;

	void Update () {
		speed2 += Time.deltaTime * 0.0018f;
		speed += Time.deltaTime;
		transform.Rotate(Vector3.up, speed);
		transform.Translate(new Vector3(0,speed2,0));
	}
}
