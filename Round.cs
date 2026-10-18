
namespace Tjugoett;

// Hanterar en spelrunda
public class Round
{
    // Kortleken som används under spelrundan
    private readonly Deck _deck;

    // Givaren som deltar i spelrundan
    private readonly Dealer _dealer;

    // Spelarna som deltar i spelrundan
    private readonly List<Player> _players;

    // Tar emot kortleken, givaren och spelarna
    public Round(Deck deck, Dealer dealer, List<Player> players)
    {
        _deck = deck;
        _dealer = dealer;
        _players = players;
    }
    
    // Avgör resultatet mellan spelaren och dealern
    public static Outcome Resolve(Hand player, Hand dealer)
    {
        // Om spelaren får över 21 förlorar spelaren
        if (player.IsBust)
        {
            return Outcome.Lose;
        }

        // Om båda får Blackjack blir det oavgjort
        if (player.IsBlackjack && dealer.IsBlackjack)
        {
            return Outcome.Push;
        }

        // Om bara spelaren har Blackjack vinner spelaren
        if (player.IsBlackjack)
        {
            return Outcome.Blackjack;
        }
        
        // Om bara dealern har Blackjack förlorar spelaren
        if (dealer.IsBlackjack)
        {
            return Outcome.Lose;
        }

        // Om dealern får över 21 vinner spelaren
        if (dealer.IsBust)
        {
            return Outcome.Win;
        }

        // Om spelaren har fler poäng än dealern vinner spelaren
        if (player.Value > dealer.Value)
        {
            return Outcome.Win;
        }

        // Om dealern har fler poäng än spelaren förlorar spelaren
        if (player.Value < dealer.Value)
        {
            return Outcome.Lose;
        }

        // Om båda har lika många poäng blir det oavgjort
        return Outcome.Push;
    }
}
