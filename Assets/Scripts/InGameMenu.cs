﻿using UnityEngine;
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
        [SerializeField]
        private Text winnerNameTxt, rewardText;

        
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
            rewardText.text = reward;
            winnerNameTxt.text = winnerName;
        }

        public void MissionFailed()
        {
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
