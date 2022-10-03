using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacebookLoginPropogator : MonoBehaviour
{
    [SerializeField]
    GameObject[] loginHandlersGO;

    void Start()
    {
        List<IFacebookLoginHandler> facebookLoginHandlers = new List<IFacebookLoginHandler>();
        foreach(GameObject go in loginHandlersGO)
        {
            IFacebookLoginHandler[] loginHandler = go.GetComponents<IFacebookLoginHandler>();
            facebookLoginHandlers.AddRange(loginHandler);
        }
        foreach(IFacebookLoginHandler fbLoginHandler in facebookLoginHandlers)
        {
            FBHandler.OnFBLoginInitiated.AddListener(fbLoginHandler.OnFacebookLoginInitiated);
            FBHandler.OnFBLoginFailed.AddListener(fbLoginHandler.OnFacebookLoginFailed);
            FBHandler.OnFBLoginSuccess.AddListener(fbLoginHandler.OnFacebookLoginSuccess);
            FBHandler.OnFBLogout.AddListener(fbLoginHandler.OnFacebookLogout);
        }
    }

}
