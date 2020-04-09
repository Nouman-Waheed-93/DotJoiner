using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PointsHandler : MonoBehaviour {

    public static PointsHandler instance;

    public static UnityEvent onPointsAdded = new UnityEvent();

    private void Start()
    {
        instance = this;
    }

    public static void AddPoints(int points)
    {
        PlayerPrefs.SetInt("Points", GetPoints() + points);
        onPointsAdded.Invoke();
    }

    public static int GetPoints() {
        return PlayerPrefs.GetInt("Points", 0);
    }

}
