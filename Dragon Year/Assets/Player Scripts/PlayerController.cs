using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //[HideInInspector]
    public PlayerDirection direction;

    //[HideInInspector]
    public float step_length = 0.2f;

    //[HideInInspector]
    //Faz a cobra não andar continuamente 
    public float movement_frequency = 0.1f;

    private float counter;
    private bool move;

    [SerializeField]
    // Codigo para a calda (como corpos separados) = util para qunado tiver mais de uma entidade (tipo varios ratos) 
    private GameObject tailPrefab;

    private List<Vector3> delta_Position;

    //Pega o corpo 
    private List<Rigidbody> nodes;

    //Separando Corpo(Calda) e cabeça 
    private Rigidbody main_Body;
    private Rigidbody head_Body;

    private Transform tr;

    //Ao comer uma fruta é necessario criar um bloco de calda? Sim.
    private bool create_Node_At_Tail;


    
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
        //main_Body = nodes[1];

    } 
    //Lista e pega os componentes do corpo

    void SetDirectionRandom() {
        //int dirRandom = Random.Range(0,(int)PlayerDirection.COUNT);
        //direction = (PlayerDirection)dirRandom;
    } 
    //Comentado //Torna aleatorio a direção que o personagem vai entrar no jogo //Não preciso disso

    void InitPlayer() {
        //SetDirectionRandom();
        //switch (direction)
        //{
        // case PlayerDirection.RIGHT:
        // nodes[1].position = nodes[0].position - new Vector3(Metrics.NODE, 0f, 0f);
        //        nodes[2].position = nodes[0].position - new Vector3(Metrics.NODE * 2f, 0f, 0f);
        //        break;
        //    case PlayerDirection.LEFT:
        //        nodes[1].position = nodes[0].position + new Vector3(Metrics.NODE, 0f, 0f);
        //        nodes[2].position = nodes[0].position + new Vector3(Metrics.NODE * 2f, 0f, 0f);
        //        break;
        //    case PlayerDirection.UP:
        //        nodes[1].position = nodes[0].position - new Vector3(0f, Metrics.NODE, 0f);
        //        nodes[2].position = nodes[0].position - new Vector3(0f, Metrics.NODE * 2f, 0f);
        //        break;
        //    case PlayerDirection.DOWN:
        //        nodes[1].position = nodes[0].position + new Vector3(0f, Metrics.NODE, 0f);
        //        nodes[2].position = nodes[0].position + new Vector3(0f, Metrics.NODE * 2f, 0f);
        //        break;
        //}
    } 
    //Comentado // Muda as partes do corpo de acordo com a direção dele dita pelo SetDirectionRandom() //Não preciso disso

    void Move() {

        
        Vector3 dPosition = delta_Position[(int)direction]; 

        Vector3 parentPos = head_Body.position + dPosition;
        Vector3 prevPosition;
         
        main_Body.position += head_Body.position + 2 * dPosition; //tem algo de errado aqui!

        for (int i = 0; i < nodes.Count; i++){

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
    }  
    //Checa se é necessario criar um bloco de corpo, por ter comido uma fruta

    void CheckMovementFrequency() {
        counter += Time.deltaTime;
        if(counter >= movement_frequency) {

            counter = 0f;
            move = true;

        }

    } 
    //Permite andar no tempo e na frequência desejada // Não sei se preciso disso 

    public void SetInputDirection(PlayerDirection dir) {

        if (dir == PlayerDirection.UP && direction == PlayerDirection.DOWN || dir == PlayerDirection.DOWN && direction == PlayerDirection.UP ||
           dir == PlayerDirection.RIGHT && direction == PlayerDirection.LEFT || dir == PlayerDirection.LEFT && direction == PlayerDirection.RIGHT) {

            return;
        }
        direction = dir;
        ForceMove();
    } 
    //Comentado //Não preciso disso

    void ForceMove() {
        counter = 0;
         move = false;
         Move();
    } 
    //Comentado //Não preciso disso

    void OnTriggerEnter(Collider target) {
        if (target.tag == Tags.FRUIT) {
            target.gameObject.SetActive(false);
            create_Node_At_Tail = true;

            GameplayController.instance.IncreaseScore();
            AudioManager.instance.Play_PickUpSound();
        }

        if (target.tag == Tags.WALL || target.tag == Tags.BOMB) {
            Time.timeScale = 0f;
            AudioManager.instance.Play_DeadSound();
        }
    }
}
