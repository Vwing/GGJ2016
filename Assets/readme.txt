NetworkDiscovery Sample.


MyNetManager has a NetworkDiscovery slot called "discovery".

When MyNetManager starts as a host, it automatically starts broadcasting on the local network, and disables the default NetworkDiscovery GUI.

Clients can use the builtin GUI of the NetworkBroadcast component to listen for broadcast messages from a host. When a host is chosen, the NetworkManager is used to automatically join the game, and the NetworkDiscovery GUI is disabled.