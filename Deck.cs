namespace UnoGame;

public class Deck : List<Card>
{
    private static readonly Random _random = new();
    public Deck()
    {
        for (int color = 0; color < 4; color++)
        {
            for (int number = 0; number < 10; number++)
            {
                Add(new(number.ToString(), (CardColor)color));
                if (number > 0)
                {
                    Add(new(number.ToString(), (CardColor)color));
                }
            }
            for (int count = 0; count < 2; count++)
            {
                Add(new("R", (CardColor)color, CardType.Reverse));
                Add(new("S", (CardColor)color, CardType.Skip));
                Add(new("D2", (CardColor)color, CardType.DrawTwo));
            }
        }
        for (int count = 0; count < 4; count++)
        {
            Add(new("W", CardColor.Black, CardType.Wild));
            Add(new("D4", CardColor.Black, CardType.DrawFour));
        }
    }

    public Card DrawCard()
    {
        Card drawCard = this[^1]; // this[Count - 1]
        Remove(drawCard);
        return drawCard;
    }

    public void Shuffle()
    {
        for (int cardIndex = 0; cardIndex < Count; cardIndex++)
        {
            int randomIndex = _random.Next(0, Count);
            (this[cardIndex], this[randomIndex]) = (this[randomIndex], this[cardIndex]);
        }
    }
}