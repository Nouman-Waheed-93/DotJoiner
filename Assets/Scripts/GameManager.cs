using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public int perBoxPoints = 10;
    
    public Vector2 gridSize;

    public Transform nameLabels;

    public Text opponentNameText;

    public YourTurnMsg playerTurnMsg;

    int currGameReward;

    Player currentPlayer, localHumanPlayer, opponentPlayer;

    public Player CurrentPlayer {
        set {
            currentPlayer = value;
        }
    }

    int boxesMade, totalBoxes;

    IGameType matchMode;

    public IGameType MatchMode {
        get {
            return matchMode;
        }
    }
    
    public void StartOfflineGame(int reward) {
        currGameReward = reward;
        matchMode = new OfflineGame();
        PrepareTheGround();
    }

    public void StartOnlineGame(int reward) {
        currGameReward = reward;
        matchMode = new NetworkGame();
        PrepareTheGround();
     }

    public void LoadOpponentName(string name)
    {
        opponentNameText.text = name;
        opponentPlayer.name = name;
    }

    public void CurrentPlayerMadeABox(Dot boxLRDot)
    {
        boxLRDot.PlayerLetterText.text = currentPlayer.name.Substring(0, 1);
        currentPlayer.score += perBoxPoints;
        boxesMade++;
        Debug.Log("Boxes made " + boxesMade + " total boxes " + totalBoxes);
        ScorePool.instance.SpawnScoreAtPosition(boxLRDot.PlayerLetterText.transform.position);
        SoundHandler.instance.PlayerBoxMade();
    }

    public bool HaveAllBoxesBeenMade()
    {
        return boxesMade >= totalBoxes;
    }


    public virtual IEnumerator GetPlayerMove()
    {
        Connection move = null;
        do
        {
            yield return null;
            move = currentPlayer.MakeMove();
        } while (move == null /*&& Timer.instance.TickTimer()*/); // Is the player still playing its turn? has the timer run out?
        
        Timer.instance.Reset();
        
        if (!CheckBoxesMadeThisMove(move))
            TogglePlayerTurn(); //otherPlayer's turn

        if (HaveAllBoxesBeenMade())
            DeclareWinner();
        else
            StartCoroutine(GetPlayerMove());
        
    }

    public virtual void DeclareWinner()
    {
        PointsHandler.AddPoints(localHumanPlayer.score);
        if (localHumanPlayer.score > opponentPlayer.score)
        {
            PlayerProfile.instance.GameWon();
            NMenus.InGameMenu.instance.ShowResult(localHumanPlayer.name, currGameReward.ToString());
            CoinHandler.instance.makeCoinTransaction(currGameReward);
            SoundHandler.instance.PlayerWon();
        }
        else if (localHumanPlayer.score < opponentPlayer.score)
        {
            NMenus.InGameMenu.instance.MissionFailed();
            SoundHandler.instance.PlayerLost();
        }
        else
        {
            NMenus.InGameMenu.instance.DrawGame();
            SoundHandler.instance.PlayerLost();
        }
  //      EndGame();
    }

    public void OpponentLeft()
    {
        PointsHandler.AddPoints(localHumanPlayer.score);
        PlayerProfile.instance.GameWon();
        NMenus.InGameMenu.instance.ShowResult(localHumanPlayer.name, currGameReward.ToString());
        CoinHandler.instance.makeCoinTransaction(currGameReward);
        SoundHandler.instance.PlayerWon();
    }

    public virtual void TogglePlayerTurn()
    {
        if (currentPlayer == localHumanPlayer)
            currentPlayer = opponentPlayer;
        else
        {
            currentPlayer = localHumanPlayer;
        }
    }

    public void ShowYourTurnMsg()
    {
        playerTurnMsg.ShowMessage();
    }

    public bool CheckBoxesMadeThisMove(Connection move)
    {
        bool boxesMade = false;

        if (LineDotAlgorithms.IsAHorizontalLine(move))
        {
            if (LineDotAlgorithms.CheckUpperLeftBoxMade(move.RightDownDot))
            {
                CurrentPlayerMadeABox(move.RightDownDot);
                boxesMade = true;
            }
            if (move.RightDownDot.down != null && LineDotAlgorithms.CheckUpperLeftBoxMade(move.RightDownDot.down.RightDownDot))
            {
                CurrentPlayerMadeABox(move.RightDownDot.down.RightDownDot);
                boxesMade = true;
            }
        }
        else if (LineDotAlgorithms.IsAVerticalLine(move))
        {
            if (LineDotAlgorithms.CheckUpperLeftBoxMade(move.RightDownDot))
            {
                CurrentPlayerMadeABox(move.RightDownDot);
                boxesMade = true;
            }
            if (move.RightDownDot.right != null && LineDotAlgorithms.CheckUpperLeftBoxMade(move.RightDownDot.right.RightDownDot))
            {
                CurrentPlayerMadeABox(move.RightDownDot.right.RightDownDot);
                boxesMade = true;
            }
        }
        return boxesMade;
    }

    void PrepareTheGround() {
        PlayerProfile.instance.GameStarted();
        LineDotAlgorithms.CreateGrid(gridSize, transform, nameLabels);
        totalBoxes = (int)((gridSize.x - 1) * (gridSize.y - 1));
        boxesMade = 0;
        localHumanPlayer = matchMode.CreatePlayer();
        opponentPlayer = matchMode.CreateOpponent();
        opponentNameText.text = opponentPlayer.name;
        
        DecideFirstTurn();
    }
    
    void DecideFirstTurn()
    {
        Debug.Log("Deciding first turn");
        if (matchMode.HaveAuthority())
        {
            Debug.Log("Have authority dalay");
            currentPlayer = Random.Range(1, 3) == 1 ? localHumanPlayer : opponentPlayer;
            StartCoroutine(GetPlayerMove());
        }
        Debug.Log("Below Authority Check");
    }

    public void CreateSingleton () {
        instance = this;
    }

    public void GiveMessage(int messageId) {
        localHumanPlayer.GiveMessage(messageId);
    }

    public void EndGame() {
        matchMode.EndGame();
        int childInd = transform.childCount - 1;
        for (; childInd >= 0; childInd--) {
            Destroy(transform.GetChild(childInd).gameObject);
        }
        childInd = nameLabels.childCount - 1;
        for (; childInd >= 0; childInd--) {
            Destroy(nameLabels.GetChild(childInd).gameObject);
        }
        VisualLineHandler.instance.DestroyAllLines();
        matchMode = null;
    }
}
