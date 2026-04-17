namespace UnoGame;

public enum Direction
{
    Left, Right
}

public class Game
{
    private bool isRoundOver = false;
    private bool isGameOver = false;
    private int currentPlayerIndex = 0;
    private Direction gameDirection;

    private int numberOfPlayers;
    private Player CurrentPlayer
    {
        get
        {
            return Players[currentPlayerIndex];
        }
    }

    public List<Player> Players { get; set; }
    public DiscardPile DiscardPile { get; set; }
    public Deck Deck { get; set; }
    
}