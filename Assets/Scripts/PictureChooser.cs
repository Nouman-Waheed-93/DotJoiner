using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureChooser : MonoBehaviour {

    public static PictureChooser instance;

    public Sprite[] pictures;

    public void Awake()
    {
        instance = this;
    }

    public static Sprite ChoosePicture()
    {
        int index = 0;
        if (PlayerPrefs.HasKey("PlayerPicture"))
        {
            index = PlayerPrefs.GetInt("PlayerPicture");
        }
        else
        {
            index = Random.Range(0, instance.pictures.Length);
            PlayerPrefs.SetInt("PlayerPicture", index);
        }
        instance.GetComponent<UnityEngine.UI.Image>().sprite = instance.pictures[index];
        return instance.pictures[index];
    }

    public static Sprite GetRandomPic()
    {
        return instance.pictures[Random.Range(0, instance.pictures.Length)];
    }

}
