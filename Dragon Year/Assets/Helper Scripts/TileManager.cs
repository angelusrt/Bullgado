using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {

	public GameObject[] tilePrefabs;
	private  Transform playerTransform;
	public float minZ = -5f;
	public float maxZ = 5f;
	public float minX = -5f;
	public float maxX = 5f;
	public float spawnZ = 0f;
	public float spawnX = 0f;
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
		go.transform.position = playerTransform.position * tileLenght ;
		//spawnX += tileLenght;
	}

}
