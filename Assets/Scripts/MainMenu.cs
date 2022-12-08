using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace NMenus
{

    public class MainMenu : MonoBehaviour
    {
        public static MainMenu instance;
        public ChallengeNotification challengePopup;
        [SerializeField]
        private GameObject menuParent, mainMenuGO, BetScreen, matchMakingScreen, ResultGO, SettingsScreenGO, LoadingScreen,
            InGame, FreeCoinsScreenGO, NotEnoughCoinGO, InviteFriendsGO, profileScreenGO, comingSoonGO, rewardGO, FBLoginNotification,
            FriendList, networkError;
        [SerializeField]
        private string moregamesLink, thisGameLink;

        public float ComingSoonScreenTime = 1;
        public GameObject activeScreen;

        public Button[] LevelBtns;

        void Start()
        {
            if (!instance)
            {
                instance = this;
                InGame.SetActive(false);
                DontDestroyOnLoad(gameObject);
                StartCoroutine(ShowLogInMessage());
            }
            else
                Destroy(gameObject);
        }

        public void ToComingSoonScreen() {
            comingSoonGO.SetActive(true);
            // TransitionToScreen(comingSoonGO);
            StartCoroutine(ComingSoonCoroutine());
        }

        IEnumerator ComingSoonCoroutine()
        {
            float cumTime = 0;
            while(cumTime < ComingSoonScreenTime)
            {
                yield return null;
                cumTime += Time.deltaTime;
            }
            comingSoonGO.SetActive(false);
            //            TransitionToScreen(mainMenuGO);
        }

        IEnumerator ShowLogInMessage()
        {
            ShowLoadingScreen();
            while (!FBHandler.instance.HasInitFailed() && !FBHandler.instance.IsInitialized()) 
                yield return null;

            if (FBHandler.instance.IsInitialized())
            {
                if (!FBHandler.instance.IsLoggedIn())
                {
                    FBLoginNotification.SetActive(true);
                    Transform btnTransform = FBLoginNotification.transform.Find("PopUp/CrossBtn");
                    Button btn = btnTransform.GetComponent<Button>();
                    btn.onClick.AddListener(ShowProfileScreenOnCross);
                }
            }
        }

        public void ToRewardScreen()
        {
            rewardGO.SetActive(true);
        }

        public void ToBetScreen() {
            TransitionToScreen(BetScreen);
        }

        public void ToFriendList()
        {
            TransitionToScreen(FriendList);
        }

        public void ShowFBLoginNotification()
        {
            FBLoginNotification.SetActive(true);
        }

        public void ToSettings() {
            SettingsScreenGO.SetActive(true);
        }

        public void ToMatchMakingScreen() {
            TransitionToScreen(matchMakingScreen);
        }
        
        public void ToFreeCoinScreen()
        {
            FreeCoinsScreenGO.SetActive(true);
        }

        public void ShowNotEnoughCoinNotification()
        {
            NotEnoughCoinGO.SetActive(true);
        }

        public void ShowNetworkError()
        {
            networkError.gameObject.SetActive(true);
        }

        public void ToInviteFriendsScreen()
        {
            InviteFriendsGO.SetActive(true);
        }

        public void ToProfileScreen()
        {
            profileScreenGO.SetActive(true);
        }

        public void ShowLoadingScreen()
        {
            activeScreen = LoadingScreen;
            LoadingScreen.SetActive(true);
        }

        public void Quit()
        {
            Application.Quit();
        }
        
        public void OnGameLevelLoaded() {
            //   menuParent.SetActive(false);
            matchMakingScreen.SetActive(false);
            InGame.SetActive(true);
        }

        public void ToMenuFromGame() {
            InGame.SetActive(false);
        }

        private void TransitionToScreen(GameObject newScreen) {
            if(activeScreen)
                activeScreen.SetActive(false);

            activeScreen = newScreen;
            activeScreen.SetActive(true);
        }

        public void SwitchSound(bool state)
        {
            int vol = state ? 1 : 0;
            AudioListener.volume = vol;
            PlayerPrefs.SetInt("Sound", vol);
            print(state + " sound");
        }

        private void ShowProfileScreenOnCross()
        {
            if (activeScreen)
                activeScreen.SetActive(false);

            profileScreenGO.SetActive(true);
            FBLoginNotification.transform.Find("PopUp/CrossBtn").GetComponent<Button>().onClick.RemoveListener(ShowProfileScreenOnCross);
        }
    }
}
