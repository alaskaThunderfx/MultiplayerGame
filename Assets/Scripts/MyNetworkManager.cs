using Mirror;
using UnityEngine;

public class MyNetworkManager: NetworkManager
{
    public override void OnClientConnect()
    {
        base.OnClientConnect();
        
        Debug.Log("I connected to a server");
    }

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);

        Debug.Log($"Another player joined! There are {numPlayers} players right now!" );
    }
}
