using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using Vuforia;

public class GodController : NetworkBehaviour {

    private Transform cam;
    private float mouseSensitivy = 8.0f;
    private float mouseLerpSpeed = 20.0f;

    // Use this for initialization
    void Start() {
        cam = transform.Find("Main Camera");

        if (!isLocalPlayer) {
            Destroy(cam.GetComponent<Camera>());
            Destroy(cam.GetComponent<AudioListener>());
            Destroy(this);
        } else {
            GameObject go = GameObject.Find("ImageTargetStones");
            Transform fs = GameObject.Find("FollowerScene").transform;
            fs.parent = go.transform;
            Transform scale = GameObject.Find("FollowerScale").transform;
            fs.localScale = scale.localScale;
            fs.localPosition = scale.position;

            Destroy(transform.Find("Model").gameObject);
        }
    }

    float curVertLook;
    float curHorizLook;

    // Update is called once per frame
    void Update() {
        OldMethod();

        Shoot();
    }
    void OldMethod()
    {
        //float targetHoriz = Input.GetAxisRaw("Mouse X") * mouseSensitivy;
        //curVertLook -= Input.GetAxisRaw("Mouse Y") * mouseSensitivy;
        //curVertLook = Mathf.Clamp(curVertLook, -90.0f, 90.0f);

        //Quaternion targetVert = Quaternion.Euler(curVertLook, 0.0f, 0.0f);
        //cam.localRotation = Quaternion.Lerp(cam.localRotation, targetVert, mouseLerpSpeed * Time.deltaTime);

        //curHorizLook = Mathf.Lerp(curHorizLook, targetHoriz, mouseLerpSpeed * Time.deltaTime);

        //transform.Rotate(0.0f, curHorizLook, 0.0f);

        //if (Input.GetKeyDown(KeyCode.Escape)) {
        //    // should figure out how to just disconnect instead
        //    Application.Quit();
        //}
    }
    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit))
        {
            if (Input.GetKeyDown(KeyCode.Space) && hit.collider.tag == "Zombie")
            {
                Zombie_ID zComp = hit.transform.GetComponent<Zombie_ID>();
                CmdZombieShot(zComp.id);
            }
        }
    }
    [Command]
    void CmdZombieShot(string uniqueID)
    {
        GameObject go = GameObject.Find(uniqueID);
        if (go)
            Destroy(go);
    }
}
