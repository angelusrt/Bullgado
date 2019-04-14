using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour {

	public GameObject tilePrefab;
	public static int size = 21;
	Tile[,] tiles;

	GameObject tileGO;
	void Awake(){
		tiles = new Tile[size,size];

		for (int i = 0; i < size; i++)
		{
			for (int j = 0; j < size; j++)
			{
				tiles[i,j] = new Tile(i -10 + (int)transform.position.x,0,j -10 + (int)transform.position.y);

				//tileGO = new GameObject("Tile_" + tiles[i,j].x + "_" + tiles[i,j].y); //nome
				tileGO =  Instantiate(tilePrefab, new Vector3(tiles[i,j].x , tiles[i,j].y , tiles[i,j].z), Quaternion.identity) as GameObject;
				tileGO.name = "Tile_" + tiles[i,j].x + "_" + tiles[i,j].z; //nome
				tileGO.transform.SetParent(this.transform, true);
				//tileGO.transform.position = new Vector3(tiles[i,j].x , tiles[i,j].y , tiles[i,j].z);


			}
		}
	}
}
