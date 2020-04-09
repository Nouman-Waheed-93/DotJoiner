using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour {

    public static Settings instance;

    public NMenus.SpriteSwapToggle notificationToggle, soundToggle, vibrationToggle, musicToggle, chatToggle;
    [HideInInspector]
    public bool NotificationOn, SoundOn, VibrationOn, MusicOn, ChatOn;
	
    public void CreateSingleton()
    {
        instance = this;
    }

    private void Start()
    {
    //    LoadSettings();
    }

    public void LoadSettings() {

        notificationToggle.Init();
        soundToggle.Init();
        chatToggle.Init();
        musicToggle.Init();
        vibrationToggle.Init();
        if (PlayerPrefs.GetInt("Notification", 1) == 1)
        {
            NotificationOn = true;
            notificationToggle.SwitchToggle();
        }
        if (PlayerPrefs.GetInt("Sound", 1) == 1) {
            SoundOn = true;
            soundToggle.SwitchToggle();
        }
        if(PlayerPrefs.GetInt("Vibration", 1) == 1)
        {
            VibrationOn = true;
            vibrationToggle.SwitchToggle();
        }
        if(PlayerPrefs.GetInt("Music", 1) == 1)
        {
            MusicOn = true;
            musicToggle.SwitchToggle();
        }
        if(PlayerPrefs.GetInt("Chat", 1) == 1)
        {
            ChatOn = true;
            chatToggle.SwitchToggle();
        }
    }

    public void ToggleNotifications(bool on) {
        NotificationOn = on;
        Debug.Log("Nodon " + on);
    }

    public void ToggleSound(bool on) {
        SoundOn = on;
    }

    public void ToggleVibration(bool on) {
        VibrationOn = on;
    }

    public void ToggleMusic(bool on) {
        MusicOn = on;
    }

    public void ToggleChat(bool on) {
        ChatOn = on;
    }

    public void SaveSettings() {
        PlayerPrefs.SetInt("Notification", NotificationOn ? 1 : 0);
        PlayerPrefs.SetInt("Sound", SoundOn ? 1 : 0);
        PlayerPrefs.SetInt("Vibration", VibrationOn ? 1 : 0);
        PlayerPrefs.SetInt("Music", MusicOn ? 1 : 0);
        BGMusic.instance.gameObject.SetActive(MusicOn);
        PlayerPrefs.SetInt("Chat", ChatOn ? 1 : 0);
        gameObject.SetActive(false);
    }

}
