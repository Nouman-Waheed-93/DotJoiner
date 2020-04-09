
public interface IGameType {

    bool HaveAuthority();
    Player CreatePlayer();
    Player CreateOpponent();
    void EndGame();

}

