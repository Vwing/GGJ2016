using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class GodSyncPosition : NetworkBehaviour {

    [SyncVar]
    private Vector3 syncPos;

    private Transform tform;
    private float lerpRate = 15.0f;

    private Vector3 lastPos;
    private float threshold = 0.25f;

    private Transform followerScene;
    private Transform userHead;

    // Use this for initialization
    void Start() {
        tform = transform;
        followerScene = GameObject.Find("FollowerScene").transform;
        userHead = GameObject.Find("UserHead").transform;
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

    // tell server your position if you have moved past threshold
    [ClientCallback]
    void transmitPosition() {
        Vector3 newPos = (userHead.position - followerScene.position) * 100.0f;
        if (isLocalPlayer && (newPos - lastPos).sqrMagnitude > threshold * threshold) {
            //CmdProvidePositionToServer(tform.position);
            CmdProvidePositionToServer(newPos);
            lastPos = newPos;
        }
    }

    // update position if this isnt player
    void lerpPosition() {
        if (!isLocalPlayer) {
            tform.localPosition = Vector3.Lerp(tform.localPosition, syncPos, Time.deltaTime * lerpRate);
        }
    }
}
