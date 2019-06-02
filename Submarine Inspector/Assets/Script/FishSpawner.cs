using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour {
	public int timeInSecs = 10;
	public int sardinhaMax = 50;
	public int crownFishMax = 25;
	int sardinhaQuantity;
	int crownFishQuantity;
	int coinRL;
	int x;
	float update;
	float update2;
	public GameObject sardinhaPrefab;
	public GameObject crownFishPrefab;
	Vector3 location;
	void Awake(){
	}
	void Update () {
		update += Time.deltaTime;
		update2 += Time.deltaTime;
		if(update >= timeInSecs && sardinhaQuantity <= sardinhaMax){
			Instantiatesardinha();
			update = 0;
			sardinhaQuantity++;
		}
		if(update2 >= 2 * timeInSecs && crownFishQuantity <= crownFishMax){
			InstantiateCrownFish();
			update2 = 0;
			crownFishQuantity++;
		}
	}
	void Instantiatesardinha(){
		coinRL = Random.Range(1,3);
		if(coinRL == 1){
			x = -52;
		}
		else if(coinRL == 2){
			x = 52;
		}
		location = new Vector3(x, Random.Range(-30,10), 3);
		GameObject newSardinha = Instantiate(sardinhaPrefab,location,Quaternion.identity);
		newSardinha.transform.SetParent(transform,true);
	}
	void InstantiateCrownFish(){
		coinRL = Random.Range(1,3);
		if(coinRL == 1){
			x = -52;
		}
		else if(coinRL == 2){
			x = 52;
		}
		location = new Vector3(x, Random.Range(-30,10), 3);
		GameObject newCrownFish = Instantiate(crownFishPrefab,location,Quaternion.identity);
		newCrownFish.transform.SetParent(transform,true);
	}
}
