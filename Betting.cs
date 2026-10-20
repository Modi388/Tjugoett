namespace Tjugoett;

// Hanterar spelarens marker och satsningar
public class Betting
{
    // Hur många marker spelaren har
    public int Balance { get; private set; }

    // Spelaren börjar med 1000 marker
    public Betting()
    {
        Balance = 1000;
    }

    // Kontrollerar om spelaren har råd med satsningen
    public bool CanBet(int amount)
    {
        return amount > 0 && amount <= Balance;
    }

    // Kontrollerar och godkänner spelarens satsning
    public bool PlaceBet(int amount)
    {
        if (!CanBet(amount))
        {
            return false;
        }

        return true;
    }
}