using Epic.OnlineServices;
using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FrogNetworkManager : NetworkManager
{
    public struct PlayerLobbyDataMessage : NetworkMessage
    {
        public string PlayerName;
    }


    public override void OnStartServer()
    {
        base.OnStartServer();

        NetworkServer.RegisterHandler<PlayerLobbyDataMessage>(OnCreateCharacter);
    }

    public override void OnClientConnect()
    {
        base.OnClientConnect();

        // you can send the message here, or wherever else you want
    }

    void OnCreateCharacter(NetworkConnectionToClient conn, PlayerLobbyDataMessage message)
    {
        Debug.Log("it reached");
        // playerPrefab is the one assigned in the inspector in Network
        // Manager but you can use different prefabs per race for example
        ServerChangeScene("Game");

        GameObject gameobject = Instantiate(playerPrefab);

        // Apply data from the message however appropriate for your game
        // Typically Player would be a component you write with syncvars or properties
        PlayerLobbyData player = gameobject.GetComponent<PlayerLobbyData>();
        player.PlayerName = message.PlayerName;

        // call this to use this gameobject as the primary controller
        NetworkClient.Ready();
        NetworkServer.AddPlayerForConnection(conn, gameobject);
    }

}
