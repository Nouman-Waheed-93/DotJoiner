using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//
public class CoinHandler : MonoBehaviour {

    public Text[] coinText;
    public static CoinHandler instance;

    public void UpdateCoinText(int amount)
    {
        string amountStr;
        if (amount > 9999)
        {
            amountStr = (amount / 1000) + "K";
        }
        else
        {
            amountStr = amount.ToString();
        }
        for(int i = 0; i < coinText.Length; i++)
        {
            coinText[i].text = amountStr;
        }
    }

    public void makeCoinTransaction(int amount) {
        int newCoinAmt = PlayerPrefs.GetInt("Coins", 200) + amount;
        UpdateCoinText(newCoinAmt);
        PlayerPrefs.SetInt("Coins", newCoinAmt);
        NotificationHandler.instance.PushNotification(amount + " Coins added");
    }
    
    public int GetCoinBalance()
    {
        return PlayerPrefs.GetInt("Coins", 200);
    }

	void Start () {
        if (instance == null)
        {
            instance = this;
            UpdateCoinText(GetCoinBalance());
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
	}
	
}
