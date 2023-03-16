using ExitGames.Client.Photon;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Facebook.Unity;
using Photon.Realtime;

public class NetworkGameHandler : MonoBehaviourPunCallbacks
{

    public static NetworkGameHandler instance;

    public WaitForFriendScreen friendWaitScreen;
    
    int currBetAmount;

    private ulong currentMatchId;

    bool _isMatchmaking, _disconnectServer;
    
    private void Awake()
    {
        instance = this;
        FBHandler.OnFBLoginSuccess.AddListener(ConnectToPhoton);
    }

    public void PlayWithFriends()
    {
        if (FBHandler.instance.IsLoggedIn())
        {
            if (PhotonNetwork.IsConnected)
            {
                NMenus.MainMenu.instance.ToFriendList();
            }
            else
            {
                NMenus.MainMenu.instance.ShowPhotonNotConnectedError();
            }
        }
        else
        {
            NMenus.MainMenu.instance.ShowFBLoginNotification();
        }

    }

    public void ConnectToPhoton()
    {
        string aToken = AccessToken.CurrentAccessToken.TokenString;
        string facebookId = AccessToken.CurrentAccessToken.UserId;
        PhotonNetwork.AuthValues = new AuthenticationValues();
        PhotonNetwork.AuthValues.AuthType = CustomAuthenticationType.Facebook;
        PhotonNetwork.AuthValues.UserId = facebookId; // alternatively set by server
        PhotonNetwork.AuthValues.AddAuthParameter("token", aToken);
        PhotonNetwork.ConnectUsingSettings();
    }

    public void Disconnect()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void CancelMatchMaking()
    {
        Disconnect();
    }

    public void ChallengeFriend(int friendIndex, int betAmount)
    {
        currBetAmount = betAmount;
        string friendID = FBFriendsData.friendsData.data[friendIndex].id;
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.CreateRoom(friendID + "_" + PlayerProfile.GetID() + "_"+ currBetAmount.ToString());
            _isMatchmaking = true;
            friendWaitScreen.SetUpAndShowUp(friendIndex);
        }
    }

    public void AccepChallenge(string challengeID, int betAmount)
    {
        currBetAmount = betAmount;
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRoom(challengeID);
        }
    }

    public void StartGameWithBetAmount(int betAmount)
    {
        currBetAmount = betAmount;
        StartFindingBattle();
    }

    public void StartFindingBattle()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRoom(currBetAmount.ToString());
            _isMatchmaking = true;
            NMenus.MainMenu.instance.ToMatchMakingScreen();
        }
        else
            PlayWithFriends();
        
    }

    public void LookUpForChallenges()
    {
        Debug.Log("Hey hey haye haye haye boht ameer");
      //  PhotonNetwork.GetCustomRoomList(TypedLobby.Default, "");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("Got room list");
        foreach (RoomInfo game in roomList)
        {

            string n = game.Name;
            Debug.Log("A game among games " + n);
            Debug.Log(n.Substring(0, n.IndexOf('_')) +" tor");
            Debug.Log(PlayerProfile.GetID());
            if (n.IndexOf('_') > 0 && n.Substring(0, n.IndexOf('_')) == PlayerProfile.GetID() && game.IsOpen && !game.RemovedFromList)
            {
                Debug.Log(game.Name + " is the game ");
                Debug.Log("Last index of _ " + n.LastIndexOf('_'));
                int secondIndexOf_ = n.LastIndexOf('_');
                int idLength = secondIndexOf_ - n.IndexOf('_');
                string challengerName = n.Substring(n.IndexOf('_') + 1, idLength - 1);
                Debug.Log(challengerName);
                string betAmount = n.Substring(secondIndexOf_ + 1);
                Debug.Log(betAmount + " ye lo bet");
                NMenus.MainMenu.instance.challengePopup.Challenged(game.Name, challengerName, betAmount, AccepChallenge);
           //     this.ShowAcceptChallengeBox(challengerName);
            }

        }
    //    Invoke("LookUpForChallenges", 5);
    }
    
    public override void OnConnectedToMaster()
    {
        NMenus.MainMenu.instance.ToFriendList();
        PhotonNetwork.JoinLobby();
        LookUpForChallenges();
    }

    public override void OnCustomAuthenticationFailed(string debugMessage)
    {
        Debug.LogErrorFormat("Error authenticating to Photon using Facebook: {0}", debugMessage);
    }

    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            CoinHandler.instance.makeCoinTransaction(-currBetAmount);
            MatchMakingScreen.instance.MatchMade("", currBetAmount * 2);
            PhotonNetwork.Instantiate("PUNPlayer", Vector3.zero, Quaternion.identity, 0);
            photonView.RPC("RPCStartGame", RpcTarget.OthersBuffered);
        }
    }

    [PunRPC]
    public void RPCStartGame()
    {
        CoinHandler.instance.makeCoinTransaction(-currBetAmount);
        MatchMakingScreen.instance.MatchMade("", currBetAmount * 2);
        PhotonNetwork.Instantiate("PUNPlayer", Vector3.zero, Quaternion.identity, 0);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom(currBetAmount.ToString());
    }
    
    public override void OnLeftRoom()
    {
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        PhotonNetwork.LeaveRoom();
        GameManager.instance.OpponentLeft();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        if(cause != DisconnectCause.DisconnectByClientLogic)
        {
            if(GameManager.instance.MatchMode != null &&
                GameManager.instance.MatchMode.GetType() != typeof(OfflineGame))
                NMenus.MainMenu.instance.ShowNetworkError();
        }
    }
}
