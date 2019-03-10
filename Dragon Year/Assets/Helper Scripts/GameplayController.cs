using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour {

    public static GameplayController instance;

    public GameObject fruit_PickUp, bomb_PickUp;

    private float min_X = -23f, max_X = 23f, min_Z = -25f, max_Z = 25f;
    private float y_Pos = 1f;
     

    public float minTimeRange = 1f;
    public float maxTimeRange = 2f;
    public int minRange = 0;
    public int maxRange = 10;

    private Text score_Text;
    private int scoreCount;

	// Use this for initialization
	void Awake () {
        MakeInstace();
	}   
    void Start() {
        //score_Text = GameObject.Find("Score").GetComponent<Text>();

        Invoke("StartSpawning", minTimeRange);
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
        yield return new WaitForSeconds(Random.Range(minTimeRange, maxTimeRange));

        if (Random.Range(minRange, maxRange) >= 2){
            Instantiate(fruit_PickUp, new Vector3(Random.Range(min_X, max_X), y_Pos, Random.Range(min_Z, max_Z)), Quaternion.identity);
        }
        else {
            Instantiate(bomb_PickUp, new Vector3(Random.Range(min_X, max_X), y_Pos, Random.Range(min_Z, max_Z)), Quaternion.identity);
        }
        Invoke("StartSpawning", minTimeRange);
    }

    public void IncreaseScore() {
        scoreCount++;
        score_Text.text = "Score:" + scoreCount;
    }
}
