namespace UnoGame;

public enum Direction
{
    Left, Right
}

public class Game
{
    private bool isRoundOver = false;
    private const int NUMBER_OF_CARDS_IN_HAND = 7;
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

    public Game()
    {
        Players = [];
        DiscardPile = [];
        Deck = [];
    }

    public void Run()
    {
        InitializeGame();
        do
        {
            InitializeRound();
            do
            {
                ProcessInput();
                RenderOutput();
            } while (!isRoundOver);
            Console.WriteLine($"Congratulations! Player {currentPlayerIndex} Wins!");
            Console.WriteLine("Press Q to quit or any other key to try another round...");
            var keyInfo = Console.ReadKey();
            if (keyInfo.Key == ConsoleKey.Q)
            {
                isGameOver = true;
            }
        } while (!isGameOver);
    }

    private void InitializeGame()
    {
        Console.Write("Enter the number of players: ");
        numberOfPlayers = int.Parse(Console.ReadLine());
        for (int i = 0; i < numberOfPlayers; i++)
        {
            Players.Add(new Player());
        }
        isGameOver = false;
    }

    private void InitializeRound()
    {
        currentPlayerIndex = 0;
        isRoundOver = false;
        Deck.Shuffle();
        DealCards();
        DealCardToDiscardPile();
        gameDirection = Direction.Left;
        RenderOutput();
    }



    private void ProcessInput()
    {
        bool validCard = false;
        Card card;
        do
        {
            Console.Write("Which card would you like to play (enter D to draw a card)? ");
            string response = Console.ReadLine();
            if (response.ToUpper() == "D")
            {
                card = Deck.DrawCard();
                CurrentPlayer.ReceiveCard(card);
                Console.WriteLine($"\n\nYou drew the {card} card.");
            }
            else
            {
                if (!int.TryParse(response, out int index) || index < 0 || index >= CurrentPlayer.CardsInHand)
                {
                    Console.WriteLine("Invalid selection. Please try again.");
                    continue;
                }
                else
                {
                    card = CurrentPlayer.Hand[index];
                    Console.WriteLine($"\n\nYou selected the {card} card");
                }
            }
        } while (!validCard);
    }

    private void RenderOutput()
    {
        Console.Clear();
        DiscardPile.Display();
        CurrentPlayer.Display();
    }

    private void DealCardToDiscardPile()
    {
        DiscardPile.Add(Deck.DrawCard());
    }

    private void DealCards()
    {
        for (int i = 0; i < NUMBER_OF_CARDS_IN_HAND; i++)
        {
            foreach (var player in Players)
            {
                player.ReceiveCard(Deck.DrawCard());
            }
        }
    }

    public bool PlayCard(Player currentPlayer, Card card)
    {
        if (DiscardPile.TopCard.Matches(card))
        {
            currentPlayer.RemoveCard(card);
            DiscardPile.Add(card);
            if (currentPlayer.CardsInHand == 0)
            {
                isRoundOver = true;
            }
            else
            {
                Action<Card> action = GetActionForCard(card);
                action(card);
            }
            return true;
        }
        else
        {
            Console.WriteLine("\nThe card selected/drawn does not match the top card on the discard pile. It remains in your hand. Please try again.");
            return false;
        }
    }

    private Action<Card> GetActionForCard(Card card)
    {
        // Face, Reverse, Skip, DrawTwo, DrawFour, Wild

        switch (card.Type)
        {
            case CardType.Skip:
                return c => SetNextPlayer(2);
            case CardType.Reverse:
                return c => 
                { 
                    ReverseDirection();
                    SetNextPlayer(1);
                };
            case CardType.DrawTwo:

        }
    }

    private void ReverseDirection()
    {
        gameDirection = (gameDirection == Direction.Left) ? Direction.Right : Direction.Left;
    }

    private void SetNextPlayer(int skipCount = 1)
    {
        int direction = (gameDirection == Direction.Left) ? -1 : 1;
        currentPlayerIndex = (currentPlayerIndex + direction * skipCount + Players.Count) % Players.Count;
    }

}