namespace Tjugoett;

// En människas strategi: visar korten och frågar spelaren via konsolen.
public class HumanStrategy : IPlayerStrategy
{
    private readonly ConsoleUi _ui;
    private readonly string _name;

    public HumanStrategy(ConsoleUi ui, string name)
    {
        _ui = ui;
        _name = name;
    }

    public Move Decide(Hand hand, Card dealerUpcard)
    {
        _ui.ShowTurn(_name, hand, dealerUpcard);
        return _ui.AskMove();
    }
}