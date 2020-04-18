using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class MyIntEvent : UnityEvent<Connection>
{
}

public class PunPlayer : MonoBehaviourPun
{

    public MyIntEvent MoveMade = new MyIntEvent();
    bool timeOver = false;

    private void Start()
    {
        StartCoroutine(TryToRegister());
    }
    
    IEnumerator TryToRegister()
    {
        Debug.Log("Registering");
        while (GameManager.instance.MatchMode == null)
        {
            Debug.Log("Cant register");
            yield return null;
        }
        if (!photonView.IsMine)
        {
            Debug.Log("remote registered");
            ((NetworkGame)GameManager.instance.MatchMode).RegisterRemotePlayer(this);
        }
        else
        {
            Debug.Log("Local registered");
            ((NetworkGame)GameManager.instance.MatchMode).RegisterLocalPlayer(this);
        }
    }

    [PunRPC]
    public void RpcSetName(string name)
    {
        if (!photonView.IsMine)
            GameManager.instance.LoadOpponentName(name);
    }
    
    [PunRPC]
    public void RpcMakeMove() {
        Debug.Log("yahan tak to agye");
        timeOver = false;
        if (photonView.IsMine)
        {
            Debug.Log("yahna bhi toh");
            StartCoroutine(MakeMoveCoroutine());
        }
    }

    IEnumerator MakeMoveCoroutine() {
        Connection move = null;
        do
        {
            yield return null;
            move = VisualLineHandler.instance.GetHumanPlayerMove();
        } while (move == null && Timer.instance.TickTimer());

        Timer.instance.Reset();

        if (move == null) {
            move = AIPlayer.MakeMoveNow();
        }

        if (move != null)
        {
            photonView.RPC("RpcMoveMade", RpcTarget.Others, move.IDNumber);
            Debug.Log("Sending move made rpc");
        }
    }

    [PunRPC]
    public void RPCTimeOver() {
        timeOver = true;
    }

    public void SendChatMessage(int messageID)
    {
        if (photonView.IsMine)
            photonView.RPC("RpcChatMessage", RpcTarget.All, messageID);
    }

    [PunRPC]
    void RpcChatMessage(int messageID) {
        if(!photonView.IsMine)
            ChatHandler.instance.ReceiveMessage(messageID);
    }
    
    [PunRPC]
    void RpcMoveMade(int connID)
    {
        Connection conn = LineDotAlgorithms.FindConnectionWithID(LineDotAlgorithms.FirstDotInTheGrid, connID);
        if (!photonView.IsMine)
            VisualLineHandler.instance.CreateLine(conn.LeftUpDot, conn.RightDownDot);
        MoveMade.Invoke(conn);
    }
}
