using System.Collections;
using UnityEngine;

[System.Serializable]
struct FriendsRawData
{
    public PlayerJSONData[] data;
}

[System.Serializable]
struct PlayerJSONData
{
    public string id;
    public string name;
    public RawPictureData picture;
}

[System.Serializable]
struct RawPictureData
{
    public PictureData data;
}

[System.Serializable]
public struct PictureData
{
    public int height;
    public int width;
    public string url;
}

public class FBFriendsData : MonoBehaviour
{
    public Transform friendsContainer;

    Texture2D profilePic;
    
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
        StartCoroutine("LoadFriendsDataFromString",data);
        Debug.Log("Vaat tor di");
    }

    private IEnumerator LoadFriendsDataFromString(string jsonString)
    {
        var data = JsonUtility.FromJson<FriendsRawData>(jsonString);
        foreach (var entry in data.data)
        {
            FBFriendCompound fbFriend = Instantiate(Resources.Load<GameObject>("FBFriend"), friendsContainer).GetComponent<FBFriendCompound>();
            yield return getImage(entry.picture.data.url);
            fbFriend.SetUp(entry.id, entry.name, profilePic);
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