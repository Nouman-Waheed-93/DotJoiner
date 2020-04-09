using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
    public void ToggleMenu()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void EndGame() {
        gameObject.SetActive(false);
        NMenus.MainMenu.instance.ToMenuFromGame();
        GameManager.instance.EndGame();
    }
    
}
