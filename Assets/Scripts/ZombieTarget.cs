using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ZombieTarget : NetworkBehaviour {

    private NavMeshAgent agent;
    private Transform tform;
    private Transform target;   // player


	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        tform = transform;
        target = GameObject.Find("Player(Clone)").transform;
        if (GameObject.Find("GodType")) {
            Destroy(agent);
            transform.parent = GameObject.Find("FollowerScene").transform;
            transform.localScale = Vector3.one;
            transform.localRotation = Quaternion.identity;
        }
	}
	
    void FixedUpdate() {
        moveToTarget();
    }

    //void searchForTarget() {
    //    if (!isServer) {
    //        return;
    //    }
    //}

	private void moveToTarget() {
        if (target && isServer) {
            agent.SetDestination(target.position);
        }
    }

}
