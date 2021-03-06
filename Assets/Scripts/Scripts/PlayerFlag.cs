﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFlag : MonoBehaviour {

    private void Start()
    {
        GetComponent<Image>().sprite = CntryFlagHandler.instance.GetFlag();
        CntryFlagHandler.instance.CountryChanged.AddListener(FlagChanged);
    }

    void FlagChanged()
    {
        GetComponent<Image>().sprite = CntryFlagHandler.instance.GetFlag();
    }

}
