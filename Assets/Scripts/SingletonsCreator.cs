using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonsCreator : MonoBehaviour {

    public PlayerProfile playerProfile;
    public ChatHandler chatHandler;
    public GameManager gameManager;
    public Settings settings;
    public int notifArrSize;

	// Use this for initialization
	void Awake () {
        if (GameManager.instance == null)
        {
            playerProfile.CreateSingleton();
            chatHandler.CreateSingleton();
            gameManager.CreateSingleton();
            settings.CreateSingleton();
            settings.LoadSettings();
            new NotificationHandler(notifArrSize);
        }
        Destroy(gameObject);
    }
	
}
