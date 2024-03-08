using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using Mirror;

public class GameSceneManager : NetworkBehaviour
{
	private string m_SceneName = "GameScene";

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

	public void Travel()
	{
		if(!isServer) { return; }
		//CustomNetworkManager.singleton.playerPrefab = playerPrefab;
		Manager.ServerChangeScene(m_SceneName);
	}

}
