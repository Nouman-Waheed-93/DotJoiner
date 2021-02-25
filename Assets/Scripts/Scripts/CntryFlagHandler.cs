using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CntryFlagHandler : MonoBehaviour {

    [System.Serializable]
    struct CountryData
    {
        public Sprite flag;
        public Sprite roundFlag;
        public string name;
    }

    public static CntryFlagHandler instance;

    public UnityEvent CountryChanged = new UnityEvent();
    public Transform flagParent;
    [SerializeField]
    CountryData[] countries;

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
        return countries[selectedFlag].name;
    }

    public Sprite GetFlag()
    {
        return countries[selectedFlag].flag;
    }

    public Sprite GetRoundFlag()
    {
        return countries[selectedFlag].roundFlag;
    }

    public int GetCountryIndex()
    {
        return selectedFlag;
    }

}
