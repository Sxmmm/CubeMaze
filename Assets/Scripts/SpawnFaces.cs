using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFaces : MonoBehaviour {

    public GameObject face;

    //((i+5)*(i+2))

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < 6; i++)
        {

            GameObject go = Instantiate(face, new Vector3(0, (5*(i+2)), 0), Quaternion.identity);
            go.gameObject.tag = (i).ToString();
            //if (GameObject.FindGameObjectWithTag("0"))
            //{
            //    GameObject.FindGameObjectWithTag("0").transform.position = new Vector3(0, -25, -1);
            //    GameObject.FindGameObjectWithTag("0").GetComponent<Transform>().Rotate(90, 0, 0, relativeTo:Space.World);
            //}
        }
    }
}
