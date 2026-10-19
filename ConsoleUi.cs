
namespace Tjugoett;

public class ConsoleUi
{
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
        Console.Write(participant.Name + ": ");

        IReadOnlyList<Card> cards = participant.Hand.Cards;
        for (int i = 0; i < cards.Count; i++)
        {
            if (hideHoleCard && i == 1)
            {
                Console.Write("[??]");
            }
            else
            {
                WriteCard(cards[i]);
            }
            Console.Write(" ");
        }

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
}