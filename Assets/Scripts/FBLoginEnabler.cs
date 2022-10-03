using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FBLoginEnabler : MonoBehaviour, IFacebookLoginHandler
{
    [SerializeField]
    bool enableOnLogin;

    public void OnFacebookLoginSuccess()
    {
        gameObject.SetActive(enableOnLogin);
    }

    public void OnFacebookLoginFailed()
    {
        gameObject.SetActive(!enableOnLogin);
    }

    public void OnFacebookLoginInitiated()
    {

    }

    public void OnFacebookLogout()
    {
        gameObject.SetActive(!enableOnLogin);
    }
}
