using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class GodSyncRotation : NetworkBehaviour {

    [SyncVar(hook = "OnCamRotSynced")]
    private Quaternion syncCamRot;

    private Transform tform;
    private Transform cam;

    private Quaternion lastCamRot;
    private float threshold = 1.0f;
    private float lerpRate = 15.0f;

    // Use this for initialization
    void Start() {
        tform = transform;
        GameObject go = GameObject.Find("ARCamera");
        if (go) {
            cam = go.transform;
        }
    }

    void FixedUpdate() {
        transmitRotations();
        lerpRotations();
    }

    void lerpRotations() {
        if (!isLocalPlayer) {
            tform.localRotation = Quaternion.Lerp(tform.localRotation, syncCamRot, lerpRate * Time.deltaTime);
        }
    }

    [Command]
    void CmdProvideRotationsToServer(Quaternion camRot) {
        syncCamRot = camRot;
        //Debug.Log("synced rotation");
    }

    [ClientCallback]
    void transmitRotations() {
        if (isLocalPlayer) {
            if (Quaternion.Angle(cam.rotation, lastCamRot) > threshold) {
                lastCamRot = cam.rotation;
                CmdProvideRotationsToServer(lastCamRot);
            }
        }
    }

    [ClientCallback]
    void OnCamRotSynced(Quaternion latest) {
        syncCamRot = latest;
    }
}
