using Steamworks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ServerListScript : MonoBehaviour
{

	protected Callback<LobbyMatchList_t> lobbyList;


	[SerializeField]
	GameObject serverListItemPrefab = null;
	[SerializeField]
	GameObject serverListContent = null;


	List<ServerListItem> serverListItems = new List<ServerListItem>();
	bool serverItemCreate;

	public void RefreshList()
	{


		if (!SteamManager.Initialized) { return; }
		lobbyList = Callback<LobbyMatchList_t>.Create(OnLobbyFounds);
		SteamMatchmaking.RequestLobbyList();

	}


	void OnLobbyFounds(LobbyMatchList_t lobbyList)
	{

		if (!serverListItemPrefab || serverListContent == null) return;
		RemoveAllServer();


		for (int i = 0; i < lobbyList.m_nLobbiesMatching; i++)
		{
			CSteamID _steamId = SteamMatchmaking.GetLobbyByIndex(i);
			if (!_steamId.IsValid() || SteamMatchmaking.GetLobbyData(_steamId, "name") == "") continue;



			GameObject _obj = Instantiate(serverListItemPrefab) as GameObject;
			ServerListItem _newServerItem = _obj.GetComponent<ServerListItem>();

			if (_newServerItem)
			{
				_newServerItem.serverKey = _steamId;
				_newServerItem.SetServerValue();
				_newServerItem.transform.SetParent(serverListContent.transform);
			}

			serverListItems.Add(_newServerItem);
		}
	}

	void RemoveAllServer()
	{
		List<ServerListItem> _serverListItemsToRemove = new List<ServerListItem> ();

		foreach (ServerListItem _serverListItem in serverListItems)
		{
			if(_serverListItem == null) continue;
			_serverListItemsToRemove.Add(_serverListItem);

		}


		if (_serverListItemsToRemove.Count > 0)
			foreach (ServerListItem serverListItemTR in _serverListItemsToRemove)
			{
				if (serverListItemTR == null) continue;
				GameObject objectToRemove = serverListItemTR.gameObject;
				serverListItems.Remove(serverListItemTR);
				Destroy(objectToRemove);
			}
	}
}
