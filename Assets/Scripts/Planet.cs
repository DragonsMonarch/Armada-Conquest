using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    private float rotSpeed=0.001f;
    private HashSet<GameObject> affectedBodies = new HashSet<GameObject>();
    // Update is called once per frame
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Unit"){
            affectedBodies.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Unit"){
            affectedBodies.Remove(other.gameObject);
        }
    }
    private void Update()
    {
        Rotator();
    }
    void Rotator(){
        foreach (GameObject body in affectedBodies){
            body.transform.RotateAround(transform.position, Vector3.up, rotSpeed);
        }
        
    }
}
