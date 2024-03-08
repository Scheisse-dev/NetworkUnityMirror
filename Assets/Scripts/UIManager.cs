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


	private void Awake()
	{
		lobbyListButton.onClick.AddListener(SwitchCanvas);
		backButton.onClick.AddListener(SwitchCanvas);
		lobbyListCanvas.enabled = !mainMenuCanvas.enabled;
	}


	private void SwitchCanvas()
	{
		
		mainMenuCanvas.enabled = !mainMenuCanvas.enabled;
		lobbyListCanvas.enabled = !mainMenuCanvas.enabled;
	}
}
