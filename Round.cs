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

    // Räknar korten som syns (tillval 5)
    private readonly CardCounter _counter;

    // Tar emot kortleken, givaren och spelarna
    public Round(Deck deck, Dealer dealer, List<Player> players, CardCounter counter)
    {
        _deck = deck;
        _dealer = dealer;
        _players = players;
        _counter = counter;
    }

    // Startar en ny spelrunda
       // Startar en ny spelrunda
    public Dictionary<Player, Outcome> Play()
    {
        // Blandar om kortleken om det finns för få kort kvar
        if (_deck.NeedsReshuffle)
        {
            _deck.Reset();
            _counter.Reset();   // alla kort är tillbaka i skon, räkningen börjar om
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
            // Spelarnas kort syns alltid
            foreach (Player player in _players)
            {
                player.ReceiveCard(DrawVisible());
            }

            // Givarens första kort syns, det andra (hålkortet) är dolt
            if (i == 0)
            {
                _dealer.ReceiveCard(DrawVisible());
            }
            else
            {
                _dealer.ReceiveCard(_deck.Draw());
            }
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

                // Ger spelaren ett nytt kort move
                player.ReceiveCard(DrawVisible());
            }
        }

        // Givaren vänder sitt hålkort, nu ser alla det och räknaren får räkna det
        _counter.Observe(_dealer.Hand.Cards[1]);

        // Givaren drar kort tills den ska stanna
        while (_dealer.ShouldHit())
        {
            _dealer.ReceiveCard(DrawVisible());
        }

        // Avgör och sparar resultatet för varje spelare
        foreach (Player player in _players)
        {
            Outcome outcome = Resolve(player.Hand, _dealer.Hand);

            results[player] = outcome;

            player.RecordOutcome(outcome);
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
    // Drar ett kort som alla vid bordet ser, och låter räknaren räkna det
    private Card DrawVisible()
    {
        Card card = _deck.Draw();
        _counter.Observe(card);
        return card;
    }
}
