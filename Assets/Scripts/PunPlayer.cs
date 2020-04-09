using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

[System.Serializable]
public class MyIntEvent : UnityEvent<Connection>
{
}

public class PunPlayer : NetworkBehaviour
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
        if (!isLocalPlayer)
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

    [Command]
    public void CmdSetName(string name)
    {
        RpcSetName(name);
    }

    [ClientRpc]
    void RpcSetName(string name)
    {
        if (!isLocalPlayer)
            GameManager.instance.LoadOpponentName(name);
    }


    public void MakeMove()
    {
        if (isServer)
            RpcMakeMove();
    }

    [ClientRpc]
    void RpcMakeMove() {
        Debug.Log("yahan tak to agye");
        timeOver = false;
        if (isLocalPlayer)
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
            CmdMoveMade(move.IDNumber);
            Debug.Log("Sending move made rpc");
        }
    }

    [RPC]
    public void RPCTimeOver() {
        timeOver = true;
    }

    public void SendChatMessage(int messageID)
    {
        if (isLocalPlayer)
            CmdChatMessage(messageID);
    }

    [ClientRpc]
    void RpcChatMessage(int messageID) {
        if(!isLocalPlayer)
            ChatHandler.instance.ReceiveMessage(messageID);
    }

    [Command]
    void CmdChatMessage(int messageID) {
        RpcChatMessage(messageID);
    }
    
    [Command]
    void CmdMoveMade(int connID) {
        RpcMoveMade(connID);
    }

    [ClientRpc]
    void RpcMoveMade(int connID)
    {
        Connection conn = LineDotAlgorithms.FindConnectionWithID(LineDotAlgorithms.FirstDotInTheGrid, connID);
        if (!isLocalPlayer)
            VisualLineHandler.instance.CreateLine(conn.LeftUpDot, conn.RightDownDot);
        MoveMade.Invoke(conn);
    }
}
