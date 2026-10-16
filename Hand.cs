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

private int HardTotal() // Summan där alla ess räknas som 1
    {
        int total = 0;
        foreach (Card card in _cards) // Gå igenom varje kort på handen, ett i taget
        {
            if (card.Rank == Rank.Ace) // Är kortet ett ess?
                total += 1; // Ja: räkna det som 1 (vi lägger till 10 senare i Value om det ryms)
            else
                total += card.Points; // Annars: ta poängen från kortet (2-10 sitt tal, klädda kort 10)
        }
        return total;
    }

private bool HasAce() // Finns det något ess på handen?
    {
        foreach (Card card in _cards) // Gå igenom varje kort
        {
            if (card.Rank == Rank.Ace) // Hittar vi ett ess
            return true; // Då svarar vi ja direkt

        }
        return false; // Inget ess hittades

    }  

    public bool IsSoft // Mjuk hand = ett ess räknas som 11
    {
        get { return HasAce() && HardTotal() + 10 <= 21; } // Finns ess OCH ryms det att lägga till 10?
    }

     public int Value // Handens värde, räknas om varje gång så att esset kan byta värde
    {
        get
        {
            if (IsSoft) // Mjuk hand: ett ess räknas som 11
                return HardTotal() + 10;
            return HardTotal(); // Annars räknas alla ess som 1
        }
    }

    public bool IsBust // Spräckt = över 21
    {
        get { return Value > 21; }
    }

    public bool IsBlackjack // Naturlig tjugoett = exakt två kort och värde 21
    {
        get { return _cards.Count == 2 && Value == 21; }
    }

}
