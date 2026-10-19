namespace Tjugoett;

public class CardCounter
{
    private int _runningCount;
    public int RunningCount => _runningCount;

    public void Observe(Card card)
    {
        if (card.Rank <= Rank.Six)
        {
            _runningCount++;
        }
        else if (card.Rank <= Rank.Nine)
        {
           // 7-9 neutrala: räkningen ändras inte
        }
        else
        {
            _runningCount --;
        }
    }
    
    public void Reset()
    {
        _runningCount = 0;
    }
}
