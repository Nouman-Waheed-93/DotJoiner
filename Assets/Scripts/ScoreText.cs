using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour {

    public AnimationCurve MoveUpSpeed;
    public AnimationCurve FadeOutSpeed;
    public Color startColor;
    public Text text;

    public float lifeTime = 1;

    float time;
    
    public void StartMovingHere()
    {
        time = 0;
        gameObject.SetActive(true);
        StartCoroutine(MoveUpAndFadeAway());
    }

    private void OnDisable()
    {
        gameObject.SetActive(false);
    }

    public IEnumerator MoveUpAndFadeAway() {
        text.text = "+" + GameManager.instance.perBoxPoints;
        text.color = startColor;
        while (time <= lifeTime) {
            time += Time.deltaTime;
            text.rectTransform.anchoredPosition += Vector2.up * MoveUpSpeed.Evaluate(time/ lifeTime);
            text.color = Color.Lerp(text.color, Color.clear, FadeOutSpeed.Evaluate(time/ lifeTime) * Time.deltaTime);
            yield return null;
        }
        gameObject.SetActive(false);
    }

}
