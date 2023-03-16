using System.Collections;
using UnityEngine;

public class FBFriendsData : MonoBehaviour
{
    public Transform friendsContainer;
    public static FriendsRawData friendsData;
    
    public Texture2D profilePic;

    void Start()
    {
        FBHandler.FriendsListUpdated += FriendsDataReceived;
    }

    public void SaveFriendsData()
    {

    }

    public void LoadFriendsDataFromFile()
    {

    }
    
    private void FriendsDataReceived(string data)
    {
        StartCoroutine(nameof(LoadFriendsDataFromString),data);
    }

    private IEnumerator LoadFriendsDataFromString(string jsonString)
    {
        friendsData = JsonUtility.FromJson<FriendsRawData>(jsonString);
        for (int i = 0; i < friendsData.data.Length; i++)
        {
            var entry = friendsData.data[i];
            FBFriendCompound fbFriend = Instantiate(Resources.Load<GameObject>("FBFriend"), friendsContainer).GetComponent<FBFriendCompound>();
            yield return getImage(entry.picture.data.url);
            entry.profilePic = this.profilePic;
            fbFriend.SetUp(i, entry.id, entry.name, entry.profilePic);
        }
        foreach(var ppp in friendsData.data)
        {
            if (ppp.profilePic == null)
                Debug.Log(ppp.name + " picture is null");
            else
                Debug.Log(ppp.name + " picture ist da");
        }
        Debug.Log("Complete friends gottend");
    }

    IEnumerator getImage(string url)
    {
        profilePic = new Texture2D(512, 512);
        WWW www = new WWW(url);
        yield return www;
        www.LoadImageIntoTexture(profilePic);
        www.Dispose();
        www = null;
    }
}