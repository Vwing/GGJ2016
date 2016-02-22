using UnityEngine;
using System;
using UnityEngine.Networking;

public class MenuManager : MonoBehaviour 
{

    private Transform cam;

    private GameObject serverCube;
    private GameObject clientCube;
    private ParticleSystem ps;
    private ParticleSystem.EmissionModule serverps;
    private ParticleSystem.EmissionModule clientps;
    private IPKeyPad keyPad;
    public string ipAddress;

    void Awake()
    {
        ipAddress = Application.isEditor ? "192.168.0.103" : "192.168.0.105";
        cam = Camera.main.transform;
        serverCube = GameObject.Find("ServerCube");
        clientCube = GameObject.Find("ClientCube");

        keyPad = UnityEngine.Object.FindObjectOfType<IPKeyPad>();
        serverps = serverCube.GetComponent<ParticleSystem>().emission;
        clientps = clientCube.GetComponent<ParticleSystem>().emission;

    }

    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        if (isLookingAtObject(serverCube.name)) 
        {
            spinTransform(serverCube.transform);
            serverps.enabled = true;

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                NetworkManager.singleton.StartHost();
        } 
        else
            serverps.enabled = false;

        if (isLookingAtObject(clientCube.name)) 
        {
            spinTransform(clientCube.transform);
            clientps.enabled = true;

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                NetworkManager.singleton.networkAddress = keyPad.theirIP;//"192.168.0.103";
                NetworkManager.singleton.networkPort = 7777;
                NetworkManager.singleton.StartClient();
            }
        } 
        else
            clientps.enabled = false;
    }

    private bool isLookingAtObject(string name)
    {
        RaycastHit hit;
        if (!Physics.Raycast(cam.position, cam.forward, out hit))
            return false;
        return hit.collider.name == name;
    }

    private void spinTransform(Transform tform) 
    {
        float t = Time.deltaTime;
        tform.Rotate(new Vector3(t * 80.0f, t * 100.0f, t * 40.0f));
    }
}
