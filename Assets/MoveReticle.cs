using UnityEngine;
using System.Collections;

public class MoveReticle : MonoBehaviour {
    Transform cam;
	// Use this for initialization
	void Start () {
        cam = Camera.main.transform;
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
	    if(Physics.Raycast(cam.position,cam.forward,out hit))
        {
            transform.position = hit.point;
        }
	}
}
