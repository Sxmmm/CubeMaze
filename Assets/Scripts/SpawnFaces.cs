using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFaces : MonoBehaviour {

    public GameObject face;

    void Start()
    {
        for (int i = 0; i < 6; i++)
        {
            GameObject go = Instantiate(face, new Vector3(0, (5*(i+2)), 0), Quaternion.identity);
            go.gameObject.tag = (i).ToString();
        }
    }
}
