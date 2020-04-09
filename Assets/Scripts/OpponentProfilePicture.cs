using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentProfilePicture : MonoBehaviour {
    
    void Start()
    {
        FBHandler.OppPicLoaded.AddListener(OnPicLoaded);
        GetComponent<UnityEngine.UI.Image>().sprite = FBHandler.OpponentProfilePhoto;
    }

    void OnPicLoaded()
    {
        GetComponent<UnityEngine.UI.Image>().sprite = FBHandler.OpponentProfilePhoto;
    }

}
