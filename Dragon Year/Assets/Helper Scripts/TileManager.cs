using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {

	public GameObject[] tilePrefabs;
	private  Transform playerTransform;
	public float tileLenght = 1f;
	public int amnTileOnScreen = 6 ;
	// Use this for initialization
	void Start() {
		playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	private void FixedUpdate() {
		SpawnTile();
	}

	private void SpawnTile(int prefabIndex = -1){
		GameObject go;
		go = Instantiate (tilePrefabs[0]) as GameObject;
		go.transform.SetParent(transform);
		go.transform.position = playerTransform.position + new Vector3(Random.Range(-2,2),-1,Random.Range(-2,2)) * tileLenght ;
		//go.transform.position = playerTransform.position + new Vector3() * tileLenght ;
		
	}

}
