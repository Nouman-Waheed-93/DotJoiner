using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchMakingScreen : MonoBehaviour {

    public Text localPlayerNameTxt, opponentNameTxt;

    public static MatchMakingScreen instance;

	// Use this for initialization
	void Awake () {
        instance = this;
	}

    public void StartMatchMaking() {
        localPlayerNameTxt.text = PlayerProfile.GetName();
        opponentNameTxt.text = "";
    }

    public void MatchMade(string opponentName, int betAmount) {
        opponentNameTxt.text = opponentName;
        StartCoroutine("StartMatch", betAmount);
    }

    IEnumerator StartMatch(int betAmount) {
        yield return null;
        NMenus.MainMenu.instance.OnGameLevelLoaded();
        GameManager.instance.StartOnlineGame(betAmount);
    }

    public void StopMatchMaking()
    {
        NetworkGameHandler.instance.CancelMatchMaking();   
    }

    // Update is called once per frame
    void Update () {
		
	}
}
