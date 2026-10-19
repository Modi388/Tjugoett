namespace Tjugoett;

public class SimpleStrategy : IPlayerStrategy // Datorspelare med en enkel regel
{
    public Move Decide(Hand hand, Card dealerUpcard) // Krävs av interfacet
    {
        if (hand.Value < 17) // Under 17: köp ett kort till
            return Move.Hit;
        return Move.Stand; // 17 eller högre: stanna
    }
}