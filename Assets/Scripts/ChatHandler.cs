using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatHandler : MonoBehaviour {

    public static ChatHandler instance;

    public MessageBox localPlayerChatBox;
    public MessageBox opponentChatBox;

    public void CreateSingleton()
    {
        instance = this;
    }
    
    public void ToggleOnOff() {
        gameObject.SetActive(!gameObject.activeSelf);
    }
    
    public void GiveMessage(int messageInd) {
        localPlayerChatBox.ShowMessage(messageInd);
        GameManager.instance.GiveMessage(messageInd);
    }
    
    public void ReceiveMessage(int messageInd) {
        if (Settings.instance.ChatOn)
        {
            SoundHandler.instance.MessageReceived();
            opponentChatBox.ShowMessage(messageInd);
        }
    }

}
