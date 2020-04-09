using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatoryMenu : MonoBehaviour {
    
 //   public RectTransform[] items;
    public float ShiftingDelay;
    public Vector2[] itemPositions;
    public Vector2 selectedSize;
    public Vector2 unselectedSize;
    Animator anim;
    int IndCurrSlctdItem;
    int moveLeftId, moveRightId;
    float dragStartPosX;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        moveLeftId = Animator.StringToHash("MoveLeft");
        moveRightId = Animator.StringToHash("MoveRight");
    }
	
    public void CycleItem(int dir) {
     
        if (dir > 0)
        {
            anim.SetTrigger(moveRightId);
        }
        else if (dir < 0)
        {
            anim.SetTrigger(moveLeftId);
        }
    }
    
    public void ChangeHierarchyReverse()
    {
        transform.GetChild(transform.childCount - 1).SetSiblingIndex(0);
    }

    public void ChangeHierarchy() {
        transform.GetChild(0).SetSiblingIndex(transform.childCount - 1);
    }
    
    public void DragStart() {
        dragStartPosX = Input.mousePosition.x;
    }

    public void DragEnd() {
        if (Input.mousePosition.x > dragStartPosX)
            CycleItem(-1);
        else if (Input.mousePosition.x < dragStartPosX)
            CycleItem(1);
    }
    
}
