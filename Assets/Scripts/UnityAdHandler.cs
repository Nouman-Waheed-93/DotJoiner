using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class UnityAdHandler : MonoBehaviour {

    public GameObject Notification;
    public Text rewardText;

    int currReward;

	// Use this for initialization
	void Start () {
        Advertisement.Initialize("3063061");
    }
    public void ShowRewardedAd()
    {
        currReward = 50;
        if (Advertisement.IsReady("rewardedVideo"))
        {
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show("rewardedVideo", options);
        }
        else
            Notification.SetActive(true);
    }

    public void ShowSecondAd()
    {
        currReward = 30;
        if (Advertisement.IsReady("video"))
        {
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show("video", options);
        }
        else
            Notification.SetActive(true);
    }

    public void ShowThirdAd()
    {
        currReward = 20;
        if (Advertisement.IsReady("RAD2"))
        {
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show("RAD2", options);
        }
        else
            Notification.SetActive(true);
    }

    public void ShowForthAd()
    {
        currReward = 50;
        if (Advertisement.IsReady("RAD3"))
        {
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show("RAD3", options);
        }
        else
            Notification.SetActive(true);
    }

    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");
                NMenus.MainMenu.instance.ToRewardScreen();
                rewardText.text = currReward.ToString();
                CoinHandler.instance.makeCoinTransaction(currReward);
                //
                // YOUR CODE TO REWARD THE GAMER
                // Give coins etc.
                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");
                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");
                break;
        }
    }

}
