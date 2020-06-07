using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace NMenus
{
    public class ChallengeNotification : MonoBehaviour
    {
        public RawImage profilePic; //profile pic of the challenger
        public Text challengeWorthHeading; // text that shows the amount of bet 
        public Text challengeDetails; //text that shows who challenged you

        IntStringDelegate challengeAcceptedCallback;
        string challengeId;
        int challengeWorth;
        
        //someone has challenged the player to a game
        public void Challenged(string challengeId, string challengerId, string challengeWorth, 
            IntStringDelegate challengeAcceptedCallback)
        {
            PlayerJSONData challengerData = WhoChallenged(challengerId);
            profilePic.texture = challengerData.profilePic;
            this.challengeId = challengeId;
            this.challengeWorth = Int32.Parse(challengeWorth);
            this.challengeWorthHeading.text = challengeWorth + " Challenge";
            this.challengeDetails.text = challengerData.name + " challenged you. Do you want to accept the challenge";
            this.challengeAcceptedCallback = challengeAcceptedCallback;
            gameObject.SetActive(true);
        }

        //mplayer has accepted the challenge
        public void AcceptChallenge()
        {
            challengeAcceptedCallback.Invoke(challengeId, challengeWorth);
            gameObject.SetActive(false);
        }

        //player has rejected the challenge
        public void RejectChallenge()
        {
            gameObject.SetActive(false);
        }

        private PlayerJSONData WhoChallenged(string id)
        {
            for(int i = 0; i < FBFriendsData.friendsData.data.Length; i++)
            {
                PlayerJSONData data = FBFriendsData.friendsData.data[i];
                if (data.id == id)
                    return data;
            }
            PlayerJSONData nullVal = new PlayerJSONData();
            return nullVal;
        }
    }
}