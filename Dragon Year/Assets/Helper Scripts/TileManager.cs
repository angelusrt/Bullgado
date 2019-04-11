using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {

	public GameObject[] tilePrefabs;
	private  Transform playerTransform;
	public float tileLenght = 1f;
	public int amnTileOnScreen = 6 ;
	bool spawner = true;

	private List <int> tiles;


	GameObject go;
	// Use this for initialization
	void Start() {
		playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	private void FixedUpdate() {
		InstantiateTile();
	}

	private void InstantiateTile(int prefabIndex = -1){
		
		go = Instantiate (tilePrefabs[0]) as GameObject;
		go.transform.SetParent(transform);

		tiles = new List<int>(){0,1,2,3,4,5,6,7,8,9};

		VerifyTilesOnSpace();
		//StopSpawningTilesOnOthers();
		
		//go.transform.position = playerTransform.position + new Vector3() * tileLenght ;
		
	}
	private void VerifyTilesOnSpace(){
		SpawnRandomTile();
	}
	private void SpawnRandomTile(){
		go.transform.position = playerTransform.position + new Vector3(Random.Range(-2,2),-1,Random.Range(-2,2)) * tileLenght ;	
	}
	private void StopSpawningTilesOnOthers(){
			if(go.transform.position == go.transform.position){
				spawner = false;
			}
	}
}
