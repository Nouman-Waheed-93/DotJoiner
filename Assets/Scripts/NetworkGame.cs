using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkGame : IGameType {

    NetworkPlayer remotePlayer, localPlayer;

    public bool HaveAuthority()
    {
        Debug.Log("On server " + Photon.Pun.PhotonNetwork.IsMasterClient);
        return Photon.Pun.PhotonNetwork.IsMasterClient;
    }
    
    public Player CreateOpponent()
    {
        remotePlayer = new NetworkPlayer("Opponent", false); //create a remote player
        return remotePlayer;
    }

    public void RegisterRemotePlayer(PunPlayer player)
    {
        remotePlayer.RegisterPlayer(player);
    }
    
    public void RegisterLocalPlayer(PunPlayer player)
    {
        localPlayer.RegisterPlayer(player);
    }

    public Player CreatePlayer()
    {
        localPlayer = new NetworkPlayer(PlayerProfile.GetName(), true);
        return localPlayer;  //create a local player
    }
    
    public void EndGame() {
        NetworkGameHandler.instance.Disconnect();
    }
}
