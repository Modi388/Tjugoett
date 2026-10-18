namespace Tjugoett;

public class Deck
{
    private readonly List<Card> _cards = new();
    private readonly Random _random;
    private readonly int _deckCount;
    private int _cutCard;

    public Deck(Random random, int deckCount = 1)
    {
        //(fältet = parametern)
        _random = random;
        _deckCount = deckCount;
        BuildCards();
        Shuffle();
        PlaceCutCard();
    }

    public int Remaining => _cards.Count;
    public bool NeedsReshuffle => Remaining <= _cutCard;

    private void Shuffle()
    {
        for (int i = _cards.Count - 1; i > 0; i--)
        {
            int j = _random.Next(i + 1);
            (_cards[i], _cards[j]) = (_cards[j], _cards[i]);
        }
    }

    private void PlaceCutCard()
    {
        int quarter = _cards.Count / 4;
        _cutCard = _random.Next(1, quarter + 1);
    }

    public Card Draw()
    {
        Card card = _cards[_cards.Count - 1];
        _cards.RemoveAt(_cards.Count - 1);
        return card;
    }

    private void BuildCards()
    {
        for (int i = 0; i <_deckCount; i++)
        {
            foreach (Suit suit in Enum.GetValues<Suit>())
            {
                foreach (Rank rank in Enum.GetValues<Rank>())

                    _cards.Add(new Card(suit, rank));
            }
        }
    }

    public void Reset()
    {
        _cards.Clear();
        BuildCards();
        Shuffle();
        PlaceCutCard();
    }

}

