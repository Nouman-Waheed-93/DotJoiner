using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPoints : MonoBehaviour {

    private void Start()
    {
        GetComponent<Text>().text = PointsHandler.GetPoints().ToString();
        PointsHandler.onPointsAdded.AddListener(OnPointsAdded);
    }

    void OnPointsAdded()
    {
        GetComponent<Text>().text = PointsHandler.GetPoints().ToString();
    }

}
