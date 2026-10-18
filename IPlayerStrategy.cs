namespace Tjugoett;

public interface IPlayerStrategy // Kontrakt: alla som kan välja drag måste ha Decide
{
    Move Decide(Hand hand, Card dealerUpcard); // Får handen och givarens öppna kort, svarar Hit eller Stand
}