using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;
using Steamworks;

public class CustomNetworkManager : NetworkManager
{
	[SerializeField] PlayerObjectController GamePlayerPrefab;
	[SerializeField] PlayerObjectController CCCPlayerPrefab;
	public List<PlayerObjectController> gamePlayers { get; } = new List<PlayerObjectController>();

	public override void OnServerAddPlayer(NetworkConnectionToClient conn)
	{
		if (SceneManager.GetActiveScene().name == "Lobby")
		{
			PlayerObjectController _gamePlayerInstance = Instantiate(GamePlayerPrefab);
			_gamePlayerInstance.connectionID = conn.connectionId;
			_gamePlayerInstance.playerIdNumber = gamePlayers.Count + 1;
			_gamePlayerInstance.playerSteamID = (ulong)SteamMatchmaking.GetLobbyMemberByIndex((CSteamID)SteamLobby.Instance.currentLobbyID, gamePlayers.Count);

			NetworkServer.AddPlayerForConnection(conn, _gamePlayerInstance.gameObject);

		}

		if (SceneManager.GetActiveScene().name == "GameScene")
		{
			PlayerObjectController _gamePlayerInstance = Instantiate(GamePlayerPrefab);
			_gamePlayerInstance.playerIdNumber = gamePlayers.Count + 1;
			_gamePlayerInstance.playerSteamID = (ulong)SteamMatchmaking.GetLobbyMemberByIndex((CSteamID)SteamLobby.Instance.currentLobbyID, gamePlayers.Count);

			NetworkServer.AddPlayerForConnection(conn, _gamePlayerInstance.gameObject);
		}
	}


	public override void ServerChangeScene(string newSceneName)
	{

		for (int i = gamePlayers.Count - 1; i >= 0; i--) 
		{
			NetworkConnectionToClient conn = gamePlayers[i].connectionToClient;
			PlayerObjectController gameplayerInstance = Instantiate(GamePlayerPrefab);
			

				//NetworkServer.Destroy(conn.identity.gameObject);

			NetworkServer.ReplacePlayerForConnection(conn, gameplayerInstance.gameObject);
			
		}
		base.ServerChangeScene(newSceneName);
		


	}

	public override void OnServerSceneChanged(string sceneName)
	{
	

	}



}
