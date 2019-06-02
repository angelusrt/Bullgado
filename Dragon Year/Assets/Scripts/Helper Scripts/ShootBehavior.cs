using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBehavior : MonoBehaviour
{
    //public PlayerController p;
    public int direct;
    private float speed = 18f;
    private float spawnShot;
    private float cadence = 0f;


    private List<Vector3> delta_Position;
   

    // Start is called before the first frame update
    void Awake()
    {
        delta_Position = new List<Vector3>() { 
            new Vector3(0f, 0f,-1),     // -z Left
            new Vector3(-1, 0f , 0f),  // -x Down
            new Vector3(0f, 0f,1),    // z Right
            new Vector3(1, 0f, 0f)   // x Up
        };

        //direct = playerControl.direction;
        //Debug.Log(direct);
  
    }
    void Start()
    {
        //Debug.Log(p.direction);
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if(transform.position.z > 10f ||transform.position.z < -10f  ||transform.position.x > 10f  ||transform.position.x < -10f ){
            Destroy(this.gameObject);
        }
    
    }
    
    //void TripleShot(){}
}

