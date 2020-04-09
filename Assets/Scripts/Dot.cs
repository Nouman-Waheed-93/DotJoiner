using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dot : MonoBehaviour {

    public Connection left;
    public Connection right;
    public Connection up;
    public Connection down;

    public Text PlayerLetterText; // The text componenet that will be used 
                                  //to write the name of the player who made the box

    //public void OndPointerDown()
    //{//Try removing this function. If everything works perfect, the method is not needed.
    //    VisualLineHandler.instance.SetTouchStartDot(this);
    //}

}

public class Connection {
    readonly Dot leftUpDot;
    readonly Dot rightDownDot;
    readonly int ID;

    public int IDNumber {
        get {
            return ID;
        }
    }

    bool connected;

    public void Connect() {
        connected = true;
//        LineDotAlgorithms.CheckBoxMade(this);
    }
    
    public bool IsConnected() {
        return connected;
    }

    public Connection(int ID, Dot leftUpDot, Dot rightDownDot) {
        this.ID = ID;
        this.leftUpDot = leftUpDot;
        this.rightDownDot = rightDownDot;
    }

    public Dot LeftUpDot
    {
        get
        {
            return leftUpDot;
        }
    }

    public Dot RightDownDot {
        get {
            return rightDownDot;
        }
    }
}

public class Move {
    public Connection connection;
    public int points;
}