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
		VerifyTilesOnSpace();
		//go.transform.position = playerTransform.position + new Vector3() * tileLenght ;
		
	}
	private void VerifyTilesOnSpace(){
		SpawnRandomTile();
	}
	private void SpawnRandomTile(){

		for (int i = -2; i <= 2; i++)
		{
			for (int j = -2; j <= 2; j++)
			{
				go = Instantiate (tilePrefabs[0]) as GameObject;
				go.transform.SetParent(transform);
				go.transform.position = playerTransform.position + new Vector3(i,-1,j);	
			}
		}
	}
	private void StopSpawningTilesOnOthers(){
			if(go.transform.position == go.transform.position){
				spawner = false;
			}
	}
}
