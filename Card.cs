namespace Tjugoett;
// spelkort. 
public class Card
{
    // bara get
    public Suit Suit { get; }
    public Rank Rank { get; }

    // slås upp i tabellen
    public int Points
    {
        get
        {
            return pointDictionary[Rank];
        }
    }


    private static readonly Dictionary<Rank, int> pointDictionary = new()
    {
        [Rank.Two] = 2,
        [Rank.Three] = 3,
        [Rank.Four] = 4,
        [Rank.Five] = 5,
        [Rank.Six] = 6,
        [Rank.Seven] = 7,
        [Rank.Eight] = 8,
        [Rank.Nine] = 9,
        [Rank.Ten] = 10,
        [Rank.Jack] = 10,
        [Rank.Queen] = 10,
        [Rank.King] = 10,
        [Rank.Ace] = 11
    };

    // Konstruktor
    public Card(Suit suit, Rank rank)
    {
        Suit = suit;
        Rank = rank;
    }

    public override string ToString()
    {
        return Suit.ToString() + Rank.ToString();
    }

}