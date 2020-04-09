using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YourTurnMsg : MonoBehaviour {

    public float fadeTime = 1;

    public void ShowMessage()
    {
        gameObject.SetActive(true);
        Invoke("FadeAway", fadeTime);
        GetComponent<UnityEngine.UI.Image>().CrossFadeAlpha(0, fadeTime, false);
    }

    void FadeAway() {
        gameObject.SetActive(false);
    }

}
