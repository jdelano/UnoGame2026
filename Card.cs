using System.Drawing;

namespace UnoGame;

public enum CardColor
{
    Red, Yellow, Blue, Green, Black
}

public enum CardType
{
    Face, Reverse, Skip, DrawTwo, DrawFour, Wild
}

public class Card
{
    public string Number { get; private set; }
    public CardColor CardColor { get; set; }
    public CardColor ChosenColor { get; set; }
    public CardType Type { get; set; }

    public bool IsWildCard
    {
        get
        {
            return Type == CardType.Wild || Type == CardType.DrawFour;
        }
    }

    public CardColor EffectiveColor
    {
        get
        {
            if (!IsWildCard)
            {
                return CardColor;
            }
            else if (ChosenColor == CardColor.Black)
            {
                return CardColor;
            }
            else
            {
                return ChosenColor;
            }
        }
    }

    public Card(string number, CardColor color, CardType type)
    {
        Number = number;
        CardColor = color;
        ChosenColor = color;
        Type = type;
    }

    public Card(string number, CardColor color) : this(number, color, CardType.Face)
    {
        
    }

    public bool Matches(Card other)
    {
        if ((this.IsWildCard && this.EffectiveColor == CardColor.Black)
            || (other.IsWildCard && other.EffectiveColor == CardColor.Black))
        {
            return true;
        }
        else if (this.CardColor == other.CardColor || this.Number == other.Number)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override string ToString()
    {
        return $"{EffectiveColor}-{Number}";
    }

    public void UpdateChosenColor()
    {
        if (IsWildCard)
        {
            Console.Write("Please specify a new color: 0=Red, 1=Yellow, 2=Blue, 3=Green");
            int response = int.Parse(Console.ReadLine());
            ChosenColor = (CardColor)response;
        }
    }

}