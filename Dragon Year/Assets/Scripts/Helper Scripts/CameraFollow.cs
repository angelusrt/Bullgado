using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {


	public Transform target;
	public float smoothSpeed = 0.15f;
	public float smoothRotateSpeed;
	public Vector3 offset;

	void FixedUpdate(){
		Vector3 desiredPosition = target.position + offset;
		Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
		transform.position = smoothedPosition;

		var targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothRotateSpeed * Time.deltaTime);
	}
	void LateUpdate(){
		
		//transform.LookAt(target);
	}

}
