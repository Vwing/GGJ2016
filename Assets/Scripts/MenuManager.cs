using UnityEngine;
using System;
using UnityEngine.Networking;

public class MenuManager : NetworkDiscovery {

    private Transform cam;

    private GameObject serverCube;
    private GameObject clientCube;
    private ParticleSystem ps;
    private ParticleSystem.EmissionModule serverps;
    private ParticleSystem.EmissionModule clientps;

    // Use this for initialization
    void Awake() {
        cam = Camera.main.transform;
        serverCube = GameObject.Find("ServerCube");
        clientCube = GameObject.Find("ClientCube");

        serverps = serverCube.GetComponent<ParticleSystem>().emission;
        clientps = clientCube.GetComponent<ParticleSystem>().emission;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }

        if (isLookingAtObject(serverCube.name)) {
            spinTransform(serverCube.transform);
            serverps.enabled = true;

            if (Input.GetKeyDown(KeyCode.Space)) {
                //StartAsServer();
                NetworkManager.singleton.StartHost();
            }
        } else {
            serverps.enabled = false;
        }

        if (isLookingAtObject(clientCube.name)) {
            spinTransform(clientCube.transform);
            clientps.enabled = true;

            if (Input.GetKeyDown(KeyCode.Space)) {
                StartAsClient();
            }
        } else {
            clientps.enabled = false;
        }

        //if (isLookingAtServer()) {

        //    serverps.enabled = true;
        //    if (Input.GetKeyDown(KeyCode.Space)) {

        //        if (NetworkServer.active) {

        //        } else {
        //            NetworkManager.singleton.StartHost();
        //            //Start
        //        }
        //    }
        //} else {
        //    serverps.enabled = false;
        //}

    }

    public override void OnReceivedBroadcast(string fromAddress, string data) {
        NetworkManager.singleton.networkAddress = fromAddress;
        NetworkManager.singleton.StartClient();

        //string[] items = data.Split(':');
        //Debug.Log(items.ToString());
        //if (items.Length == 3 && items[0] == "NetworkManager") {
        //  NetworkManager.singleton.networkAddress = items[1];
        //NetworkManager.singleton.networkPort = Convert.ToInt32(items[2]);
        //NetworkManager.singleton.StartClient();
        //}
    }

    private bool isLookingAtObject(string name) {
        RaycastHit hit;
        if (!Physics.Raycast(cam.position, cam.forward, out hit)) {
            return false;
        }
        return hit.collider.name == name;
    }

    private void spinTransform(Transform tform) {
        float t = Time.deltaTime;
        tform.Rotate(new Vector3(t * 80.0f, t * 100.0f, t * 40.0f));
    }
}
