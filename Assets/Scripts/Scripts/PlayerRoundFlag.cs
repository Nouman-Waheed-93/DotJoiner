using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRoundFlag : MonoBehaviour {

    private void Start()
    {
        GetComponent<Image>().sprite = CntryFlagHandler.instance.GetRoundFlag();
        CntryFlagHandler.instance.CountryChanged.AddListener(FlagChanged);
    }

    void FlagChanged()
    {
        GetComponent<Image>().sprite = CntryFlagHandler.instance.GetRoundFlag();
    }

}
