using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BetScreen : MonoBehaviour {

    public static BetScreen instance;

    public Text betText;
    public int[] betAmounts;

    string opponent;

    int currBetInd;

    public void CreateSingleton()
    {
        instance = this;
    }

    public void ChangeBet(int dir) {
        currBetInd = Mathf.Clamp(currBetInd + dir, 0, betAmounts.Length-1);
        betText.text = betAmounts[currBetInd].ToString() + " Gold";
    }
    
    public void Challenge(string opponent)
    {
        this.opponent = opponent;
    }

    public void FindRival()
    {
        if (CoinHandler.instance.GetCoinBalance() < betAmounts[currBetInd])
            NMenus.MainMenu.instance.ShowNotEnoughCoinNotification();
        else
        {
            if (opponent == "Random")
                NetworkGameHandler.instance.StartGameWithBetAmount(betAmounts[currBetInd]);
            else
                NetworkGameHandler.instance.ChallengeFriend(opponent, betAmounts[currBetInd]);
        }
    }

}
