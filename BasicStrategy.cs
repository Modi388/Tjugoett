namespace Tjugoett;

public class BasicStrategy : IPlayerStrategy // Datorspelare som följer grundstrategin
{
    public Move Decide(Hand hand, Card dealerUpcard) // Krävs av interfacet
    {
        int dealer = dealerUpcard.Points; // Givarens öppna kort som ett tal, 2-11 (ess = 11)

        if (hand.IsSoft) // Mjuk hand: ett ess räknas som 11
            return DecideSoft(hand.Value, dealer);

        return DecideHard(hand.Value, dealer); // Annars hård hand
    }

    private Move DecideHard(int value, int dealer) // Regler för hård hand
    {
        if (value >= 17)
            return Move.Stand; // 17 eller högre: stanna alltid
        if (value <= 11)
            return Move.Hit; // 11 eller lägre: köp alltid, du kan inte spräcka
        if (value == 12)
            return dealer >= 4 && dealer <= 6 ? Move.Stand : Move.Hit; // 12: stanna bara mot 4-6
        return dealer <= 6 ? Move.Stand : Move.Hit; // 13-16: stanna mot 2-6, annars köp
    }

    private Move DecideSoft(int value, int dealer) // Regler för mjuk hand
    {
        if (value >= 19)
            return Move.Stand; // Mjuk 19 eller högre: stanna
        if (value == 18)
            return dealer <= 8 ? Move.Stand : Move.Hit; // Mjuk 18: köp mot 9, 10 och ess
        return Move.Hit; // Mjuk 17 eller lägre: köp
    }
}