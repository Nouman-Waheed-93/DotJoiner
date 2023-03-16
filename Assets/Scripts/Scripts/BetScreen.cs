using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BetScreen : MonoBehaviour {

    public static BetScreen instance;

    public Text betText;
    public int[] betAmounts;

    int opponentIndex;

    int currBetInd;

    public void CreateSingleton()
    {
        instance = this;
    }

    public void ChangeBet(int dir) {
        currBetInd = Mathf.Clamp(currBetInd + dir, 0, betAmounts.Length-1);
        betText.text = betAmounts[currBetInd].ToString() + " Gold";
    }
    
    public void Challenge(int opponentIndex)
    {
        this.opponentIndex = opponentIndex;
    }

    public void FindRival()
    {
        if (CoinHandler.instance.GetCoinBalance() < betAmounts[currBetInd])
            NMenus.MainMenu.instance.ShowNotEnoughCoinNotification();
        else
        {
            if (opponentIndex == -1)
                NetworkGameHandler.instance.StartGameWithBetAmount(betAmounts[currBetInd]);
            else
                NetworkGameHandler.instance.ChallengeFriend(opponentIndex, betAmounts[currBetInd]);
        }
    }

}
