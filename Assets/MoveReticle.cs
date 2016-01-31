using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MoveReticle : NetworkBehaviour {
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
            if (Input.GetKeyDown(KeyCode.Space) && hit.collider.tag == "Zombie")
                Shoot(hit.collider);
        }
	}
    void Shoot(Collider shot)
    {
        Zombie_ID zComp = shot.transform.GetComponent<Zombie_ID>();
        CmdZombieShot(zComp.id);
    }
    //void Shoot()
    //{
    //    Collider[] zombs = Physics.OverlapSphere(transform.position, 1f);
    //    foreach (Collider z in zombs)
    //    {
    //        Zombie_ID zComp = GetComponent<Zombie_ID>();
    //        if (zComp)
    //        {
    //            CmdZombieShot(zComp.id);
    //        }
    //    }
    //}

    [Command]
    void CmdZombieShot(string uniqueID)
    {
        GameObject go = GameObject.Find(uniqueID);
        if (go)
            Destroy(go);
    }
}
