using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSpawner : MonoBehaviour
{
    public GameObject sodaCanPrefab;
    Vector3 location;
    public int totalTrashNum = 8;
    void Start()
    {
        for (int i = 0; i <= totalTrashNum; i++)
        {
            InstantiateTrash();
        }
    }
    void InstantiateTrash(){
		location = new Vector3(Random.Range(-40,41), Random.Range(-30,10), 3);
		GameObject newSardinha = Instantiate(sodaCanPrefab,location,Quaternion.identity);
		newSardinha.transform.SetParent(transform,true);
    }

}
