using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class GameBroadcaster : NetworkDiscovery {

	// Use this for initialization
	void Start () {
        Initialize();
        StartAsServer();
	}
	
	// Update is called once per frame
	void Update () {
	    if(NetworkManager.singleton.numPlayers > 1) {
            //StopBroadcast();
            
        }
	}
}
