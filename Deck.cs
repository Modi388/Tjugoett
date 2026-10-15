namespace Tjugoett;

public class Deck
{
    private readonly List<Card> _cards = new();

    private readonly Random _random;
    public Deck(Random random)
    {
        //(fältet = parametern)
        _random = random;

        foreach (Suit suit in Enum.GetValues<Suit>())
        {
            foreach (Rank rank in Enum.GetValues<Rank>())

                _cards.Add(new Card(suit,rank));
    }
    }
    public int Remaining => _cards.Count;



}