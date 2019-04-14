using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public static PlayerController instance;
    public PlayerDirection direction;


    [SerializeField]
    private GameObject tailPrefab;
    [SerializeField]
    private GameObject tailBPrefab;

    [SerializeField]
    private GameObject ShotPrefab;


    public float step_length = 1f;
    public float movement_frequency = 0.5f;
    private float counter;
    private bool move;
    private float spawnShot;
    private float cadence = 0f;
    private bool create_Node_At_Tail;
    private bool create_Nodeb_At_Tail;
    private bool Destroy_Tail;
    private int xMax = 11;
    private int zMax = 11;


    private List<Vector3> delta_Position;
    private List<Rigidbody> nodes;
    private List<int> BluePosition;
    private Rigidbody main_Body;
    private Rigidbody head_Body;
    private Transform tr;

    
    void Awake () {
        tr = transform;

        //Permite atrelar a calda
        main_Body = GetComponent<Rigidbody>();

        InitSnakeNodes();
        //InitPlayer();
        BluePosition = new List<int>();
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

            BluePosition.Add(nodes.Count - 1);
        }
        if(tr.position.x >= xMax){
            for (int i = 0; i <= nodes.Count - 1; i++)
            { 
                nodes[0].position = new Vector3(-xMax + 1,1,nodes[0].position.z);
                nodes[i].position = nodes[0].position;
                tr.position = nodes[0].position;
            }
        }
        if (tr.position.x <= -xMax){
            for (int i = 0; i <= nodes.Count - 1; i++)
            { 
                nodes[0].position = new Vector3(xMax - 1,1,nodes[0].position.z);
                nodes[i].position = nodes[0].position;
                tr.position = nodes[0].position;
            }
        }
        if (tr.position.z >= zMax){
            for (int i = 0; i <= nodes.Count - 1; i++)
            { 
                nodes[0].position = new Vector3(nodes[0].position.x,1,-zMax + 1);
                nodes[i].position = nodes[0].position;
                tr.position = nodes[0].position;
            }
        }
        if (tr.position.z <= -zMax){
            for (int i = 0; i <= nodes.Count - 1; i++)
            { 
                nodes[0].position = new Vector3(nodes[0].position.x,1,zMax - 1);
                nodes[i].position = nodes[0].position;
                tr.position = nodes[0].position;
            }
        }
        /*if(tr.position.x >= xMax){tr.position = new Vector3(-xMax + 1,1,nodes[].position.z);}
         else if(tr.position.x <= -xMax){tr.position = new Vector3(xMax - 1,1,tr.position.z);}
         else if(tr.position.z >= zMax){tr.position = new Vector3(tr.position.x,1,-zMax + 1);}
         else if(tr.position.z <= -zMax){tr.position = new Vector3(tr.position.x,1,zMax - 1);}*/

        /*if(head_Body.position.x >= xMax){head_Body.position = new Vector3(-xMax,1,head_Body.position.z);
                                                tr.position = head_Body.position;}
        else if(head_Body.position.x <= -xMax){head_Body.position = new Vector3(xMax,1,head_Body.position.z);
                                                tr.position = head_Body.position;}
        else if(head_Body.position.z >= zMax){head_Body.position = new Vector3(head_Body.position.x,1,-zMax);
                                                tr.position = head_Body.position;}
        else if(head_Body.position.z <= -zMax){head_Body.position = new Vector3(head_Body.position.x,1,zMax);
                                                tr.position = head_Body.position;}*/
    }  
    //Checa se é necessario criar um bloco de corpo, por ter comido uma fruta
    void DestroyUntillCheckpoint(){
        if(BluePosition.Count == 0){
            Time.timeScale = 0f;
            //AudioManager.instance.Play_DeadSound();
            Debug.Log("### Game Over ###");
        }
        if(BluePosition.Count > 0){

            if(Destroy_Tail == true){
                int NodesDestructionNum = (nodes.Count -1) - (BluePosition[BluePosition.Count -1]);

                for ( int i = 0 ; i <= NodesDestructionNum; i++)
                {
                    nodes[nodes.Count - 1].gameObject.SetActive(false);
                    nodes.Remove(nodes[nodes.Count - 1]);  
                }
                BluePosition.Remove(BluePosition[BluePosition.Count - 1]);
                Destroy_Tail = false;
            }
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
            //AudioManager.instance.Play_PickUpSound();
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
