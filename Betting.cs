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
}