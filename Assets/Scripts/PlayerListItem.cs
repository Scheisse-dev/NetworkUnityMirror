using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Steamworks;


public class PlayerListItem : MonoBehaviour
{
	public string playerName;
	public int connectionID;
	public ulong playerSteamID;


	public Text playerNameText;
	private bool avatarReceived;
	public RawImage playerIcon;





	protected Callback<AvatarImageLoaded_t> imageLoaded;


	private void Start()
	{
		imageLoaded = Callback<AvatarImageLoaded_t>.Create(OnImageLoaded);
	}

	public void SetPlayerValue()
	{
		playerNameText.text = playerName;
		if (!avatarReceived) GetPlayerIcon();
	}

	void GetPlayerIcon()
	{
		int _imageID = SteamFriends.GetLargeFriendAvatar((CSteamID)playerSteamID);
		if (_imageID == -1) return;
		playerIcon.texture = GetSteamImageAsTexture(_imageID);
	}

	private Texture2D GetSteamImageAsTexture(int iImage)
	{
		Texture2D texture = null;

		bool isValid = SteamUtils.GetImageSize(iImage, out uint width, out uint height);

        if (isValid)
        {
			byte[] image = new byte[width * height * 4];
			isValid = SteamUtils.GetImageRGBA(iImage, image, (int)(width * height * 4));

			if (isValid)
			{
				texture = new Texture2D((int)width, (int)height, TextureFormat.RGBA32, false, true);
				texture.LoadRawTextureData(image);
				texture.Apply();
			}
        }
		avatarReceived = true;
		return texture;
	}

	void OnImageLoaded(AvatarImageLoaded_t callback)
	{
		if (callback.m_steamID.m_SteamID == playerSteamID)
			playerIcon.texture = GetSteamImageAsTexture(callback.m_iImage);
		else
			return;
	}


}
