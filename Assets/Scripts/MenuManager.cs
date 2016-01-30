using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MenuManager : MonoBehaviour {

    private Transform cam;

    private GameObject startCube;
    private ParticleSystem ps;
    private ParticleSystem.EmissionModule psem;

    // Use this for initialization
    void Awake() {
        cam = Camera.main.transform;
        startCube = GameObject.Find("Cube");
        psem = startCube.GetComponent<ParticleSystem>().emission;
        psem.enabled = false;

    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }

        if (Application.platform == RuntimePlatform.Android) {
            androidUpdate();
        } else {
            computerUpdate();
        }
    }

    private void androidUpdate() {
        if (NetworkServer.active) {
            startCube.SetActive(true);
            if (isLookingAtStart()) {
                spinCube();
                psem.enabled = true;
                if (Input.GetMouseButtonDown(0)) {
                    NetworkManager.singleton.StartClient();
                }
            } else {
                psem.enabled = false;
            }
        } else {
            startCube.SetActive(false);
        }
    }

    private void computerUpdate() {
        if (isLookingAtStart()) {
            spinCube();
            psem.enabled = true;
            if (Input.GetKeyDown(KeyCode.Space)) {
                NetworkManager.singleton.StartHost();
            }
        } else {
            psem.enabled = false;
        }

    }

    private bool isLookingAtStart() {
        return Physics.Raycast(cam.position, cam.forward);
    }

    private void spinCube() {
        float t = Time.deltaTime;
        startCube.transform.Rotate(new Vector3(t * 80.0f, t * 100.0f, t * 40.0f));
    }
}
