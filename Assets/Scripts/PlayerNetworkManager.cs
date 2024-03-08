using Org.BouncyCastle.Asn1.X509;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Steamworks;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class PlayerNetworkManager : NetworkBehaviour
{
	[SerializeField]
	GameObject playerPrefab = null;

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
		Debug.Log(Manager.gamePlayers.Count);

		foreach(PlayerObjectController _player in Manager.gamePlayers)
		{
			GameObject _new = Instantiate(playerPrefab);
			NetworkServer.Spawn(_new);
			
		}
	}

}
