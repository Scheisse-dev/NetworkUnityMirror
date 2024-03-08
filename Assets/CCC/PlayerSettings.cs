using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using Mirror;
using Steamworks;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class PlayerSettings : NetworkBehaviour
{
    [SerializeField] MeshRenderer playerMesh = null;
    [SerializeField] TextMeshProUGUI playerName;
    [SyncVar] public string networkPlayerName = "Player 0";
	[SyncVar] public Color playerColor = Color.blue;

    public List<Color> colors = new();

    private void Awake()
    {
        playerMesh = GetComponentInChildren<MeshRenderer>();        
    }

    public override void OnStartClient()
    {
        if (isServer)
        {
            networkPlayerName = (SteamFriends.GetPersonaName());
            playerColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);
        }
    }


    private void Update()
    {
        UpdateSettings();
    }

    void UpdateSettings()
    {
        playerMesh.material.color = playerColor;
        playerName.text = networkPlayerName;
    }
}
