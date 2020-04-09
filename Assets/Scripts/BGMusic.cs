using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusic : MonoBehaviour {
    public static BGMusic instance;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            if (!GetMusicState())
                gameObject.SetActive(false);
        }
        else
            Destroy(gameObject);
    }

    public bool GetMusicState()
    {
        return PlayerPrefs.GetInt("Music", 1) == 1;
    }
    
}
