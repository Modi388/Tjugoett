namespace Tjugoett;

public class Deck
{
    private readonly List<Card> _cards = new();

    private readonly Random _random;
    public Deck(Random random)
    {
        //(fältet = parametern)
        _random = random;
        BuildCards();
        Shuffle();

    }

    public int Remaining => _cards.Count;
    public bool NeedsReshuffle => Remaining < 15;

    private void Shuffle()
    {
        for (int i = _cards.Count - 1; i > 0; i--)
        {
            int j = _random.Next(i + 1);
            (_cards[i], _cards[j]) = (_cards[j], _cards[i]);
        }
    }
    public Card Draw()
    {
        Card card = _cards[_cards.Count - 1];
        _cards.RemoveAt(_cards.Count - 1);
        return card;
    }

    private void BuildCards()
    {
        foreach (Suit suit in Enum.GetValues<Suit>())
        {
            foreach (Rank rank in Enum.GetValues<Rank>())

                _cards.Add(new Card(suit, rank));
        }
    }

    public void Reset()
    {
        _cards.Clear();
        BuildCards();
        Shuffle();
    }

}

