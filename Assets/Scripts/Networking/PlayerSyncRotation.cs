﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerSyncRotation : NetworkBehaviour {

    [SyncVar(hook = "OnPlayerRotSynced")]
    private float syncPlayerRot;
    [SyncVar(hook = "OnCamRotSynced")]
    private Quaternion syncCamRot;

    private Transform tform;
    private Transform cam;

    private float lastPlayerRot;
    private Quaternion lastCamRot;
    private float threshold = 1.0f;
    private float lerpRate = 15.0f;

    // Use this for initialization
    void Start() {
        tform = transform;
        cam = tform.Find("Main Camera");
    }

    void FixedUpdate() {
        transmitRotations();
        lerpRotations();
    }

    void lerpRotations() {
        if (!isLocalPlayer) {
            Vector3 playerNewRot = new Vector3(0.0f, syncPlayerRot, 0.0f);
            tform.localRotation = Quaternion.Lerp(tform.localRotation, Quaternion.Euler(playerNewRot), lerpRate * Time.deltaTime);

            cam.localRotation = Quaternion.Lerp(cam.localRotation, syncCamRot, lerpRate * Time.deltaTime);
        }
    }

    [Command]
    void CmdProvideRotationsToServer(float playerRot, Quaternion camRot) {
        syncPlayerRot = playerRot;
        syncCamRot = camRot;
        //Debug.Log("synced rotation");
    }

    [ClientCallback]
    void transmitRotations() {
        if (isLocalPlayer) {
            if (checkIfBeyondThreshold(tform.localEulerAngles.y, lastPlayerRot) ||
                Quaternion.Angle(cam.rotation, lastCamRot) > threshold) {
                lastPlayerRot = tform.localEulerAngles.y;
                lastCamRot = cam.rotation;
                CmdProvideRotationsToServer(lastPlayerRot, lastCamRot);
            }
        }
    }

    bool checkIfBeyondThreshold(float r1, float r2) {
        return Mathf.Abs(Mathf.DeltaAngle(r1, r2)) > threshold;
    }

    [ClientCallback]
    void OnPlayerRotSynced(float latest) {
        syncPlayerRot = latest;
    }

    [ClientCallback]
    void OnCamRotSynced(Quaternion latest) {
        syncCamRot = latest;
    }
}
