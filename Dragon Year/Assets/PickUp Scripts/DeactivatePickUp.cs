using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivatePickUp : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("Deactivate", 6f); 
	}
	
	// Update is called once per frame
	void Deactivate () {
        gameObject.SetActive(false);
	}
}
 