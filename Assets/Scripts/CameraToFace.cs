using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraToFace : MonoBehaviour {

    public GameObject gameCam;
    public Vector3 camPos;

    // Use this for initialization
    void Start ()
    {
        gameCam = GameObject.FindGameObjectWithTag("MainCamera");
        camPos = new Vector3(12.5f, 25f, 12.5f);
    }

    // Update is called once per frame
    void Update ()
    {
        gameCam.GetComponent<Transform>().position = camPos;
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "0")
        {
            camPos = new Vector3(12.5f, 25f, 12.5f);
            gameCam.GetComponent<Transform>().eulerAngles = new Vector3(90, -90, 0);
        }

        if (col.gameObject.tag == "1")
        {
            camPos = new Vector3(12.5f, -12.5f, -25f);
            gameCam.GetComponent<Transform>().eulerAngles = new Vector3(0, 0, 0);
        }

        if (col.gameObject.tag == "2")
        {
            camPos = new Vector3(50f, -12.5f, 12.5f);
            gameCam.GetComponent<Transform>().eulerAngles = new Vector3(0, -90, 0);
        }

        if (col.gameObject.tag == "3")
        {
            camPos = new Vector3(12.5f, -12.5f, 50f);
            gameCam.GetComponent<Transform>().eulerAngles = new Vector3(0, 180, 0);
        }
        if (col.gameObject.tag == "4")
        {
            camPos = new Vector3(-25f, -12.5f, 12.5f);
            gameCam.GetComponent<Transform>().eulerAngles = new Vector3(0, 90, 0);
        }
        if (col.gameObject.tag == "5")
        {
            camPos = new Vector3(12.5f, -50f, 12.5f);
            gameCam.GetComponent<Transform>().eulerAngles = new Vector3(-90, -90, 0);
        }
    }
}
