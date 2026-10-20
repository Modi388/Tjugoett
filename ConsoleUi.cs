
namespace Tjugoett;

public class ConsoleUi
{
    private readonly CardRenderer _renderer = new CardRenderer();
    public ConsoleUi()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;   // så att ♥♦♣♠ visas rätt
    }
    // Gör om ett kort till kort text, t.ex. "♥A" eller "♠10".
    public string FormatCard(Card card)
    {
        return SuitSymbol(card.Suit) + RankText(card.Rank);
    }

    // Färgen som symbol
    private string SuitSymbol(Suit suit)
    {
        return suit switch
        {
            Suit.Hearts => "♥",
            Suit.Diamonds => "♦",
            Suit.Clubs => "♣",
            Suit.Spades => "♠",
            _ => "?"
        };
    }

    // Valören som kort svensk text. Bygger inte på enumens tal.
    private string RankText(Rank rank)
    {
        return rank switch
        {
            Rank.Two => "2",
            Rank.Three => "3",
            Rank.Four => "4",
            Rank.Five => "5",
            Rank.Six => "6",
            Rank.Seven => "7",
            Rank.Eight => "8",
            Rank.Nine => "9",
            Rank.Ten => "10",
            Rank.Jack => "Kn",
            Rank.Queen => "D",
            Rank.King => "K",
            Rank.Ace => "A",
            _ => "?"
        };
    }

    // Skriver ut ett kort. Hjärter och ruter blir röda.
    private void WriteCard(Card card)
    {
        if (card.Suit == Suit.Hearts || card.Suit == Suit.Diamonds)
        {
            Console.ForegroundColor = ConsoleColor.Red;
        }

        Console.Write(FormatCard(card));
        Console.ResetColor();
    }
    // Frågar spelaren om köpa eller stanna. Frågar igen tills svaret är giltigt.
    public Move AskMove()
    {
        while (true)
        {
            Console.Write("Vill du köpa (k) eller stanna (s)? ");
            string? input = Console.ReadLine();

            if (input == null)
            {
                return Move.Stand;   // inmatningen tog slut, stanna hellre än att krascha
            }

            string answer = input.Trim().ToLower();

            if (answer == "k")
            {
                return Move.Hit;
            }
            if (answer == "s")
            {
                return Move.Stand;
            }

            Console.WriteLine("Ogiltigt val. Skriv k för att köpa eller s för att stanna.");
        }
    }

    // Visar en deltagares kort och handens värde.
    // hideHoleCard = true döljer givarens andra kort (hålkortet).
    public void ShowHand(Participant participant, bool hideHoleCard)
    {
        Console.WriteLine(participant.Name + ":");
        WriteCardRow(participant.Hand.Cards, hideHoleCard);

        if (hideHoleCard)
        {
            Console.WriteLine();
            return;
        }

        Console.Write("(" + participant.Hand.Value);
        if (participant.Hand.IsSoft)
        {
            Console.Write(" mjuk");
        }
        Console.WriteLine(")");

        if (participant.Hand.IsBust)
        {
            Console.WriteLine("Spräckt!");
        }
        else if (participant.Hand.IsBlackjack)
        {
            Console.WriteLine("Tjugoett!");
        }
    }


    // Frågar hur många spelare som ska vara med och vad de heter.
    // max = hur många som får plats (1–5 minus de som redan finns).
    public List<string> AskPlayerNames(int max)
    {
        int count = AskNumber("Hur många spelare (1-" + max + ")? ", 1, max);

        List<string> names = new List<string>();
        for (int i = 1; i <= count; i++)
        {
            string name = "";
            while (name == "")
            {
                Console.Write("Namn på spelare " + i + ": ");
                name = (Console.ReadLine() ?? "").Trim();

                if (name == "")
                {
                    Console.WriteLine("Namnet får inte vara tomt.");
                }
            }
            names.Add(name);
        }
        return names;
    }
    // Frågar om spelaren ska styras av en människa eller av datorn.
    public int AskPlayerType(string name)
    {
        Console.WriteLine("Vem styr " + name + "?");
        Console.WriteLine("1. Människa");
        Console.WriteLine("2. Dator (enkel: köper under 17)");
        Console.WriteLine("3. Dator (grundstrategi)");
        return AskNumber("Välj 1-3: ", 1, 3);
    }


    // Frågar efter ett heltal mellan min och max. Frågar igen tills svaret är giltigt.
    private int AskNumber(string question, int min, int max)
    {
        while (true)
        {
            Console.Write(question);
            string input = Console.ReadLine() ?? "";

            if (int.TryParse(input, out int number) && number >= min && number <= max)
            {
                return number;
            }
            Console.WriteLine("Skriv ett tal mellan " + min + " och " + max + ".");
        }
    }


    // Visar läget när en mänsklig spelare ska välja:
    // givarens öppna kort och spelarens egen hand.
    public void ShowTurn(string playerName, Hand hand, Card dealerUpcard)
    {
        Console.WriteLine("Givarens öppna kort:");
        WriteCardRow(new List<Card> { dealerUpcard }, false);

        Console.WriteLine(playerName + ":");
        WriteCardRow(hand.Cards, false);
        Console.Write("(" + hand.Value);
        if (hand.IsSoft)
        {
            Console.Write(" mjuk");
        }
        Console.WriteLine(")");
    }
    // Visar huvudmenyn och returnerar spelarens val (1-4).
    public int ShowMenu()
    {
        Console.WriteLine();
        Console.WriteLine("=== TJUGOETT ===");
        Console.WriteLine("1. Lägg till spelare");
        Console.WriteLine("2. Spela en runda");
        Console.WriteLine("3. Visa statistik");
        Console.WriteLine("4. Avsluta");
        return AskNumber("Välj (1-4): ", 1, 4);
    }
    // Visar slutläget efter en runda: alla händer och varje spelares resultat.
    public void ShowResults(Dealer dealer, Dictionary<Player, Outcome> results)
    {
        Console.WriteLine();
        Console.WriteLine("--- Resultat ---");
        ShowHand(dealer, false);

        foreach (KeyValuePair<Player, Outcome> result in results)
        {
            ShowHand(result.Key, false);
            Console.WriteLine("  → " + OutcomeText(result.Value));
        }
    }

    // Översätter ett resultat till svensk text.
    private string OutcomeText(Outcome outcome)
    {
        return outcome switch
        {
            Outcome.Win => "Vinst",
            Outcome.Blackjack => "Vinst med tjugoett!",
            Outcome.Push => "Lika",
            Outcome.Lose => "Förlust",
            _ => "?"
        };
    }
    // Visar vinster, förluster och lika för varje spelare.
    public void ShowStatistics(List<Player> players)
    {
        Console.WriteLine();
        Console.WriteLine("--- Statistik ---");

        if (players.Count == 0)
        {
            Console.WriteLine("Inga spelare än.");
            return;
        }

        foreach (Player player in players)
        {
            Console.WriteLine(player.Name + ": " + player.Wins + " vinster, "
                + player.Losses + " förluster, " + player.Pushes + " lika");
        }
    }

    // Skriver ut ett kort meddelande till spelaren, t.ex. ett felmeddelande från Game.
    public void ShowMessage(string message)
    {
        Console.WriteLine(message);
    }
    // Skriver ut flera kort BREDVID varandra.
    // Vi kan inte skriva ett kort i taget, eftersom då hamnar de under varandra.
    // I stället skriver vi rad 0 för alla kort, sedan rad 1 för alla kort, osv.
    private void WriteCardRow(IReadOnlyList<Card> cards, bool hideHoleCard)
    {
        List<string[]> pictures = new List<string[]>();
        List<bool> isRed = new List<bool>();

        for (int i = 0; i < cards.Count; i++)
        {
            if (hideHoleCard && i == 1)
            {
                pictures.Add(_renderer.RenderBack());
                isRed.Add(false);
            }
            else
            {
                Card card = cards[i];
                pictures.Add(_renderer.RenderFront(RankText(card.Rank), SuitSymbol(card.Suit)));
                isRed.Add(card.Suit == Suit.Hearts || card.Suit == Suit.Diamonds);
            }
        }

        if (pictures.Count == 0)
        {
            return;
        }

        for (int line = 0; line < pictures[0].Length; line++)
        {
            for (int c = 0; c < pictures.Count; c++)
            {
                if (isRed[c])
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                Console.Write(pictures[c][line]);
                Console.ResetColor();
                Console.Write(" ");
            }
            Console.WriteLine();
        }
    }
    public void ShowCount(int runningCount, double trueCount)
    {
        Console.WriteLine("Running count: " + runningCount);
        Console.WriteLine("True count: " + trueCount.ToString("0.0"));
    }
}