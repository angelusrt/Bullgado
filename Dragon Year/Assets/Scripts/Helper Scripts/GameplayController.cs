using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour {

    public static GameplayController instance;
    public static PlayerController playerController;
    public GameObject fruit_PickUp, bomb_PickUp, fruitb_PickUp , fruitr_PickUp , box_PickUp, boxl_PickUp;
    private int min_X = -10, max_X = 10, y_Pos = 1, min_Z = -10, max_Z = 10 , x = 0, z = 0;
    private Dictionary<Vector3,Rigidbody> Pick_Up;

	// Use this for initialization
	void Awake () {
        Pick_Up = new Dictionary<Vector3,Rigidbody>();
        Pick_Up.Add(transform.GetChild(0).position, transform.GetChild(0).GetComponent<Rigidbody>());
        Pick_Up.Add(transform.GetChild(1).position, transform.GetChild(1).GetComponent<Rigidbody>());

        MakeInstace();
	}   
    void Start() {

        Invoke("StartSpawning", 1/2);
    }
	// Update is called once per frame

	void MakeInstace () {
        if (instance == null) {
            instance = this;
        }
	}
    void StartSpawning() {
        StartCoroutine(SpawPickUps());
    }
    public void CancelSpawning() {
        CancelInvoke("StartSpawning");

    }
    IEnumerator SpawPickUps() {
        yield return new WaitForSeconds(Random.Range(1, 2));

        if (Random.Range(0, 10) >= 2){
            AvailableSpace(out x , out z);
            GameObject newPickFruit = Instantiate(fruit_PickUp, new Vector3(x, y_Pos,z), Quaternion.identity);
            newPickFruit.transform.SetParent(transform, true);
            Pick_Up.Add(new Vector3(x, y_Pos,z), newPickFruit.GetComponent<Rigidbody>());
        }
        if(Random.Range(0,200) >= 190){
            AvailableSpace(out x , out z);
            GameObject newPickBlueFruit = Instantiate(fruitb_PickUp, new Vector3(x, y_Pos,z), Quaternion.identity);
            newPickBlueFruit.transform.SetParent(transform, true);
            Pick_Up.Add(new Vector3(x, y_Pos,z), newPickBlueFruit.GetComponent<Rigidbody>());
        }
        if(Random.Range(0,200) >= 190){
            AvailableSpace(out x , out z);
            GameObject newPickRedFruit = Instantiate(fruitr_PickUp, new Vector3(x, y_Pos,z), Quaternion.identity);
            newPickRedFruit.transform.SetParent(transform, true);
            Pick_Up.Add(new Vector3(x, y_Pos,z), newPickRedFruit.GetComponent<Rigidbody>());
        }
        if(Random.Range(0,50) >= 40){
            AvailableSpace(out x , out z);

            GameObject newPickBox = Instantiate(box_PickUp, new Vector3(x , 5 , z), Quaternion.identity);
            GameObject newPickBoxL = Instantiate(boxl_PickUp, new Vector3(x , 0.1f , z), Quaternion.identity);
            newPickBox.transform.SetParent(transform, true);
            newPickBoxL.transform.SetParent(transform, true);
            Pick_Up.Add(newPickBox.transform.position, newPickBox.GetComponent<Rigidbody>());
            Pick_Up.Add(new Vector3(x, 0.1f, z), newPickBoxL.GetComponent<Rigidbody>());
        }
        if(Random.Range(0,50) <= 20){
            AvailableSpace(out x , out z);

            GameObject newPickBomb = Instantiate(bomb_PickUp, new Vector3(x, y_Pos, z), Quaternion.identity);
            newPickBomb.transform.SetParent(transform, true);
            Pick_Up.Add(new Vector3(x, y_Pos, z), newPickBomb.GetComponent<Rigidbody>());
        }
        Invoke("StartSpawning", 0f);
    }
    void AvailableSpace(out int x , out int z){
        x = Random.Range(min_X,max_X);
        z = Random.Range(min_Z,max_Z);
        if(Pick_Up.ContainsKey(new Vector3(x,y_Pos,z))){
            AvailableSpace(out x , out z);
        }
        /*for (int i = 0; i < playerController.nodes.Count; i++)
        {
            if(x == playerController.nodes[i].position.x || z == playerController.nodes[i].position.z|| Pick_Up.ContainsKey(new Vector3(x,y_Pos,z))){
                AvailableSpace(out x , out z);
            }
        }*/
    }
}
