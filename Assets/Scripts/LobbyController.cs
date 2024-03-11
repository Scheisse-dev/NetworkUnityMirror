using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using Steamworks;
using System.Linq;
using UnityEngine.SceneManagement;

public class LobbyController : MonoBehaviour
{
	public static LobbyController Instance;

	public Text LobbyNameText;

	public GameObject playerListViewContent;
	public GameObject playerListItemPrefab;
	public GameObject localPlayerObject;



	public ulong currentLobbyId;
	public bool playerItemCreated = false;
	private List<PlayerListItem> playerListItems = new List<PlayerListItem>();
	public PlayerObjectController localPlayerController;


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

	private void Awake()
	{
		if(Instance == null) { Instance = this; }

	}

	public void UpdateLobbyName()
	{
		if (SceneManager.GetActiveScene().name != "Lobby") return;
		currentLobbyId = Manager.GetComponent<SteamLobby>().currentLobbyID;
		if(LobbyNameText != null)
			LobbyNameText.text = SteamMatchmaking.GetLobbyData(new CSteamID(currentLobbyId), "name");
	}

	public void UpdatePlayerList()
	{
		if (SceneManager.GetActiveScene().name != "Lobby") return;

		if (!playerItemCreated) { CreateHostPlayerItem(); }

		if (playerListItems == null) return;

		if (playerListItems.Count < Manager.gamePlayers.Count) CreateClientPlayerItem(); 
		if (playerListItems.Count > Manager.gamePlayers.Count) RemovePlayerItem(); 

		if (playerListItems.Count == Manager.gamePlayers.Count) UpdatePlayerItem();
	}


	public void FindLocalPlayer()
	{
		localPlayerObject = GameObject.Find("localGamePlayer");
		localPlayerController = localPlayerObject.GetComponent<PlayerObjectController>();
	}

	public void CreateHostPlayerItem()
	{
		foreach (PlayerObjectController player in Manager.gamePlayers)
		{
			if (player == null) continue;
			GameObject _newPlayerItem = Instantiate(playerListItemPrefab) as GameObject;
			PlayerListItem _newPlayerItemScript = _newPlayerItem.GetComponent<PlayerListItem>();

			if (_newPlayerItemScript)
			{
				_newPlayerItemScript.playerName = player.playerName;
				_newPlayerItemScript.connectionID = player.connectionID;
				_newPlayerItemScript.playerSteamID = player.playerSteamID;

				_newPlayerItemScript.SetPlayerValue();

				_newPlayerItemScript.transform.SetParent(playerListViewContent.transform);
				_newPlayerItemScript.transform.localScale = Vector3.one;
			}


			if(playerListItems != null)
				playerListItems.Add(_newPlayerItemScript);


		}
		playerItemCreated = true;
	}

	public void CreateClientPlayerItem()
	{
		if (SceneManager.GetActiveScene().name != "Lobby") return;

		foreach (PlayerObjectController player in Manager.gamePlayers)
		{
			if (player == null) continue;
			if (!playerListItems.Any(b => b.connectionID == player.connectionID))
			{
				GameObject _newPlayerItem = Instantiate(playerListItemPrefab) as GameObject;
				PlayerListItem _newPlayerItemScript = _newPlayerItem.GetComponent<PlayerListItem>();
				_newPlayerItemScript.playerName = player.playerName;
				_newPlayerItemScript.connectionID = player.connectionID;
				_newPlayerItemScript.playerSteamID = player.playerSteamID;

				_newPlayerItemScript.SetPlayerValue();

				if (playerListViewContent == null) continue;
				_newPlayerItemScript.transform.SetParent(playerListViewContent.transform);
				_newPlayerItemScript.transform.localScale = Vector3.one;


				playerListItems.Add(_newPlayerItemScript);
			}
		}
	}
	public void UpdatePlayerItem()
	{
		if (SceneManager.GetActiveScene().name != "Lobby") return;

		foreach (PlayerObjectController player in Manager.gamePlayers)
		{
			foreach (PlayerListItem playerListItemScript in playerListItems)
			{
				if (playerListItemScript == null) continue;
				if (playerListItemScript.connectionID == player.connectionID)
				{
					playerListItemScript.playerName = player.playerName;
					playerListItemScript.SetPlayerValue();
				}
			}
		}
	}
	public void RemovePlayerItem()
	{
		if (SceneManager.GetActiveScene().name != "Lobby") return;

		List<PlayerListItem> playerListItemToRemove = new List<PlayerListItem> ();

		foreach (PlayerListItem playerListItem in playerListItems)
		{
			if(playerListItem == null) continue;
			if (!Manager.gamePlayers.Any(b => b.connectionID == playerListItem.connectionID))
				playerListItemToRemove.Add(playerListItem);

		}


		if (playerListItemToRemove.Count > 0)
			foreach (PlayerListItem playerListItemTR in playerListItemToRemove)
			{
				if (playerListItemTR == null) continue;
				GameObject objectToRemove = playerListItemTR.gameObject;
				playerListItems.Remove(playerListItemTR);
				Destroy(objectToRemove);
				objectToRemove = null;
			}

	}
}
