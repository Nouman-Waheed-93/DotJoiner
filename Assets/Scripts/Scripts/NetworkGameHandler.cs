using ExitGames.Client.Photon;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class NetworkGameHandler : MonoBehaviourPunCallbacks
{

    public static NetworkGameHandler instance;
    
    int currBetAmount;

    private ulong currentMatchId;

    bool _isMatchmaking, _disconnectServer;

    public bool isServer;

    private void Start()
    {
        instance = this;
    }

    public void Connect()
    {
        if (PhotonNetwork.IsConnected)
            NMenus.MainMenu.instance.ToBetScreen();
        else
        {
            PhotonNetwork.ConnectUsingSettings();
            NMenus.MainMenu.instance.ShowLoadingScreen();
        }
    }

    public void Disconnect()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void CancelMatchMaking()
    {
        Disconnect();
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
            Connect();
    }

    //public override void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matchList)
    //{
    //    base.OnMatchList(success, extendedInfo, matchList);
    //    print(matchList.Count + " matches");
    //    if (success)
    //    {
    //        if (matchList.Count > 0)
    //        {
    //            print("More than one match");
    //            JoinMatch(matchList[0]);
    //        }
    //        else
    //        {
    //            CreateMatch();
    //        }
    //    }

    //}


    //public void JoinMatch(MatchInfoSnapshot matchToJoin)
    //{

    //    matchMaker.JoinMatch(matchToJoin.networkId, "", "", "", 0, 0, OnMatchJoined);
    //    _isMatchmaking = true;
    //}

    //public override void OnMatchJoined(bool success, string extendedInfo, MatchInfo matchInfo)
    //{
    //    base.OnMatchJoined(success, extendedInfo, matchInfo);

    //    isServer = false;
    //    CoinHandler.instance.makeCoinTransaction(-currBetAmount);
    //    MatchMakingScreen.instance.MatchMade("KuchBhi", currBetAmount * 2);

    //}

    //public void CreateMatch()
    //{
    //    matchMaker.CreateMatch("Fight" + currBetAmount, 2, true, "", "", "", 0, 0, OnMatchCreate);
    //    _isMatchmaking = true;
    //}

    //public override void OnMatchCreate(bool success, string extendedInfo, MatchInfo matchInfo)
    //{
    //    base.OnMatchCreate(success, extendedInfo, matchInfo);

    //    if (success)
    //    {
    //        isServer = true;
    //        currentMatchId = (System.UInt64)matchInfo.networkId;
    //    }

    //}

    //public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    //{
    //    base.OnServerAddPlayer(conn, playerControllerId);
    //    if (numPlayers == 2)
    //    {
    //        CoinHandler.instance.makeCoinTransaction(-currBetAmount);
    //        MatchMakingScreen.instance.MatchMade("Loch bhi", currBetAmount * 2);
    //    }
    //}

    //public override void OnDestroyMatch(bool success, string extendedInfo)
    //{
    //    base.OnDestroyMatch(success, extendedInfo);
    //    if (_disconnectServer)
    //    {
    //        print("Stopped");
    //        StopMatchMaker();
    //        StopHost();
    //    }

    //}

    //public override void OnServerDisconnect(NetworkConnection conn)
    //{
    //    base.OnServerDisconnect(conn);
    //    //End game and do stuff

    //}

    //public override void OnStopClient()
    //{
    //    base.OnStopClient();
    //    print("stop client");
    //    _isMatchmaking = false;
    //}

    //public override void OnStartHost()
    //{
    //    base.OnStartHost();
    //    print("Hoo hoo strarting host aeas");
    //}

    //public override void OnServerConnect(NetworkConnection conn)
    //{
    //    Debug.Log("A client connected to the server: " + conn);
    //}

    //public override void OnClientConnect(NetworkConnection conn)
    //{
    //    base.OnClientConnect(conn);
    //    Debug.Log("OnClient connecticut ondfdfd");
    //    ChangeNumberOfPlayers(1, "");
    //    if (!NetworkServer.active)
    //    {//only to do on pure client (not self hosting client)
    //     //   ChangeScreen(MainScreen);
    //     //            endGameDelegate = StopClientClbk;
    //    }
    //}


    //public override void OnClientDisconnect(NetworkConnection conn)
    //{
    //    base.OnClientDisconnect(conn);
    //    print("CLient disconnect");
    //    _isMatchmaking = false;
    //}

    //public override void OnClientError(NetworkConnection conn, int errorCode)
    //{
    //    base.OnClientError(conn, errorCode);
    //    _isMatchmaking = false;
    //}

    //public void ChangeNumberOfPlayers(int delta, string otherPlayerName)
    //{
    //    if (NetworkServer.connections.Count == 2)
    //    {
    //        RematchBtn.SetActive(true);
    //        CoinHandler.instance.makeCoinTransaction(-currBetAmount);
    //        MatchMakingScreen.instance.MatchMade(otherPlayerName, currBetAmount * 2);
    //        ChangeScreen(BattlePreparationScreen);
    //    }
    //    else
    //    {
    //        //       RematchBtn.SetActive(false);
    //        //        if (_isMatchmaking)
    //        //           ChangeScreen(LoadingScreen);
    //        //      else
    //        //         ChangeScreen(MainScreen);
    //    }
    //}

    //public override void OnStartClient(NetworkClient client)
    //{
    //    Debug.Log("Aeo lo");
    //    base.OnStartClient(client);
    //    Debug.Log("ye lo gajar");
    //}

    //public override void OnStartServer()
    //{
    //    base.OnStartServer();
    //    Debug.Log("Commando dekh lyo");
    //}

    //public void StopHostClbk()
    //{
    //    if (_isMatchmaking)
    //    {
    //        print("Destroying Match ");
    //        matchMaker.DestroyMatch((NetworkID)currentMatchId, 0, OnDestroyMatch);
    //        _disconnectServer = true;
    //    }
    //    else
    //    {
    //        print("Stopping Host");
    //        StopHost();
    //    }

    //    _isMatchmaking = false;
    //    // ChangeScreen(MainScreen);
    //}

    //public void StopClientClbk()
    //{
    //    StopClient();

    //    if (_isMatchmaking)
    //    {
    //        StopMatchMaker();
    //    }

    //    _isMatchmaking = false;
    //}

    public override void OnConnected()
    {
        NMenus.MainMenu.instance.ToBetScreen();
        Debug.Log("Yahan aye hen janab");
    }

    public override void OnConnectedToMaster()
    {
        NMenus.MainMenu.instance.ToBetScreen();
        Debug.Log("Yahan aye hen janab");
    }
    
    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            Debug.Log("Hamo b jayen ge sath me");
            CoinHandler.instance.makeCoinTransaction(-currBetAmount);
            MatchMakingScreen.instance.MatchMade("", currBetAmount * 2);
            PhotonNetwork.Instantiate("PUNPlayer", Vector3.zero, Quaternion.identity, 0);
            photonView.RPC("RPCStartGame", RpcTarget.Others);
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

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        throw new System.NotImplementedException();
    }

    public override void OnLeftRoom()
    {
        throw new System.NotImplementedException();
    }
}
