using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageBox : MonoBehaviour {
    public string[] messages;
    public Transform EmojiP;
    public Text messageText;
    public float messageTime;
    float cumMessageTime;

    int currMessageInd;

    public void ShowMessage(int messageInd)
    {
        cumMessageTime = 0;
        SwitchMessageState(false);
        currMessageInd = messageInd;
        SwitchMessageState(true);
        gameObject.SetActive(true);   
    }

    void SwitchMessageState(bool state)
    {
        if (currMessageInd < messages.Length)
        {
            messageText.text = messages[currMessageInd];
            messageText.gameObject.SetActive(state);
        }
        else
        {
            EmojiP.GetChild(currMessageInd - messages.Length).gameObject.SetActive(state);
        }
    }

    private void Update()
    {
        if(messageTime > cumMessageTime)
        {
            cumMessageTime += Time.deltaTime;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
