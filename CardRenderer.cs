namespace Tjugoett;

// Ritar kort som textbilder. Varje kort blir en string[] med en sträng per rad.
// Renderaren skriver inget själv och känner inte till färger. Det sköter ConsoleUi.
public class CardRenderer
{
    private const int InnerWidth = 7;   // antal tecken innanför ramen

    // Framsidan: valören uppe till vänster och nere till höger, färgen i mitten.
    public string[] RenderFront(string rank, string suit)
    {
        return new string[]
        {
            "┌───────┐",
            "│" + rank.PadRight(InnerWidth) + "│",
            "│       │",
            "│   " + suit + "   │",
            "│       │",
            "│" + rank.PadLeft(InnerWidth) + "│",
            "└───────┘"
        };
    }

    // Baksidan, används för givarens dolda hålkort.
    public string[] RenderBack()
    {
        return new string[]
        {
            "┌───────┐",
            "│▒▒▒▒▒▒▒│",
            "│▒▒▒▒▒▒▒│",
            "│▒▒▒▒▒▒▒│",
            "│▒▒▒▒▒▒▒│",
            "│▒▒▒▒▒▒▒│",
            "└───────┘"
        };
    }
}