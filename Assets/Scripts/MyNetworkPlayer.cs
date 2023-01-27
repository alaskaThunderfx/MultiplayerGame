using System;
using System.Linq;
using Mirror;
using TMPro;
using UnityEngine;

public class MyNetworkPlayer : NetworkBehaviour
{
    [SyncVar(hook = nameof(HandleDisplayNameUpdated))] [SerializeField] private string displayName = "Missing Name";
    [SyncVar(hook = nameof(HandleDisplayColorUpdated))] [SerializeField] private Color displayColour = Color.black;

    [SerializeField] private TMP_Text displayNameText = null;
    [SerializeField] private Renderer displayColourRenderer = null;

    private string[] _specialChars = { "*", "'", "!", "@", "#", "$", "%", "^", "&", "(", ")" };
    private string[] _blacklistedWord = { "Jesus", "Friend", "Sora", "Donald", "Goofy" };
    
    #region Server
    
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

    [Command]
    private void CmdSetDisplayName(string newDisplayName)
    {
        if (newDisplayName.Length is < 3 or > 15) return;
        if (_specialChars.Any(newDisplayName.Contains)) return;
        if (_blacklistedWord.Any(newDisplayName.Contains)) return;
        if (newDisplayName.Any(char.IsWhiteSpace)) return;

        RpcLogNewName(newDisplayName);
        
        SetDisplayName(newDisplayName);
    }

    #endregion

    #region Client
    private void HandleDisplayColorUpdated(Color oldColor, Color newColor)
    {
        displayColourRenderer.material.SetColor("_BaseColor", newColor);
    }

    private void HandleDisplayNameUpdated(string oldName, string newName)
    {
        displayNameText.text = newName;
    }

    [ContextMenu("Set My Name")]
    private void SetMyName()
    {
        CmdSetDisplayName("My New Name");
    }

    [ClientRpc]
    private void RpcLogNewName(string newDisplayName)
    {
        Debug.Log(newDisplayName);
    }
    
    #endregion
}