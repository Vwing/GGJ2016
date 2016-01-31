using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ZombieSpawner : NetworkBehaviour {

    public GameObject zombiePrefab;

    private float spawnRadius;
    private int numZombies = 0;
    private int maxZombies = 10;


    void spawnZombie() {
        Vector2 circ = Random.insideUnitCircle.normalized;
        Vector3 spawn = new Vector3(circ.x, 0.0f, circ.y);
        GameObject go = (GameObject)Instantiate(zombiePrefab, spawn, Quaternion.identity);
    }


    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}
