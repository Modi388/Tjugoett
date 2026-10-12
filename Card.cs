namespace Tjugoett;

public class Card
{
    // property
    public Suit Suit { get; }
    public Rank Rank { get; }

    // Konstruktor
    public Card(Suit suit, Rank rank)
    {
        Suit = suit;
        Rank = rank;
    }
}