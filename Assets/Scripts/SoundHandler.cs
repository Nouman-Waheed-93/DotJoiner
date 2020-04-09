using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHandler : MonoBehaviour {

    public static SoundHandler instance;
    public AudioClip notificationClip, boxMadeClip, lostClip, wonClip, chatClip, lineClip, btnClip;

    AudioSource source;

    public void NotificationAlert() {
        PlaySoundEffect(notificationClip);
        Vibrate();
    }

    public void MessageReceived()
    {
        PlaySoundEffect(chatClip);
        Vibrate();
    }

    public void LineMade()
    {
        PlaySoundEffect(lineClip);
    }

    public void PlayerBoxMade() {
        PlaySoundEffect(boxMadeClip);
    }

    public void OpponentBoxMade()
    {
        Vibrate();
    }

    public void PlayerWon() {
        PlaySoundEffect(wonClip);
    }

    public void PlayerLost() {
        PlaySoundEffect(lostClip);
    }

    public void ButtonClicked()
    {
        PlaySoundEffect(btnClip);
    }

    void Start()
    {
        instance = this;
        source = GetComponent<AudioSource>();
    }

    void PlaySoundEffect(AudioClip clip)
    {
        if (Settings.instance.SoundOn)
            source.PlayOneShot(clip);
    }

    void Vibrate()
    {
        if (Settings.instance.VibrationOn)
            Handheld.Vibrate();
    }

}
