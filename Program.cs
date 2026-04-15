using UnoGame;

Deck deck = new();
Console.Write(deck.Count);
deck.Shuffle();
foreach (var card in deck)
{
    Console.WriteLine(card);
}
