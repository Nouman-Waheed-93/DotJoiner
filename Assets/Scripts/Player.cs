﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public abstract class Player {

    public string name;
    public char initial;
    public int score;

    public Player(string name) {
        this.name = name;
        this.initial = name.ToCharArray()[0];
    }

    public abstract Connection MakeMove();
    public abstract void GiveMessage(int messageID);

}

public class HumanPlayer : Player {

    public HumanPlayer(string name) : base(name) { }

    public override Connection MakeMove()
    {
        if (Timer.instance.TickTimer())
            return VisualLineHandler.instance.GetHumanPlayerMove();
        return MakeRandomMove();
    }

    private Connection MakeRandomMove()
    {
        Move bestMove = LineDotAlgorithms.GetARandomMove(LineDotAlgorithms.FirstDotInTheGrid);
        VisualLineHandler.instance.CreateLine(bestMove.connection.LeftUpDot, bestMove.connection.RightDownDot);
        return bestMove.connection;
    }

    public override void GiveMessage(int messageID)
    {

    }

}

public class AIPlayer : Player {

    public AIPlayer(string name) : base(name) { }

    const float moveTime = 1;
    float cumMoveTime = 0;

    public override Connection MakeMove()
    {
        if (cumMoveTime > moveTime)
        {
            cumMoveTime = 0;
            return MakeMoveNow();
        }
        cumMoveTime += Time.deltaTime;
        return null;    
    }

    public static Connection MakeMoveNow() {
        Move bestMove = LineDotAlgorithms.FindBestMove(LineDotAlgorithms.FirstDotInTheGrid);
        VisualLineHandler.instance.CreateLine(bestMove.connection.LeftUpDot, bestMove.connection.RightDownDot);
        return bestMove.connection;
    }

    public override void GiveMessage(int messageID)
    {
    }
}

public class NetworkPlayer : Player {

    PunPlayer player;
    Connection move; //The move player will make

    public NetworkPlayer(string name, bool localPlayer) : base(name){
        if (localPlayer)
        {
        }
    }

    public override void GiveMessage(int messageID)
    {
        player.SendChatMessage(messageID);
    }

    public void RegisterPlayer(PunPlayer punPlayer) {
        Debug.Log("Player registered");
        player = punPlayer;
        player.MoveMade.AddListener(MoveMade);
        if (punPlayer.photonView.IsMine)
            punPlayer.photonView.RPC("RpcSetName", Photon.Pun.RpcTarget.OthersBuffered, name);
    }

    bool moveRequested;

    public override Connection MakeMove()
    {
        if (player && !moveRequested)
        {
            move = null;
            player.photonView.RPC("RpcMakeMove", Photon.Pun.RpcTarget.AllBuffered);
            moveRequested = true;
        }
        if (move != null)
        {
            moveRequested = false;
        }
        return move;
    }

    public void MoveMade(Connection move) {
        this.move = move;
        if (!Photon.Pun.PhotonNetwork.IsMasterClient)
        {
            GameManager.instance.CurrentPlayer = this;
            GameManager.instance.CheckBoxesMadeThisMove(move);
            if (GameManager.instance.HaveAllBoxesBeenMade())
                GameManager.instance.DeclareWinner();
        }
    }

}
