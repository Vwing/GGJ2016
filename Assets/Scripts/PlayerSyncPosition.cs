using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerSyncPosition: NetworkBehaviour {

    [SyncVar]
    private Vector3 syncPos;

    private Transform tform;
    private float lerpRate = 15.0f;

    private Vector3 lastPos;
    private float threshold = 0.25f;

    // Use this for initialization
    void Start() {
        tform = transform;
    }

    void FixedUpdate() {
        transmitPosition();
        lerpPosition();
    }

    void OnCollisionEnter() {
        transmitPosition();
        lerpPosition();
    }

    [Command]
    void CmdProvidePositionToServer(Vector3 pos) {
        syncPos = pos;
        //Debug.Log("synced position");
    }

    // tell server your position if you have moved past threshold
    [ClientCallback]
    void transmitPosition() {
        if (isLocalPlayer && (tform.position - lastPos).sqrMagnitude > threshold * threshold) {
            CmdProvidePositionToServer(tform.position);
            lastPos = tform.position;
        }
    }

    // update position if this isnt player
    void lerpPosition() {
        if (!isLocalPlayer) {
            tform.position = Vector3.Lerp(tform.position, syncPos, Time.deltaTime * lerpRate);
        }
    }
}
