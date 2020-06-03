using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FBFriendCompound : MonoBehaviour {

    public RawImage profilePicture;
    public Text nameText;

    private string id;

    public void SetUp(string id, string name, Texture2D profilePicture)
    {
        this.id = id;
        nameText.text = name;
        this.profilePicture.texture = profilePicture;
    }

    public void Challenge()
    {
        BetScreen.instance.Challenge(id);
        NMenus.MainMenu.instance.ToBetScreen();
    }

}
