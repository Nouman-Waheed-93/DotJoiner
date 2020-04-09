using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountryName : MonoBehaviour {

    private void Start()
    {
        GetComponent<Text>().text = CntryFlagHandler.instance.GetCountryName();
        CntryFlagHandler.instance.CountryChanged.AddListener(FlagChanged);
    }

    void FlagChanged()
    {
        GetComponent<Text>().text = CntryFlagHandler.instance.GetCountryName();
    }

}
