using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour {

    public static GameplayController instance;
    public GameObject fruit_PickUp, bomb_PickUp, fruitb_PickUp , fruitr_PickUp , box_PickUp, boxl_PickUp;
    private int min_X = -10, max_X = 10, min_Z = -10, max_Z = 10;
    private int y_Pos = 1;
     private List<Rigidbody> Pick_Ups;
    private Text score_Text;
    private int scoreCount;

	// Use this for initialization
	void Awake () {
        Pick_Ups = new List<Rigidbody>();
        Pick_Ups.Add(transform.GetChild(0).GetComponent<Rigidbody>());
        Pick_Ups.Add(transform.GetChild(1).GetComponent<Rigidbody>());



        MakeInstace();
	}   
    void Start() {
        score_Text = GameObject.Find("Score").GetComponent<Text>();

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
            GameObject newPickFruit = Instantiate(fruit_PickUp, new Vector3(Random.Range(min_X, max_X), y_Pos, Random.Range(min_Z, max_Z)), Quaternion.identity);
            newPickFruit.transform.SetParent(transform, true);
            Pick_Ups.Add(newPickFruit.GetComponent<Rigidbody>());
        }
        if(Random.Range(0,200) >= 190){
            GameObject newPickBlueFruit = Instantiate(fruitb_PickUp, new Vector3(Random.Range(min_X, max_X), y_Pos, Random.Range(min_Z, max_Z)), Quaternion.identity);
            newPickBlueFruit.transform.SetParent(transform, true);
            Pick_Ups.Add(newPickBlueFruit.GetComponent<Rigidbody>());
        }
        if(Random.Range(0,200) >= 190){
            GameObject newPickRedFruit = Instantiate(fruitr_PickUp, new Vector3(Random.Range(min_X, max_X), y_Pos, Random.Range(min_Z, max_Z)), Quaternion.identity);
            newPickRedFruit.transform.SetParent(transform, true);
            Pick_Ups.Add(newPickRedFruit.GetComponent<Rigidbody>());
        }
        if(Random.Range(0,50) >= 40){
            int x = Random.Range(min_X, max_X);
            int z = Random.Range(min_Z, max_Z);

            GameObject newPickBox = Instantiate(box_PickUp, new Vector3(x , 5 , z), Quaternion.identity);
            GameObject newPickBoxL = Instantiate(boxl_PickUp, new Vector3(x , 0.1f , z), Quaternion.identity);
            newPickBox.transform.SetParent(transform, true);
            newPickBoxL.transform.SetParent(transform, true);
            Pick_Ups.Add(newPickBox.GetComponent<Rigidbody>());
            Pick_Ups.Add(newPickBoxL.GetComponent<Rigidbody>());
        }
        if(Random.Range(0,50) <= 20){
            GameObject newPickBomb = Instantiate(bomb_PickUp, new Vector3(Random.Range(min_X, max_X), y_Pos, Random.Range(min_Z, max_Z)), Quaternion.identity);
            newPickBomb.transform.SetParent(transform, true);
            Pick_Ups.Add(newPickBomb.GetComponent<Rigidbody>());
        }
        Invoke("StartSpawning", 0f);
    }

    public void IncreaseScore() {
        scoreCount++;
        score_Text.text = "Score: " + scoreCount;
    }
}
