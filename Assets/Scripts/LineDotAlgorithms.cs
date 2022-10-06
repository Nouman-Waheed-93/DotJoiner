using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class LineDotAlgorithms {

    // static int totalBoxes, boxesMade;
    static Dot UpperMostLeftMostDot;
    
    public static Dot FirstDotInTheGrid{
        get{
        return UpperMostLeftMostDot;
        }
    }

    public static void CreateGrid(Vector2 gridSize, Transform dotParent, Transform NameLabelParent)
    {
      Dot[,] dotGrid = new Dot[(int)gridSize.x, (int)gridSize.y];
        Dot templateDot = Resources.Load<Dot>("Dot");
        Text templateText = Resources.Load<Text>("NameLabel");
        int ConnIDNumber = 0; //Number to give identity to connections
        for (int y = 0; y < gridSize.y; y++)
            for (int x = 0; x < gridSize.x; x++)
            {
                dotGrid[x, y] = MonoBehaviour.Instantiate(templateDot, dotParent);
                if (x > 0)
                {
                    dotGrid[x - 1, y].right = dotGrid[x, y].left = new Connection(ConnIDNumber++, dotGrid[x - 1, y], dotGrid[x, y]);
                    if (y > 0)
                        dotGrid[x, y].PlayerLetterText =
                            MonoBehaviour.Instantiate(templateText, NameLabelParent);
                }
                if (y > 0)
                {
                    dotGrid[x, y - 1].down = dotGrid[x, y].up = new Connection(ConnIDNumber++, dotGrid[x, y - 1], dotGrid[x, y]);
                }
            }
        UpperMostLeftMostDot = dotGrid[0, 0];
    }

    public static Connection FindConnectionWithID(Dot dot, int ID) {
        Connection conn = null;

        if (dot.right != null && dot.right.IDNumber == ID)
            return dot.right;
        if (dot.down != null && dot.down.IDNumber == ID)
            return dot.down;

        if (dot.left == null && dot.down != null)
            conn = FindConnectionWithID(dot.down.RightDownDot, ID);
        if (conn != null)
            return conn;
        if (dot.right != null)
            conn = FindConnectionWithID(dot.right.RightDownDot, ID);
        
        return conn;
    }

    public static bool IsAHorizontalLine(Connection line)
    {
        return (line.LeftUpDot.right != null && line.LeftUpDot.right == line);
    }

    public static bool IsAVerticalLine(Connection line)
    {
        return (line.LeftUpDot.down != null && line.LeftUpDot.down == line);
    }
    
    static List<Dot> dotsVisited = new List<Dot>();
    static List<Connection> movesMadeInChain = new List<Connection>();


    public static bool CheckUpperLeftBoxMade(Dot dot) {
        return (dot.left != null && dot.up != null && dot.left.IsConnected()
            && dot.left.LeftUpDot.up.IsConnected() && dot.up.IsConnected()
                && dot.up.LeftUpDot.left.IsConnected());
    }

    public static Move FindBestMove(Dot dot) {
        Move bestMoveInRightSide = null;
        if (dot.right != null) {
            Move RightSideMove = null;
            if (!dot.right.IsConnected()) {
                RightSideMove = new Move();
                RightSideMove.connection = dot.right;
                CalculateMoveScore(RightSideMove);
            }
            Move tempMoveInRight = FindBestMove(dot.right.RightDownDot);
            if (RightSideMove != null && tempMoveInRight != null)
                bestMoveInRightSide = RightSideMove.points > tempMoveInRight.points ? RightSideMove : tempMoveInRight;
            else if (RightSideMove != null)
                bestMoveInRightSide = RightSideMove;
            else if (tempMoveInRight != null)
                bestMoveInRightSide = tempMoveInRight;
        }

        Move bestMoveInDownSide = null;
        if (dot.down != null) {
            Move DownSideMove = null;
            if (!dot.down.IsConnected()) {
                DownSideMove = new Move();
                DownSideMove.connection = dot.down;
                CalculateMoveScore(DownSideMove);
            }
            Move tempMoveInDown = dot.left == null ? FindBestMove(dot.down.RightDownDot) : null;
            if (DownSideMove != null && tempMoveInDown != null)
                bestMoveInDownSide = DownSideMove.points > tempMoveInDown.points ? DownSideMove : tempMoveInDown;
            else if (DownSideMove != null)
                bestMoveInDownSide = DownSideMove;
            else if (tempMoveInDown != null)
                bestMoveInDownSide = tempMoveInDown;
        }

        if (bestMoveInRightSide != null && bestMoveInDownSide != null)
        {
            if (bestMoveInDownSide.points > bestMoveInRightSide.points)
                return bestMoveInDownSide;
            else if (bestMoveInDownSide.points < bestMoveInRightSide.points)
                return bestMoveInRightSide;
            else
                return Random.Range(0, 2) == 0 ? bestMoveInDownSide : bestMoveInRightSide;
        }
        else if (bestMoveInRightSide != null)
            return bestMoveInRightSide;
        else if (bestMoveInDownSide != null)
            return bestMoveInDownSide;
        return null;
    }

    public static Move GetARandomMove(Dot dot)
    {
        Move rightSideMove = null;
        if (dot.right != null)
        {
            Move move1 = null;
            if (!dot.right.IsConnected())
            {
                move1 = new Move();
                move1.connection = dot.right;
            }
            Move move2 = GetARandomMove(dot.right.RightDownDot);
            if (move1 != null && move2 != null)
                rightSideMove = Random.Range(0, 2) == 0 ? move1 : move2;
            else if (move1 != null)
                rightSideMove = move1;
            else if (move2 != null)
                rightSideMove = move2;
        }

        Move downSideMove = null;
        if (dot.down != null)
        {
            Move move1 = null;
            if (!dot.down.IsConnected())
            {
                move1 = new Move();
                move1.connection = dot.down;
            }
            Move move2 = GetARandomMove(dot.down.RightDownDot);
            if (move1 != null && move2 != null)
                downSideMove = Random.Range(0, 2) == 0 ? move1 : move2;
            else if (move1 != null)
                downSideMove = move1;
            else if (move2 != null)
                downSideMove = move2;
        }

        if (rightSideMove != null && downSideMove != null)
        {
            return Random.Range(0, 2) == 0 ? downSideMove : rightSideMove;
        }
        else if (rightSideMove != null)
            return rightSideMove;
        else if (downSideMove != null)
            return downSideMove;
        return null;
    }

    public static Connection GetOpponentBestMove(Connection move, Dot BRDot) {
        if (BRDot.left != null && !BRDot.left.IsConnected() && !movesMadeInChain.Contains(BRDot.left) && BRDot.left != move)
            return BRDot.left;
        if (BRDot.up != null && !BRDot.up.IsConnected() && !movesMadeInChain.Contains(BRDot.up) && BRDot.up != move)
            return BRDot.up;
        if (BRDot.up != null && BRDot.up.LeftUpDot.left != null && !movesMadeInChain.Contains(BRDot.up.LeftUpDot.left) &&
            !BRDot.up.LeftUpDot.left.IsConnected() && BRDot.up.LeftUpDot.left != move)
            return BRDot.up.LeftUpDot.left;
        if (BRDot.left != null && BRDot.left.LeftUpDot.up != null && !movesMadeInChain.Contains(BRDot.left.LeftUpDot.up) &&
            !BRDot.left.LeftUpDot.up.IsConnected() && BRDot.left.LeftUpDot.up != move)
            return BRDot.left.LeftUpDot.up;
        return null;
    }

    public static void CalculateMoveScore(Move move) {

        dotsVisited.Clear();
        movesMadeInChain.Clear();
        
        if (move.connection != null)
        {
            GivePointsToMove(move.connection.RightDownDot, move);
            if (IsAHorizontalLine(move.connection) && move.connection.RightDownDot.down != null)
            {
                GivePointsToMove(move.connection.RightDownDot.down.RightDownDot, move);
            }
            else if (IsAVerticalLine(move.connection) && move.connection.RightDownDot.right != null)
            {
                GivePointsToMove(move.connection.RightDownDot.right.RightDownDot, move);
            }
        }

        dotsVisited.Clear();
        movesMadeInChain.Clear();

    }
    
    public static int CalculateChain(Dot PreviousBRDot, Connection moveCon) {
        int pointsInChain = 0;

        if (moveCon == null)
            return 0;
        
        Dot BRDotToCheckInMove = moveCon.RightDownDot;
        bool doubleProcessDone = false;

    ProcessDot:
        if (BRDotToCheckInMove != PreviousBRDot)
            if (!dotsVisited.Contains(BRDotToCheckInMove)){
                if(NumberOfLinesConnectedOnUpperLeftBox(BRDotToCheckInMove) == 2) {
                    pointsInChain += CheckPointsInBox(moveCon, BRDotToCheckInMove);
                }
                dotsVisited.Add(BRDotToCheckInMove);
            }
            else if (NumberOfLinesConnectedOnUpperLeftBox(BRDotToCheckInMove) == 1)
                pointsInChain += CheckPointsInBox(moveCon, BRDotToCheckInMove);


        if (IsAHorizontalLine(moveCon) && !doubleProcessDone && moveCon.RightDownDot.down != null) {
            BRDotToCheckInMove = moveCon.RightDownDot.down.RightDownDot;
            doubleProcessDone = true;
            goto ProcessDot;
        }
        else if (IsAVerticalLine(moveCon) && !doubleProcessDone && moveCon.RightDownDot.right != null) {
            BRDotToCheckInMove = moveCon.RightDownDot.right.RightDownDot;
            doubleProcessDone = true;
            goto ProcessDot;
        }
        
        return pointsInChain;
    }

    static void GivePointsToMove(Dot BRDOT, Move move)
    {
        switch (NumberOfLinesConnectedOnUpperLeftBox(BRDOT))
        {
            case 2:
                if (move.points < 1)
                    move.points -= CalculateChain(null, move.connection);
                else
                    move.points += 1;
                break;
            case 3:
                if (move.points <= 0)
                    move.points = 1;
                else
                    move.points += 1;
                break;
            default:
                break;
        }
    }

    static int CheckPointsInBox(Connection conn, Dot BRDotInMove) {
        int pointsInMove = 0;
        Connection opponentMove = GetOpponentBestMove(conn, BRDotInMove);
        movesMadeInChain.Add(conn);
        pointsInMove = CalculateChain(BRDotInMove, opponentMove) + 1;
        return pointsInMove;
    }

    public static int NumberOfLinesConnectedOnUpperLeftBox(Dot dot) {
        int numberOfLines = 0;
        if (dot.left != null && dot.left.IsConnected())
            numberOfLines++;
        if (dot.left != null && dot.left.LeftUpDot.up != null && dot.left.LeftUpDot.up.IsConnected())
            numberOfLines++;
        if (dot.up != null && dot.up.IsConnected())
            numberOfLines++;
        if (dot.up != null && dot.up.LeftUpDot.left != null && dot.up.LeftUpDot.left.IsConnected())
            numberOfLines++;
        return numberOfLines;
    }

}
