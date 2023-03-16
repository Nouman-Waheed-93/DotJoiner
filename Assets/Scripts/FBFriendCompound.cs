using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FBFriendCompound : MonoBehaviour {

    public RawImage profilePicture;
    public Text nameText;

    private string id;
    private int friendIndex;

    public void SetUp(int friendIndex, string id, string name, Texture2D profilePicture)
    {
        this.friendIndex = friendIndex;
        this.id = id;
        nameText.text = name;
        this.profilePicture.texture = profilePicture;
    }

    public void Challenge()
    {
        BetScreen.instance.Challenge(friendIndex);
        NMenus.MainMenu.instance.ToBetScreen();
    }

}
