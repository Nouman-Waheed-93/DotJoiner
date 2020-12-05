using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerProfile : MonoBehaviour {

    public static PlayerProfile instance;

    public InputField playerNameField;
    public Text coinTxt, scoreTxt, playerID, gamesWonTxt, WinPercentTxt, totalGamesPlayedTxt;
    
    public void CreateSingleton()
    {
        instance = this;
        FBHandler.FBIdLoaded.AddListener(LoadFBID);
        FBHandler.FBNameLoaded.AddListener(LoadFBName);
    }

	void OnEnable () {
        LoadProfile();
	}
	
    void LoadProfile()
    {
     //   LoadFBID();
     //   LoadFBName();
        playerNameField.text = GetName();
        coinTxt.text = CoinHandler.instance.GetCoinBalance().ToString();
   //     scoreTxt.text = "0";
        playerID.text = GetID();
        gamesWonTxt.text = PlayerPrefs.GetInt("GamesWon", 0).ToString();
        WinPercentTxt.text = ((int)((float)(PlayerPrefs.GetInt("GamesWon", 0) / (float)PlayerPrefs.GetInt("GamesPlayed", 1)) * 100f)).ToString();
        totalGamesPlayedTxt.text = PlayerPrefs.GetInt("GamesPlayed", 0).ToString();
    }

    void LoadFBID()
    {
        Debug.Log("HOd id id id");
        if (FBHandler.UserIdTxt != null || FBHandler.UserIdTxt != "")
        {
            playerID.text = FBHandler.UserIdTxt;
            SetID(FBHandler.UserIdTxt);
        }
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

    private void SetID(string id)
    {
        PlayerPrefs.SetString("PlayerID", id);
    }

    public static string GetID()
    {
        return PlayerPrefs.GetString("PlayerID", "");
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
