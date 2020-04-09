using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerName : MonoBehaviour {
    
	void Start ()
    {
        GetComponent<Text>().text = PlayerProfile.GetName();
        FBHandler.FBNameLoaded.AddListener(GotFBName);
	}
	
    void GotFBName()
    {
        GetComponent<Text>().text = FBHandler.UsernameTxt;
    }

}
