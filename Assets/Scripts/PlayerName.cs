using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerName : MonoBehaviour {
    
	void Start ()
    {
        GetComponent<Text>().text = PlayerProfile.GetName();
        PlayerProfile.instance.onNameUpdated.AddListener(NameChanged);
        FBHandler.FBNameLoaded.AddListener(GotFBName);
	}
	
    void NameChanged(string newName)
    {
        GetComponent<Text>().text = newName;
    }

    void GotFBName()
    {
        GetComponent<Text>().text = FBHandler.UsernameTxt;
    }

}
