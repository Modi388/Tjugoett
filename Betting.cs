namespace Tjugoett;

// Hanterar spelarens marker och satsningar
public class Betting
{
    // Hur många marker spelaren har
    public int Balance { get; private set; }

    // Hur många marker spelaren har satsat
    public int CurrentBet { get; private set; }

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

    // Kontrollerar och sparar spelarens satsning
    public bool PlaceBet(int amount)
    {
    // Nekar om spelaren inte har råd eller redan har satsat
    if (!CanBet(amount) || CurrentBet > 0)
    {
        return false;
    }

    // Sparar satsningen
    CurrentBet = amount;

    // Drar bort satsningen från saldot
    Balance -= amount;

    return true;
    }

    // Betalar ut marker efter spelrundan
    public void SettleBet(Outcome outcome)
    {
    // Om spelaren inte har satsat något avslutas metoden
    if (CurrentBet == 0)
    {
        return;
    }

    // Vid vanlig vinst får spelaren tillbaka dubbla satsningen
    if (outcome == Outcome.Win)
    {
        Balance += CurrentBet * 2;
    }
    // Vid Blackjack får spelaren tillbaka satsningen och 1,5 gånger i vinst
    else if (outcome == Outcome.Blackjack)
    {
        Balance += CurrentBet * 5 / 2;
    }
    // Vid oavgjort får spelaren tillbaka sin satsning
    else if (outcome == Outcome.Push)
    {
        Balance += CurrentBet;
    }

    // Satsningen nollställs inför nästa runda
    CurrentBet = 0;
    }

 
}