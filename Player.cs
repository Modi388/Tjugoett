namespace Tjugoett;

public class Player : Participant // Spelaren ärver namn och hand från Participant
{
    private readonly IPlayerStrategy _strategy; // Den som bestämmer spelarens drag

    public int Wins { get; private set; } // Kan läsas utifrån, men bara Player ändrar dem
    public int Losses { get; private set; }
    public int Pushes { get; private set; }

    public Player(string name, IPlayerStrategy strategy, int wins = 0, int losses = 0, int pushes = 0)
        : base(name) // Skickar namnet vidare till Participant
    {
        _strategy = strategy;
        Wins = wins;
        Losses = losses;
        Pushes = pushes;
    }

    public Move Decide(Card dealerUpcard) // Frågar strategin vad spelaren ska göra
    {
        return _strategy.Decide(Hand, dealerUpcard);
    }

    public void RecordOutcome(Outcome outcome) // Round anropar den när rundan är avgjord
    {
        if (outcome == Outcome.Win || outcome == Outcome.Blackjack) // Blackjack räknas som vinst
            Wins++;
        else if (outcome == Outcome.Lose)
            Losses++;
        else
            Pushes++; // Lika
    }
}