using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	[SerializeField] Button lobbyListButton;
	[SerializeField] Button backButton;
	[SerializeField] Canvas mainMenuCanvas;
	[SerializeField] Canvas lobbyListCanvas;
	[SerializeField] ServerListScript List;

	private void Awake()
	{
		lobbyListButton.onClick.AddListener(SwitchCanvas);
		lobbyListButton.onClick.AddListener(Refresh);
		backButton.onClick.AddListener(SwitchCanvas);
		lobbyListCanvas.enabled = !mainMenuCanvas.enabled;
	}


	private void SwitchCanvas()
	{
		
		mainMenuCanvas.enabled = !mainMenuCanvas.enabled;
		lobbyListCanvas.enabled = !mainMenuCanvas.enabled;
	}

	private void Refresh()
	{
		List.RefreshList();
	}
}
