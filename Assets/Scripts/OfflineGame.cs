using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfflineGame : IGameType {

    public bool HaveAuthority() {
        return true;
    }

    public Player CreatePlayer()
    {
        return new HumanPlayer(PlayerProfile.GetName());
    }

    public Player CreateOpponent()
    {
        return new AIPlayer(NameChoser.RandomName());
    }

    public void EndGame()
    {
        
    }

}

public static class NameChoser {

    static string[] aiNames = {
            "George",
            "Harry",
            "Jack",
            "Noah",
            "Oliver"
        };

    public static string NameAtIndex(int nameInd)
    {
        return aiNames[nameInd];
    }

    public static string RandomName()
    {
        return aiNames[Random.Range(0, aiNames.Length)];
    }

}
