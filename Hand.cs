namespace UnoGame;

public class Hand : List<Card>
{
    public void Display()
    {
        for (int i = 0; i < Count; i++)
        {
            Console.WriteLine($"{i}: {this[i]}");
        }
        Console.WriteLine("D: Draw a card from the deck.");
    }
}