using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Zombie_ID : NetworkBehaviour {

    [SyncVar]
    public string id;

    private Transform tform;

	// Use this for initialization
	void Start () {
        tform = transform;
	}
	
	// Update is called once per frame
	void Update () {
        setIdentity();
	}

    void setIdentity() {
        if(tform.name == "" || tform.name == "Zombie(Clone)") {
            tform.name = id;
        }
    }
}
