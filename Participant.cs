namespace Tjugoett;

public abstract class Participant // Basklass för alla vid bordet (spelare och givare)
{
    public string Name { get; } // Namnet kan läsas men inte ändras utifrån
    public Hand Hand { get; } // Varje deltagare har en egen hand

    protected Participant(string name) // Bara subklasser får anropa konstruktorn
    {
        Name = name;
        Hand = new Hand(); // Skapar en tom hand
    }

    public void ReceiveCard(Card card) // Tar emot ett kort när Round delar ut
    {
        Hand.Add(card); // Lägger det på handen
    }
}