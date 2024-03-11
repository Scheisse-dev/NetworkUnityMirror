using Steamworks;
using UnityEngine;
using UnityEngine.UI;

public class ServerListItem : MonoBehaviour
{
	public CSteamID serverKey;
	public Text serverNameText;
	public Button joinButton = null;




	private void Start()
	{
		joinButton.onClick.AddListener(JoinServer);
	}

	public void SetServerValue()
	{
		if (serverNameText != null)
		{
			string _temp = SteamMatchmaking.GetLobbyData(serverKey, "name");
			serverNameText.text = _temp;
			
		}
	}


	void JoinServer()
	{
		SteamMatchmaking.RequestLobbyData(serverKey);
		SteamMatchmaking.JoinLobby(serverKey);
	}
}
