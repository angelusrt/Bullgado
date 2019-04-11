using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //[HideInInspector]
    public PlayerDirection direction;

    //[HideInInspector]
    public float step_length = 1f;

    //[HideInInspector]
    //Faz a cobra não andar continuamente 
    public float movement_frequency = 0.5f;

    private float counter;
    private bool move;

    private float speed = 8f;
    private float spawnShot;
    private float cadence = 0f;

    [SerializeField]
    private GameObject tailPrefab;
    [SerializeField]
    private GameObject tailBPrefab;

    [SerializeField]
    private GameObject ShotPrefab;

    private List<Vector3> delta_Position;

    //Pega o corpo 
    private List<Rigidbody> nodes;

    //Separando Corpo(Calda) e cabeça 
    private Rigidbody main_Body;
    private Rigidbody head_Body;

    private Transform tr;

    //Ao comer uma fruta é necessario criar um bloco de calda? Sim.
    private bool create_Node_At_Tail;
    private bool create_Nodeb_At_Tail;
    private bool Destroy_Tail;

    private int xMax = 10;
    private int zMax = 10;

    private int CountBlueTails;
    
    void Awake () {
        tr = transform;

        //Permite atrelar a calda
        main_Body = GetComponent<Rigidbody>();

        InitSnakeNodes();
        //InitPlayer();

        delta_Position = new List<Vector3>() { 
            new Vector3(0f, 0f,-step_length),     // -z Left
            new Vector3(-step_length, 0f , 0f),  // -x Down
            new Vector3(0f, 0f,step_length),    // z Right
            new Vector3(step_length, 0f, 0f)   // x Up
        };
    } 
    //Cria a Lista de movimento

    void Update() {
        CheckMovementFrequency();
        SpawnOfShots();
        //Debug.Log(CountBlueTails);
    } 
    //Checa o CheckMovementFrequency()

    void FixedUpdate () {
		if(move) {
            move = false;
            Move();
        }
    } 
    //Roda o Move()

    void InitSnakeNodes() {

        nodes = new List<Rigidbody>();
        nodes.Add(tr.GetChild(0).GetComponent<Rigidbody>());
        nodes.Add(tr.GetChild(1).GetComponent<Rigidbody>());
        nodes.Add(tr.GetChild(2).GetComponent<Rigidbody>());

        head_Body = nodes[0];
        

    } 
    //Lista e pega os componentes do corpo
    void Move() {

        
        Vector3 dPosition = delta_Position[(int)direction]; 

        Vector3 parentPos = head_Body.position;
        Vector3 prevPosition;
         
        main_Body.position += dPosition; 
        head_Body.position += dPosition; 

        for (int i = 1; i < nodes.Count; i++){

            prevPosition = nodes[i].position;

            nodes[i].position = parentPos;
            parentPos = prevPosition;
        }
        if (create_Node_At_Tail) {
            create_Node_At_Tail = false;
            GameObject newNode = Instantiate(tailPrefab, nodes[nodes.Count - 1].position, Quaternion.identity);
            newNode.transform.SetParent(transform, true);
            nodes.Add(newNode.GetComponent<Rigidbody>());

        }
        if (create_Nodeb_At_Tail) {
            create_Nodeb_At_Tail = false;
            GameObject newNode = Instantiate(tailBPrefab, nodes[nodes.Count - 1].position, Quaternion.identity);
            newNode.transform.SetParent(transform, true);
            nodes.Add(newNode.GetComponent<Rigidbody>());
            CountBlueTails++;
        }
        // if(tr.position.x >= xMax){
        //     tr.position = new Vector3(-xMax,1,tr.position.z);
        // }
        // else if(tr.position.x <= -xMax){
        //     tr.position = new Vector3(xMax,1,tr.position.z);
        // }
        // else if(tr.position.z >= zMax){
        //     tr.position = new Vector3(tr.position.x,1,-zMax);
        // }
        // else if(tr.position.z <= -zMax){
        //     tr.position = new Vector3(tr.position.x,1,zMax);
        // }
         if(head_Body.position.x >= xMax){
            head_Body.position = new Vector3(-xMax,1,head_Body.position.z);
            tr.position = head_Body.position;
        }
        else if(head_Body.position.x <= -xMax){
            head_Body.position = new Vector3(xMax,1,head_Body.position.z);
            tr.position = head_Body.position;
        }
        else if(head_Body.position.z >= zMax){
            head_Body.position = new Vector3(head_Body.position.x,1,-zMax);
            tr.position = head_Body.position;
        }
        else if(head_Body.position.z <= -zMax){
            head_Body.position = new Vector3(head_Body.position.x,1,zMax);
            tr.position = head_Body.position;
        }
    }  
    //Checa se é necessario criar um bloco de corpo, por ter comido uma fruta
    void DestroyUntillCheckpoint(){
        if(CountBlueTails == 0){
            Time.timeScale = 0f;
            AudioManager.instance.Play_DeadSound();
        }
        if(CountBlueTails > 0){
            //Destroy(newNo);
            CountBlueTails--;
            if(Destroy_Tail == true){
                nodes[nodes.Count - 1].gameObject.SetActive(false);
                nodes.Remove(nodes[nodes.Count - 1]);
                //Destroy(nodes[nodes.Count - 1].gameObject);
                Destroy_Tail = false;
            }
            //if(){Destroy_Tail = false;}
        }
    }
    void CheckMovementFrequency() {
        counter += Time.deltaTime;
        if(counter >= movement_frequency) {

            counter = 0f;
            move = true;

        }

    } 
    public void SetInputDirection(PlayerDirection dir) {

        if (dir == PlayerDirection.UP && direction == PlayerDirection.DOWN || dir == PlayerDirection.DOWN && direction == PlayerDirection.UP ||
           dir == PlayerDirection.RIGHT && direction == PlayerDirection.LEFT || dir == PlayerDirection.LEFT && direction == PlayerDirection.RIGHT) {

            return;
        }
        direction = dir;
        ForceMove();
    } 
    void ForceMove() {
        counter = 0;
         move = false;
         Move();
    } 
    void OnTriggerEnter(Collider target) {
        if (target.tag == Tags.FRUIT) {
            //target.gameObject.SetActive(false);
            Destroy(target.gameObject);
            create_Node_At_Tail = true;

            GameplayController.instance.IncreaseScore();
            //AudioManager.instance.Play_PickUpSound();
        }
        if (target.tag == Tags.BLUEFRUIT){
            Destroy(target.gameObject);
            create_Nodeb_At_Tail = true;

            GameplayController.instance.IncreaseScore();
        }

        if (target.tag == Tags.WALL || target.tag == Tags.BOMB) {
            Destroy_Tail = true;
            DestroyUntillCheckpoint();        
        }
    }
    void SpawnOfShots()
    {
        if(Input.GetKeyDown(KeyCode.F)){
            if(Time.time > spawnShot){
                spawnShot = cadence + Time.time;
                Instantiate(ShotPrefab, transform.position, Quaternion.identity);   
            }
        }
    }
}
