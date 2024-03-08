using Steamworks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ServerListScript : MonoBehaviour
{

	protected Callback<LobbyMatchList_t> lobbyList;

	

	private void OnEnable()
	{

		//lobbyList = Callback<LobbyMatchList_t>.Create(OnLobbyFounds);
		//SteamMatchmaking.RequestLobbyList();

	}


	void OnLobbyFounds(LobbyMatchList_t lobbyList)
	{

		Debug.Log(lobbyList.ToString());
		
	}
}
