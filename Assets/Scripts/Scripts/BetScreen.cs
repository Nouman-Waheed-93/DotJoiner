using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BetScreen : MonoBehaviour {

    public Text betText;
    public int[] betAmounts;
    
    int currBetInd;

    public void ChangeBet(int dir) {
        currBetInd = Mathf.Clamp(currBetInd + dir, 0, betAmounts.Length-1);
        betText.text = betAmounts[currBetInd].ToString() + " Gold";
    }
    
    public void FindRival()
    {
        if (CoinHandler.instance.GetCoinBalance() < betAmounts[currBetInd])
            NMenus.MainMenu.instance.ShowNotEnoughCoinNotification();
        else
            NetworkGameHandler.instance.StartGameWithBetAmount(betAmounts[currBetInd]);
    }

}
