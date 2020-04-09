using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CntryFlagHandler : MonoBehaviour {

    public static CntryFlagHandler instance;
    public Sprite[] flags;
    public string[] names;
    public UnityEvent CountryChanged = new UnityEvent();
    public Transform flagParent;
    
    int selectedFlag;
    int selectedFlagInMenu;

    private void Awake()
    {
        instance = this;
        selectedFlag = PlayerPrefs.GetInt("Country", 0);
    }

    public void SelectFlag(int i)
    {
        Debug.Log("previous flag " + selectedFlagInMenu+ " Flag selected " + i);
        flagParent.GetChild(selectedFlagInMenu).GetComponent<Image>().color = Color.clear;
        selectedFlagInMenu = i;
        flagParent.GetChild(selectedFlagInMenu).GetComponent<Image>().color = Color.white;
    }

    public void SetCountry() {
        selectedFlag = selectedFlagInMenu;
        PlayerPrefs.SetInt("Country", selectedFlag);
        CountryChanged.Invoke();
    }

    public string GetCountryName()
    {
        return names[selectedFlag];
    }

    public Sprite GetCountry()
    {
        return flags[selectedFlag];
    }

    public int GetCountryIndex()
    {
        return selectedFlag;
    }

}
