using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float ballSpeed;

    private Vector3 defaultSpawnP;
    private int deathCount = 0;

    [SerializeField]
    Rigidbody ballRB;

    public float xSpeed;
    public float ySpeed;

    //public GameObject;

    // Use this for initialization
    void Start() {
        ballRB = GetComponent<Rigidbody>();
        defaultSpawnP = transform.position;
	}
	
	// Update is called once per frame
	void Update() {
        xSpeed = Input.GetAxis("Horizontal");
        ySpeed = Input.GetAxis("Vertical");

        
        if(transform.position.y <= -35) {
            Respawn();
        }
    }

    void Respawn() {
        transform.position = defaultSpawnP;
        ballRB.velocity = new Vector3(0,0,0);
        deathCount++;
    }

    void OnTriggerStay(Collider col)
    {
        if(col.gameObject.tag == "0")
        {
            Physics.gravity = new Vector3(0f, -9.5f, 0f);
            ballRB.AddForce(new Vector3(-ySpeed, 0, xSpeed) * ballSpeed * Time.deltaTime);
        }

        if(col.gameObject.tag == "1")
        {
            Physics.gravity = new Vector3(0f, 0f, 9.5f);
            ballRB.AddForce(new Vector3(xSpeed, ySpeed, 0) * ballSpeed * Time.deltaTime);
        }

        if (col.gameObject.tag == "2")
        {
            Physics.gravity = new Vector3(-9.5f, 0f, 0f);
            ballRB.AddForce(new Vector3(0, ySpeed, xSpeed) * ballSpeed * Time.deltaTime);
        }

        if (col.gameObject.tag == "3")
        {
            Physics.gravity = new Vector3(0f, 0f, -9.5f);
            ballRB.AddForce(new Vector3(-xSpeed, ySpeed, 0) * ballSpeed * Time.deltaTime);
        }
        if (col.gameObject.tag == "4")
        {
            Physics.gravity = new Vector3(9.5f, 0f, 0f);
            ballRB.AddForce(new Vector3(0, ySpeed, -xSpeed) * ballSpeed * Time.deltaTime);
        }
        if (col.gameObject.tag == "5")
        {
            Physics.gravity = new Vector3(0f, 9.5f, 0f);
            ballRB.AddForce(new Vector3(ySpeed, 0, xSpeed) * ballSpeed * Time.deltaTime);
        }
    }
}
