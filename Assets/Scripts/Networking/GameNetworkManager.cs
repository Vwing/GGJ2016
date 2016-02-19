using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class GameNetworkManager : NetworkManager {

    // Use this for initialization
    void Start() {

    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId) {
        if (playerControllerId < conn.playerControllers.Count && conn.playerControllers[playerControllerId].IsValid && conn.playerControllers[playerControllerId].gameObject != null) {
            if (LogFilter.logError) { Debug.LogError("There is already a player at that playerControllerId for this connections."); }
            return;
        }

        // if on android then god prefab
        GameObject playerGO;
        if (numPlayers == 0) {
            playerGO = (GameObject)Instantiate(spawnPrefabs[0], Vector3.zero, Quaternion.identity);
        } else {
            playerGO = (GameObject)Instantiate(spawnPrefabs[1], new Vector3(0.0f, 2.0f, 0.0f), Quaternion.identity);
        }
        NetworkServer.AddPlayerForConnection(conn, playerGO, playerControllerId);
    }
}
