using UnityEngine;


[System.Serializable]
public class FriendsRawData
{
    public PlayerJSONData[] data;
}

[System.Serializable]
public class PlayerJSONData
{
    public string id;
    public string name;
    public Texture2D profilePic;
    public RawPictureData picture;
}

[System.Serializable]
public struct RawPictureData
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

public delegate void IntStringDelegate(string s, int i);

public static class GlobalVals{

}
