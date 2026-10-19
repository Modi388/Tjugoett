
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

    // Startar en ny spelrunda
    public Dictionary<Player, Outcome> Play()
    {
        // Blandar om kortleken om det finns för få kort kvar
        if (_deck.NeedsReshuffle)
        {
            _deck.Reset();
        }

        Dictionary<Player, Outcome> results = new();

        // Tömmer givarens hand inför en ny runda
        _dealer.Hand.Clear();

        // Tömmer alla spelares händer inför en ny runda
        foreach (Player player in _players)
        {
            player.Hand.Clear();
        }

        // Delar ut två kort till varje spelare och givaren
        for (int i = 0; i < 2; i++)
        {
            foreach (Player player in _players)
            {
                player.ReceiveCard(_deck.Draw());
            }

            _dealer.ReceiveCard(_deck.Draw());
        }

        // Varje spelare får välja att ta kort eller stanna
        foreach (Player player in _players)
        {
            // Spelaren fortsätter så länge den har under 21 poäng
            while (!player.Hand.IsBust &&
                   !player.Hand.IsBlackjack &&
                   player.Hand.Value < 21)
            {
                // Frågar spelaren om Hit eller Stand
                Move move = player.Decide(_dealer.Upcard);

                // Avslutar spelarens tur om den väljer Stand
                if (move == Move.Stand)
                {
                    break;
                }

                // Ger spelaren ett nytt kort
                player.ReceiveCard(_deck.Draw());
            }
        }

        return results;
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
