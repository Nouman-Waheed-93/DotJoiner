using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public float MoveTime = 10;
    float cumMoveTime = 0;

    Image img;

    public static Timer instance;

    private void Start()
    {
        instance = this;
        img = GetComponent<Image>();
    }

    public void Reset()
    {

        cumMoveTime = 0;
        img.fillAmount = 1 - cumMoveTime / MoveTime;

    }

    public bool TickTimer() {
        cumMoveTime += Time.deltaTime;
        img.fillAmount = 1 - cumMoveTime / MoveTime;
        return cumMoveTime < MoveTime;
     }

}
