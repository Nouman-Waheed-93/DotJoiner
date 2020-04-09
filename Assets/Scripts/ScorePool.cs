using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePool : MonoBehaviour {

    public static ScorePool instance;

    public Vector3 positionOffset;

    public int startingSize = 3;

    List<ScoreText> scores = new List<ScoreText>();
    int currInd;

    public void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }
        instance = this;
        for (int i = 0; i < startingSize; i++) {
            CreateNAddNewScore();
        }
    }

    public void SpawnScoreAtPosition(Vector3 position) {
        if (scores[currInd].gameObject.activeSelf)
        {
            CreateNAddNewScore();
            currInd++;
            scores[currInd].StartMovingHere();
        }
        scores[currInd].transform.position = position + positionOffset;
        scores[currInd].StartMovingHere();
        currInd++;
        if (currInd >= scores.Count) {
            currInd = 0;
        }
    }

    void CreateNAddNewScore() {
        scores.Add(Instantiate(Resources.Load<ScoreText>("Score"),transform));
    }

}
