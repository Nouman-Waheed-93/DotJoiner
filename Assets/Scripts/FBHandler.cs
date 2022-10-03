using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Facebook.Unity;
using UnityEngine.Events;
using System.IO;
using System;

public class FBHandler : MonoBehaviour {

    private static readonly List<string> readPermissions = new List<string> { "public_profile", "email", "user_friends" };
    private static readonly List<string> publishPermissions = new List<string> { "publish_actions" };

    public static FBHandler instance;

    public static UnityEvent FBPicLoaded = new UnityEvent();
    public static UnityEvent OppPicLoaded = new UnityEvent();
    public static UnityEvent FBNameLoaded = new UnityEvent();
    public static UnityEvent FBIdLoaded = new UnityEvent();
    public static UnityEvent OnFBLoginSuccess = new UnityEvent();
    public static UnityEvent OnFBLoginInitiated = new UnityEvent();
    public static UnityEvent OnFBLoginFailed = new UnityEvent();
    public static UnityEvent OnFBLogout = new UnityEvent();
    public static event Action<string> FriendsListUpdated;

    public int SharingRewardAmount;
    public Button ShareButton;
  //  public GameObject PlayerLoggedINUI;
    public static Sprite FBProfilePhoto;
    public static Sprite OpponentProfilePhoto;
    public Transform fbFriendsScreen;
    public static string UsernameTxt;
    public static string UserIdTxt;

    private void Awake()
    {
        if (instance)
            Destroy(instance);
        else
            instance = this;
    }

    // Use this for initialization
    void Start () {

        if (!FB.IsInitialized)
        {
            // Initialize the Facebook SDK
            FB.Init(InitCallback);
        }
        else
        {
            FB.ActivateApp();
            // Already initialized, signal an app activation App Event
            LoginFacebookAutomatically();
        }
        FBProfilePhoto = PictureChooser.ChoosePicture();
        OpponentProfilePhoto = PictureChooser.GetRandomPic();
    }

