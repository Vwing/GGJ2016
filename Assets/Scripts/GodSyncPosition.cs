﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class GodSyncPosition : NetworkBehaviour {

    [SyncVar]
    private Vector3 syncPos;

    private Transform tform;
    private float lerpRate = 15.0f;

    private Vector3 lastPos;
    private float threshold = 0.25f;

    private GameObject imageTarget;
    private GameObject trackableParent;

    // Use this for initialization
    void Start() {
        tform = transform;
        imageTarget = GameObject.Find("ImageTargetStones");
        trackableParent = GameObject.Find("TrackableParent");
    }

    void FixedUpdate() {
        transmitPosition();
        lerpPosition();
    }

    [Command]
    void CmdProvidePositionToServer(Vector3 pos) {
        syncPos = pos;
        //Debug.Log("synced position");
    }
    private float mag = 310f;
    // tell server your position if you have moved past threshold
    [ClientCallback]
    void transmitPosition() {
        if (isLocalPlayer) {
            Vector3 dir = trackableParent.transform.position - imageTarget.transform.position;
            Vector3 newPos = imageTarget.transform.InverseTransformDirection(dir) * mag;
            newPos.y -= 10;

            if ((newPos - lastPos).sqrMagnitude > threshold * threshold) {
                CmdProvidePositionToServer(newPos);
                lastPos = newPos;
            }
        }
    }

    // update position if this isnt player
    void lerpPosition() {
        if (!isLocalPlayer) {
            tform.localPosition = Vector3.Lerp(tform.localPosition, syncPos, Time.deltaTime * lerpRate);
        }
    }
}
