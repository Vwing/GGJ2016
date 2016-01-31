using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ZombieSpawner : NetworkBehaviour {

    public GameObject zombiePrefab;

    public float spawnRadius;
    private int numZombies = 0;
    private int maxZombies = 10;

    // Use this for initialization
    void Start() {

    }

    float spawnTime = 1.0f;
    // Update is called once per frame
    void Update() {
        if (!isServer) {
            return;
        }
        spawnTime -= Time.deltaTime;
        if (spawnTime < 0.0f && numZombies++ < maxZombies) {
            spawnZombie();
            spawnTime += 1.0f;
        }
    }

    void spawnZombie() {
        Vector2 circ = Random.insideUnitCircle.normalized * spawnRadius;
        Vector3 spawn = new Vector3(circ.x, -1.0f, circ.y) + transform.position;
        GameObject go = (GameObject)Instantiate(zombiePrefab, spawn, Quaternion.identity);
        NetworkServer.Spawn(go);
    }
}
