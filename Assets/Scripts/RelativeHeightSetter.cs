using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelativeHeightSetter : MonoBehaviour
{
    [SerializeField]
    RectTransform relativeTarget;

    // Start is called before the first frame update
    void Start()
    {
        AdjustHeight();
    }

    private void AdjustHeight()
    {
        RectTransform myRect = (RectTransform)transform;
        relativeTarget.parent = myRect;
        float height = Mathf.Abs(relativeTarget.anchoredPosition.y);
        myRect.sizeDelta = new Vector2(myRect.sizeDelta.x, height);
        ReSizeAndRepositionChilds(myRect, height);
    }

    private void ReSizeAndRepositionChilds(RectTransform myRect, float height)
    {
        for (int i = 0; i < myRect.childCount; i++)
        {
            RectTransform childRect = (RectTransform)myRect.GetChild(i).transform;
            if (Mathf.Abs(childRect.anchoredPosition.y) + childRect.sizeDelta.y > height)
            {
                float childHeight = height * 0.9f;
                float space = height * 0.05f;
                float positionSign = Mathf.Sign(childRect.anchoredPosition.y);
                childRect.sizeDelta = new Vector2(childRect.sizeDelta.x, childHeight);
                childRect.anchoredPosition = new Vector2(childRect.anchoredPosition.x, space * positionSign);
            }
        }
    }
}
