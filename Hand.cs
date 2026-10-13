namespace Tjugoett; // Det talar om att Hand hör till projektet Tjugoett, så att den hittar Card och Rank som Mohammed skrivit.

public class Hand

{
 // En klass är en ritning. Hand beskriver vad en hand är och kan göra. publiv betyder att andra klasser får använda den såsom (Player, Round, ConsoleUi).

private readonly List<Card> _cards = new(); // listan med korten, private eftersom bara Hand får röra listan. Readonly betyder att _cards alltid pekar på samma lista, det går att lägga till och ta bort kort men listan bryts inte.  

public IReadOnlyList<Card> Cards => _cards; // Andra klasser (t.ex. ConsoleUi) får läsa korten men inte ändra dem, eftersom denna typ är skrivskyddad.
public void Add(Card card) // Tar emot ett kort när Round delar ut eller spelaren köper.
    {
        _cards.Add(card); // Lägger kortet sist i listan.
    }

    public void Clear() // ANropas i blrjan av varje runda
    {
        _cards.Clear(); // Tömmer handen så att fö
        //förra rundans kort inte påverkar värdet.
      }

private int HardTotal() // Sumamn där alla ess räknas som 1
    {
        int total = 0;
        foreach (Card card in _cards)  // gå igenom varje kort på handen, ett i taget.
        {
            if (card.Rank == Rank.Ace) // Är kortet ett ess?
                total += 1; // Om svaret blir ja: lägg till 1
            else if (card.Rank >= Rank.Ten) // Annars, är det tio, knekt, dam eller kund?
                total += 10; // Ja: lägg till 10
            else 
            total += (int)card.Rank; // 2-9 är sitt eget värde
        }
        return total;

        }
    }

