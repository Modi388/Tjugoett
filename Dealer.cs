namespace Tjugoett;

public class Dealer : Participant // Givaren är en deltagare, men inte en spelare
{
    public Dealer() : base("Givaren") // Skickar namnet "Givaren" till Participant
    {
    }

    public Card Upcard => Hand.Cards[0]; // Första kortet är öppet, andra är hålkortet

    public bool ShouldHit() // Givarens fasta regel, givaren väljer aldrig
    {
        return Hand.Value <= 16; // Köp på 16 eller lägre, stanna på 17 eller högre
    }
}