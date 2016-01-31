using UnityEngine;
using System.Collections;

public class WorldPos : MonoBehaviour {
    public Vector3 position;
    public Vector3 rotation;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        position = transform.position;
        rotation = transform.rotation.eulerAngles;
	}
}
