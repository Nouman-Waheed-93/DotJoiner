using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FBPicture : MonoBehaviour {

	// Use this for initialization
	void Start () {
        FBHandler.FBPicLoaded.AddListener(OnPicLoaded);
        GetComponent<UnityEngine.UI.Image>().sprite = FBHandler.FBProfilePhoto;
	}
	
    void OnPicLoaded() {
        GetComponent<UnityEngine.UI.Image>().sprite = FBHandler.FBProfilePhoto;
    }

}