    private void InitCallback()
    {
        if (FB.IsInitialized)
        {
            FB.ActivateApp();
            LoginFacebookAutomatically();
        }
        else
        {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
    }
    
    public void OnFBButtonClicked() {
        OnFBLoginInitiated?.Invoke();
        if (FB.IsInitialized)
        {
            LoginFacebook();
        }
        else
        {
            FB.Init(InitCallbackWithLogin);
        }
    }

    private void InitCallbackWithLogin()
    {
        if (FB.IsInitialized)
        {
            FB.ActivateApp();
            if (FB.IsLoggedIn)
            {
                FB.Mobile.RefreshCurrentAccessToken((IAccessTokenRefreshResult result) => {
                    OnLoginComplete();
                });
            }
            else
            {
                LoginFacebook();
            }
        }
        else
        {
            OnFBLoginFailed?.Invoke();
        }
    }

    public void OnLogOutClicked()
    {
        FB.LogOut();
        OnFBLogout?.Invoke();
    }
    
    public static void GetPlayerInfo()
    {
        string queryString = "/me?fields=id,first_name";
        FB.API(queryString, HttpMethod.GET, GetPlayerInfoCallback);
    }

    public static void GetEnemyProfilePhoto(string id) {
        FB.API("/me/picture?type=square&height=512&width=512", HttpMethod.GET, ProfilePhotoCallback);
    }

    public static void EnemyProfilePhotoCallback(IGraphResult result)
    {
        if (result.Texture != null)
        {
            OpponentProfilePhoto = Sprite.Create(result.Texture, new Rect(0, 0, result.Texture.width, result.Texture.height), new Vector2(0.5f, 0.5f));
            OppPicLoaded.Invoke();
        }
    }
    
    private void OnLoginComplete()
    {
        Debug.Log("OnLoginComplete");

        if (!FB.IsLoggedIn)
        {
            // Reenable the Login Button
            OnFBLoginFailed?.Invoke();
            return;
        }

        // AccessToken class will have session details
        string aToken = AccessToken.CurrentAccessToken.TokenString;
        string facebookId = AccessToken.CurrentAccessToken.UserId;

        // Show loading animations
        GetPlayerInfo();
        OnFBLoginSuccess?.Invoke();
        GetGameFBFriends();
    }

    private static void GetPlayerInfoCallback(IGraphResult result)
    {
        Debug.Log("GetPlayerInfoCallback");
        if (result.Error != null)
        {
            Debug.LogError(result.Error);
            return;
        }
        Debug.Log(result.RawResult);

        //Save player id
        if (result.ResultDictionary.TryGetValue("id", out UserIdTxt))
        {
            FBIdLoaded.Invoke();
        }
        // Save player name
        string name;
        if (result.ResultDictionary.TryGetValue("first_name", out name))
        {
            UsernameTxt = name;
            FBNameLoaded.Invoke();
            //FB.API("/me/picture?redirect=false", HttpMethod.GET, ProfilePhotoCallback);
            FB.API("/me/picture?type=square&height=512&width=512", HttpMethod.GET, ProfilePhotoCallback);

        }
    }

    private static void ProfilePhotoCallback(IGraphResult result)
    {
        if (result.Texture != null)
        {
            FBProfilePhoto = Sprite.Create(result.Texture, new Rect(0, 0, result.Texture.width, result.Texture.height), new Vector2(0.5f, 0.5f));
            FBPicLoaded.Invoke();
        }
    }

    private static IEnumerator fetchProfilePic(string url)
    {
        WWW www = new WWW(url);
        yield return www;
        FBProfilePhoto = Sprite.Create(www.texture, new Rect(0,0,www.texture.width, www.texture.height), new Vector2(0.5f, 0.5f));
        FBPicLoaded.Invoke();
    }

    public void OnShareClicked() {
        FB.LogInWithPublishPermissions(publishPermissions, delegate (ILoginResult result)
        {

            if (result.Error != null || result.Cancelled)
            {
                Debug.LogError(result.Error);
            }
            else
             FB.FeedShare("", new Uri("https://play.google.com/store/apps/details?id=com.GalyaKela.WOW"), "Join the dots", "make the box.",
                "Get the points.", null, "", SharingDone);
            
        });
    }

    public void ShareAchievement(string Title, string Detail, string Link) {

        FB.LogInWithPublishPermissions(publishPermissions, delegate (ILoginResult result)
        {

            if (result.Error != null || result.Cancelled)
            {
                Debug.LogError(result.Error);
            }
            else
                FB.FeedShare("", new Uri("https://play.google.com/store/apps/details?id=com.GalyaKela.WOW"), Title, Detail,
Detail, new Uri(Link), "", AchievmentShared);

            Debug.Log("Not Logged In");

        });

    }

    void AchievmentShared(IShareResult result) {
     //   SPVGM.instance.ShareBtn.SetActive(false);
     //   PlayerPrefs.SetInt(GlobalVals.Credits, PlayerPrefs.GetInt(GlobalVals.Credits, 0) + 20);
    }

    void SharingDone(IShareResult result) {
        if (result.Cancelled || result.Error != null)
        {
            Debug.Log("COuld not share");
            return;
        }
        CoinHandler.instance.makeCoinTransaction(SharingRewardAmount);
        Debug.Log("We were successful");
        ShareButton.interactable = false;
    }

    public void GetGameFBFriends()
    {
        string query = "/me/friends?fields=id,name,picture.type(square).height(512).width(512)";
        FB.API(query, HttpMethod.GET, GetLIstOfPlayers);
    }
    
    void GetLIstOfPlayers(IGraphResult result)
    {
        FriendsListUpdated.Invoke(result.ResultDictionary.ToJson());
        Debug.Log("Friends frineds firn");
    }

    private void LoginFacebookAutomatically()
    {
        if (FB.IsLoggedIn)
        {
            FB.Mobile.RefreshCurrentAccessToken((IAccessTokenRefreshResult result)=> {
                OnLoginComplete();
            });
        }
    }

    private void LoginFacebook()
    {
        FB.LogInWithReadPermissions(readPermissions, delegate (ILoginResult result)
        {
            Debug.Log("LoginCallback");
            if (FB.IsLoggedIn)
            {
                OnLoginComplete();
            }
            else
            {
                if (result.Error != null)
                {
                    Debug.LogError(result.Error);
                }
                Debug.Log("Not Logged In");
                OnFBLoginFailed?.Invoke();
            }
        });
    }
}
