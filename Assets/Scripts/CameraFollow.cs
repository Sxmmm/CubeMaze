using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public GameObject target;
    public float xOffset, yOffset, zOffset;

    private void Start() {
        target = GameObject.FindGameObjectWithTag("Player");     
    }

    // Update is called once per frame
    void Update () {
        transform.position = target.transform.position + new Vector3(xOffset, yOffset, zOffset);
        transform.LookAt(target.transform.position);

        if(Input.GetKeyDown("q") && xOffset > 0) {
            xOffset--;
            yOffset--;
        }

        if (Input.GetKeyDown("e") && xOffset < 28) {
            xOffset++;
            yOffset++;
        }
    }
}
