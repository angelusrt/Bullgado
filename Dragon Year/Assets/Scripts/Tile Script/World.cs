using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {
	public float renderDist;
	Dictionary<Vector3, Chunk> chunkMap;
	public GameObject chunkGO;
	// Use this for initialization
	void Awake(){
		chunkMap = new Dictionary<Vector3,Chunk>();
	}
	void Start () {

	}
	    
	// Update is called once per frame
	void Update () {
		FindChunkToLoad();
		DeleteChunks();
	}
	void FindChunkToLoad(){

		int xPos = (int)transform.position.x;
		int zPos = (int)transform.position.z;

		for (int i = xPos - Chunk.size; i < xPos + (2*Chunk.size); i+= Chunk.size)
		{
			for (int j = zPos - Chunk.size; j < zPos + (2*Chunk.size); j+= Chunk.size)
			{
				MakeChunkAt(i,j);
			}
		}

	}
	void MakeChunkAt(int x, int z){

		x = Mathf.FloorToInt(x / (float)Chunk.size) * Chunk.size;
		z = Mathf.FloorToInt(z / (float)Chunk.size) * Chunk.size;

		if(chunkMap.ContainsKey(new Vector3(x,0,z)) == false){
			GameObject go = Instantiate(chunkGO, new Vector3(x,0,z),Quaternion.identity);
			chunkMap.Add(new Vector3(x, 0, z),go.GetComponent<Chunk>());
		}
	}
	void DeleteChunks(){

		List<Chunk> deleteChunks = new List<Chunk>(chunkMap.Values);
		Queue<Chunk> deleteQueue = new Queue<Chunk>();

		for (int i = 0; i < deleteChunks.Count; i++)
		{
			float distance = Vector3.Distance(transform.position, deleteChunks[i].transform.position);

			if(distance > renderDist * Chunk.size){

				deleteQueue.Enqueue(deleteChunks[i]);
			}
		}
		while (deleteQueue.Count > 0)
		{
			Chunk chunk = deleteQueue.Dequeue();
			chunkMap.Remove(chunk.transform.position);
			Destroy(chunk.gameObject);
		}
	}
}
