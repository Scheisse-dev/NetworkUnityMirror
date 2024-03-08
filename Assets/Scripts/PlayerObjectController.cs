using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Steamworks;
using UnityEngine.SceneManagement;

public class PlayerObjectController : NetworkBehaviour
{
	[SyncVar] public int connectionID;
	[SyncVar] public int playerIdNumber;
	[SyncVar] public ulong playerSteamID;
	[SyncVar(hook = nameof(PlayerNameUpdate))] public string playerName;

	private CustomNetworkManager manager;
	private CustomNetworkManager Manager
	{
		get
		{
			if (manager != null)
				return manager;
			return manager = CustomNetworkManager.singleton as CustomNetworkManager;
		}
	}

	private void Start()
	{
		if (!NetworkClient.ready)
			NetworkClient.Ready();

	}


	public override void OnStartAuthority()
	{
		CmdSetPlayerName(SteamFriends.GetPersonaName().ToString());
		gameObject.name = "localGamePlayer";
		LobbyController.Instance.FindLocalPlayer();
		LobbyController.Instance.UpdateLobbyName();
	}

	public override void OnStartClient()
	{
		if (!NetworkClient.ready)
			NetworkClient.Ready();
		Manager.gamePlayers.Add(this);

		if (SceneManager.GetActiveScene().name == "Lobby")
		{
			LobbyController.Instance.UpdateLobbyName();
			LobbyController.Instance.UpdatePlayerList();
		}
		
	}

	public override void OnStopClient()
	{
		Manager.gamePlayers.Remove(this);
		LobbyController.Instance.UpdatePlayerList();
	}

	[Command]
	private void CmdSetPlayerName(string _playerName)
	{
		this.PlayerNameUpdate(this.playerName, _playerName);
	}

	public void PlayerNameUpdate(string _oldValue, string _newValue)
	{
		if (isServer)
			this.playerName = _newValue;
		if (isClient)
			LobbyController.Instance.UpdatePlayerList();
	}


	public void Ready()
	{
		NetworkClient.Ready();
	}

}
