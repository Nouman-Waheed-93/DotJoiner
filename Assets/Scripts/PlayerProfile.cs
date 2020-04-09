using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerProfile : MonoBehaviour {

    public static PlayerProfile instance;

    public InputField playerNameField;
    public Text coinTxt, scoreTxt, playerID, gamesWonTxt, WinPercentTxt, totalGamesPlayedTxt;
    
    public void CreateSingleton()
    {
        instance = this;
    }

	void Start () {
        LoadProfile();
        FBHandler.FBNameLoaded.AddListener(LoadFBName);
	}
	
    void LoadProfile()
    {
     //   LoadFBName();
        playerNameField.text = GetName();
        coinTxt.text = CoinHandler.instance.GetCoinBalance().ToString();
        scoreTxt.text = "0";
        playerID.text = "0";
        gamesWonTxt.text = PlayerPrefs.GetInt("GamesWon", 0).ToString();
        WinPercentTxt.text = ((float)(PlayerPrefs.GetInt("GamesWon", 0) / (float)PlayerPrefs.GetInt("GamesPlayed", 1)) * 100f).ToString();
        totalGamesPlayedTxt.text = PlayerPrefs.GetInt("GamesPlayed", 0).ToString();
    }

    void LoadFBName()
    {
        if (FBHandler.UsernameTxt != null || FBHandler.UsernameTxt != "")
        {
            playerNameField.text = FBHandler.UsernameTxt;
            SetName(FBHandler.UsernameTxt);
        }
    }

    public void SetName(string name)
    {
        PlayerPrefs.SetString("PlayerName", name);
    }
    
    public static string GetName()
    {
        return PlayerPrefs.GetString("PlayerName", NameChoser.RandomName());
    }

    public void GameStarted() {
        PlayerPrefs.SetInt("GamesPlayed", PlayerPrefs.GetInt("GamesPlayed", 0) + 1);
    }

    public void GameWon() {
        PlayerPrefs.SetInt("GamesWon", PlayerPrefs.GetInt("GamesWon", 0) + 1);
    }

    public void SaveProfile()
    {
        SetName(playerNameField.text);
        gameObject.SetActive(false);
    }

}
