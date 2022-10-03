public interface IFacebookLoginHandler
{
    void OnFacebookLoginSuccess();
    void OnFacebookLoginFailed();
    void OnFacebookLoginInitiated();
    void OnFacebookLogout();
}
