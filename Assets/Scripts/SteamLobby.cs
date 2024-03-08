using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Steamworks;
using UnityEngine.UI;

public class SteamLobby : MonoBehaviour
{

	public static SteamLobby Instance;

	uint qsize = 10;
	Queue myLogQueue = new Queue();

	protected Callback<LobbyCreated_t> lobbyCreated;
	protected Callback<GameLobbyJoinRequested_t> joinRequest;
	protected Callback<LobbyEnter_t> lobbyEnter;

	public ulong currentLobbyID;
	private const string HostAddressKey = "HostAddress";
	private CustomNetworkManager manager;




	private void Start()
	{
		if (!SteamManager.Initialized) { return; }
		if (Instance == null) { Instance = this; }

		manager = GetComponent<CustomNetworkManager>();

		lobbyCreated = Callback<LobbyCreated_t>.Create(OnLobbyCreated);
		joinRequest = Callback<GameLobbyJoinRequested_t>.Create(OnJoinRequest);
		lobbyEnter = Callback<LobbyEnter_t>.Create(OnLobbyEntered);
	}


	public void HostLobby()
	{
		SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypePublic, manager.maxConnections);
	}
	private void OnLobbyCreated(LobbyCreated_t callback)
	{
		if(callback.m_eResult != EResult.k_EResultOK) {return; }
		Debug.Log("Lobby created Successfully");

		manager.StartHost();

		SteamMatchmaking.SetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), HostAddressKey, SteamUser.GetSteamID().ToString());
		SteamMatchmaking.SetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), "name", SteamFriends.GetPersonaName().ToString());

	}

	private void OnJoinRequest(GameLobbyJoinRequested_t callback)
	{
		Debug.Log("Request to join lobby");
		SteamMatchmaking.JoinLobby(callback.m_steamIDLobby);

	}

	private void OnLobbyEntered(LobbyEnter_t callback)
	{



		currentLobbyID = callback.m_ulSteamIDLobby;



		if (NetworkServer.active) { return; }

		manager.networkAddress = SteamMatchmaking.GetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), HostAddressKey);
		manager.StartClient();

	}



	void OnEnable()
	{
		Application.logMessageReceived += HandleLog;
	}

	void OnDisable()
	{
		Application.logMessageReceived -= HandleLog;
	}

	void HandleLog(string logString, string stackTrace, LogType type)
	{
		myLogQueue.Enqueue("[" + type + "] : " + logString);
		if (type == LogType.Exception)
			myLogQueue.Enqueue(stackTrace);
		while (myLogQueue.Count > qsize)
			myLogQueue.Dequeue();
	}

	void OnGUI()
	{
		GUILayout.BeginArea(new Rect(Screen.width - 400, 0, 400, Screen.height));
		GUILayout.Label("\n" + string.Join("\n", myLogQueue.ToArray()));
		GUILayout.EndArea();
	}
}
