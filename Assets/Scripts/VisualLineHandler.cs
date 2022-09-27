using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VisualLineHandler : MonoBehaviour {

    public static VisualLineHandler instance;
    
    Dot FirstDot;

    Image Templateline;
    Image currLine;

    GameObject prevLine;

    public Transform referenceElement;

    GraphicRaycaster m_raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;

    private void Start()
    {
        instance = this;
        m_raycaster = GetComponentInParent<GraphicRaycaster>();
        m_EventSystem = GetComponentInParent<EventSystem>();
        Templateline = Resources.Load<Image>("Line");
        Input.multiTouchEnabled = false;
    }

    public void DestroyAllLines() {
        int childInd = transform.childCount - 1;
        for (; childInd >= 0; childInd--) {
            if (transform.GetChild(childInd) != referenceElement)
                Destroy(transform.GetChild(childInd).gameObject);
        }
    }

    //Should be called per frame
    public Connection GetHumanPlayerMove()
    {
        if (Input.GetMouseButtonDown(0)) {
            Dot dotUnderPointer = GetTheDotUnderPointer();
            if (dotUnderPointer != null)
                SetTouchStartDot(dotUnderPointer);
        }
        else if (Input.GetMouseButtonUp(0))
        {        
            Dot dotUnderPointer = GetTheDotUnderPointer();
            if (dotUnderPointer != null)
                return SetSecondPosition(dotUnderPointer);
            
            if (currLine != null)
            {
                Destroy(currLine.gameObject);
            }
            
            FirstDot = null;
        }
        else if (FirstDot != null)
            DisplayLine();
        return null;
    }

    Dot GetTheDotUnderPointer() {
        m_PointerEventData = new PointerEventData(m_EventSystem);
        //Set the Pointer Event Position to that of the mouse position
        m_PointerEventData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();

        m_raycaster.Raycast(m_PointerEventData, results);
        Dot dot = null;
        foreach (RaycastResult result in results)
        {
            dot = result.gameObject.GetComponent<Dot>();
            if (dot != null)
                break;
        }
        return dot;
    }

    public void SetTouchStartDot(Dot dot) {
        FirstDot = dot;
        if (FirstDot != null)
        {
            CreateVisualLineOnPivot(dot);
        }
    }
    
    public Connection SetSecondPosition(Dot dot) {
        if (dot != null && FirstDot != null) {
            return CreateLine(FirstDot, dot);
        }
        return null;
    }
    
    public void DisplayLine() {
        referenceElement.position = Input.mousePosition;
        currLine.rectTransform.sizeDelta = 
            new Vector2(Vector3.Distance(referenceElement.GetComponent<RectTransform>().anchoredPosition, 
            FirstDot.GetComponent<RectTransform>().anchoredPosition), currLine.rectTransform.sizeDelta.y);
        currLine.rectTransform.rotation =
            Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.right, Input.mousePosition - FirstDot.transform.position));
    }

    void CreateVisualLineOnPivot(Dot pivot) {
        currLine = Instantiate(Templateline, transform);
        currLine.transform.position = FirstDot.transform.position;
    }

    public Connection CreateLine(Dot FirstDot, Dot SecondDot) {
        //  bool MoveSuccessful = true;
        Connection connectionMade = null;
        this.FirstDot = FirstDot;
        if (currLine != null)
            Destroy(currLine.gameObject);

        if (prevLine != null)
            prevLine.GetComponent<Animator>().SetBool("blink", false);

        CreateVisualLineOnPivot(FirstDot);
        
        currLine.rectTransform.sizeDelta =
          new Vector2(Vector2.Distance(FirstDot.GetComponent<RectTransform>().anchoredPosition,
          SecondDot.GetComponent<RectTransform>().anchoredPosition), currLine.rectTransform.sizeDelta.y);

        if (FirstDot.left != null && FirstDot.left.LeftUpDot == SecondDot && !FirstDot.left.IsConnected())
        {
            connectionMade = FirstDot.left;
            FirstDot.left.Connect();
            currLine.rectTransform.rotation = Quaternion.Euler(0, 0, 180);
        }
        else if (FirstDot.right != null && FirstDot.right.RightDownDot == SecondDot && !FirstDot.right.IsConnected())
        {
            connectionMade = FirstDot.right;
            FirstDot.right.Connect();
            currLine.rectTransform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (FirstDot.up != null && FirstDot.up.LeftUpDot == SecondDot && !FirstDot.up.IsConnected())
        {
            connectionMade = FirstDot.up;
            FirstDot.up.Connect();
            currLine.rectTransform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (FirstDot.down != null && FirstDot.down.RightDownDot == SecondDot && !FirstDot.down.IsConnected())
        {
            connectionMade = FirstDot.down;
            FirstDot.down.Connect();
            currLine.rectTransform.rotation = Quaternion.Euler(0, 0, 270);
        }
        else
        {
            Destroy(currLine);
        }

        if (currLine != null)
        {
            currLine.GetComponent<Animator>().SetBool("blink", true);
            SoundHandler.instance.LineMade();
        }
        prevLine = currLine.gameObject;
        currLine = null;
        this.FirstDot = null;
        return connectionMade;
    }

}
