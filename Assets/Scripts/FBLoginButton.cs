using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FBLoginButton : MonoBehaviour, IFacebookLoginHandler
{
    public void OnFacebookLoginInitiated()
    {
        GetComponent<Button>().interactable = false;
    }

    public void OnFacebookLogout()
    {
        GetComponent<Button>().interactable = true;
    }

    public void OnFacebookLoginSuccess()
    {
        GetComponent<Button>().interactable = false;
    }

    public void OnFacebookLoginFailed()
    {
        GetComponent<Button>().interactable = true;
    }

}
