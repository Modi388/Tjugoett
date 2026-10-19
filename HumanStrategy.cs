
namespace Tjugoett;

// En människas strategi: frågar  spelaren via konsolen
public class HumanStrategy : IPlayerStrategy
{
    private readonly ConsoleUi _ui;

    public HumanStrategy(ConsoleUi ui)
    {
        _ui = ui;
    }

    public Move Decide(Hand hand, Card dealerUpcard)
    {
        return _ui.AskMove();
    }

}