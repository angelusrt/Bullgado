using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {

	public GameObject[] tilePrefabs;
	public GameObject[] normal_tilePrefab;
	
	private  Transform playerTransform;
	public float tileLenght = 1f;
	public int amnTileOnScreen = 6 ;
	bool spawner = true;

	private List <int> tiles;


	GameObject go;
	// Use this for initialization
	void Start() {
		playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

	 	for (int i = -10; i <= 10; i++)
	 	{
	 		for (int j = -10; j <= 10; j++)
	 		{
	 			go = Instantiate (normal_tilePrefab[0]) as GameObject;
	 			go.transform.SetParent(transform);
	 			go.transform.position = playerTransform.position + new Vector3(i,-1,j);	
			}
	 	}
	}
	
	
	// Update is called once per frame
	private void LateUpdate() {
		InstantiateTile();
	}
	private void InstantiateTile(int prefabIndex = -1){
		VerifyTilesOnSpace();		
	}
	private void VerifyTilesOnSpace(){
		SpawnRandomTile();
	}
	private void SpawnRandomTile(){

		/* for (int i = -5; i <= 5; i++)
		{
			for (int j = -5; j <= 5; j++)
			{
				go = Instantiate (tilePrefabs[0]) as GameObject;
				go.transform.SetParent(transform);
				go.transform.position = playerTransform.position + new Vector3(i,-1,j);	
			}
		}*/
	}
	private void StopSpawningTilesOnOthers(){
		if(go.transform.position == go.transform.position){
			spawner = false;
		}
	}
}
