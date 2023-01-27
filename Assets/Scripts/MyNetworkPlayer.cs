using Mirror;
using UnityEngine;

public class MyNetworkPlayer : NetworkBehaviour
{
    [SyncVar] [SerializeField] private string displayName = "Missing Name";
    [SyncVar] [SerializeField] private Color displayColour = Color.black;

    [Server]
    public void SetDisplayName(string newDisplayName)
    {
        displayName = newDisplayName;
    }
    
    [Server]
    public void SetDisplayColor(Color newDisplayColor)
    {
        displayColour = newDisplayColor;
    }
}