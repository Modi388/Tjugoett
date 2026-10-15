namespace Tjugoett;

public class Deck
{
    private readonly List<Card> _cards = new(); 

    private readonly Random _random;
    public Deck(Random random)
    { 
        //(fältet = parametern)
        _random = random;
        // två loopar i varandra: varje Suit kombinerad med varje Rank
        // skapa ett nytt Card och lägg det i listan
    }
}