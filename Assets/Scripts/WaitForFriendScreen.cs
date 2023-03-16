using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaitForFriendScreen : MonoBehaviour
{
    public Text friendName;
    public RawImage friendPhoto;

    public void SetUpAndShowUp(int friendIndex)
    {
        friendName.text = "Waiting for " + FBFriendsData.friendsData.data[friendIndex].name;
        friendPhoto.texture = FBFriendsData.friendsData.data[friendIndex].profilePic;
        gameObject.SetActive(true);
    }

    public void CancelChallenge()
    {
        NetworkGameHandler.instance.Disconnect();
    }

}
