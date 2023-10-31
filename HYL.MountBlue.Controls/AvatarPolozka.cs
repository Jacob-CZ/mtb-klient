using System.Drawing;

namespace HYL.MountBlue.Controls;

public class AvatarPolozka
{
	private Image mimgAvatar;

	private bool mbolVlastni;

	private int mintAvatarID;

	public Image Avatar => mimgAvatar;

	public bool VlastniAvatar => mbolVlastni;

	public int AvatarID => mintAvatarID;

	public AvatarPolozka(Image avatar, bool vlastniAvatar, int avatarID)
	{
		mimgAvatar = avatar;
		mbolVlastni = vlastniAvatar;
		mintAvatarID = avatarID;
	}
}
