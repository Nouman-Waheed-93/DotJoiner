using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace NMenus
{
    public class InGameMenu : MonoBehaviour
    {
        public static InGameMenu instance;

        public GameObject miniMenuGO; //The ingame menu gameObject
        public GameObject notificationGO;
        public GameObject ResultGO;
        public GameObject FailedGO;
        public GameObject rewardGO;
        [SerializeField]
        private Text winnerNameTxt, rewardText, loseScoreTxt;

        
        private void Start()
        {
            instance = this;
        }

        public void ToggleMiniMenu() {
            miniMenuGO.SetActive(!miniMenuGO.activeSelf);
        }

        public void ToggleNotificationPanel()
        {
            notificationGO.SetActive(!notificationGO.activeSelf);
        }

        public void ShowResult(string winnerName, string reward)
        {
            ResultGO.SetActive(true);
            rewardText.gameObject.SetActive(reward != "0");
            rewardGO.SetActive(rewardText.gameObject.activeSelf);
            rewardText.text = reward;
            winnerNameTxt.text = winnerName;
        }

        public void MissionFailed(int score)
        {
            loseScoreTxt.text = score.ToString();
            FailedGO.SetActive(true);
        }

        public void DrawGame() {
            FailedGO.SetActive(true);
        }

        public void ToMainMenu() {
            ResultGO.SetActive(false);
            FailedGO.SetActive(false);
            GameManager.instance.EndGame();
            MainMenu.instance.ToMenuFromGame();
        }

    }
}
