using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkManagerOverride : NetworkManager
{
    [SerializeField] private Transform playerStartPos;
    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        Transform startPos = playerStartPos;
        GameObject player = startPos != null
            ? Instantiate(playerPrefab, startPos.position, startPos.rotation)
            : Instantiate(playerPrefab);

        // instantiating a "Player" prefab gives it the name "Player(clone)"
        // => appending the connectionId is WAY more useful for debugging!
        player.name = $"{playerPrefab.name} [connId={conn.connectionId}]";
        NetworkServer.AddPlayerForConnection(conn, player);
    }

    private void OnConnectedToServer()
    {
        
    }
}
