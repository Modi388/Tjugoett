namespace Tjugoett;

// Styr spelet: menyn, spelarna, rundorna och sparandet.
// All in- och utmatning går via ConsoleUi.
public class Game
{
    private const int MaxPlayers = 5;

    private readonly ConsoleUi _ui;
    private readonly Deck _deck;
    private readonly Dealer _dealer;
    private readonly GameStorage _storage;

    private readonly List<Player> _allPlayers = new List<Player>();   // alla kända: från filen + nya
    private readonly CardCounter _counter = new CardCounter();
    private readonly List<Player> _tablePlayers = new List<Player>(); // de som sitter vid bordet nu

    public Game(ConsoleUi ui, Deck deck, Dealer dealer, GameStorage storage)
    {
        _ui = ui;
        _deck = deck;
        _dealer = dealer;
        _storage = storage;
        LoadPlayers();
    }

    // Huvudloopen: visar menyn tills spelaren väljer att avsluta.
    public void Run()
    {
        while (true)
        {
            int choice = _ui.ShowMenu();

            if (choice == 1)
            {
                AddPlayers();
            }
            else if (choice == 2)
            {
                PlayRound();
            }
            else if (choice == 3)
            {
                _ui.ShowStatistics(_allPlayers);
            }
            else
            {
                _ui.ShowMessage("Tack för att du spelade!");
                return;
            }
        }
    }

    // Läser in sparade spelare från filen vid start.
    private void LoadPlayers()
    {
        foreach (PlayerRecord record in _storage.Load())
        {
            IPlayerStrategy strategy = new HumanStrategy(_ui, record.Name);
            _allPlayers.Add(new Player(record.Name, strategy, record.Wins, record.Losses, record.Pushes));
        }
    }

    // Lägger till spelare vid bordet. Finns namnet sparat får spelaren tillbaka sin statistik.
    private void AddPlayers()
    {
        int free = MaxPlayers - _tablePlayers.Count;
        if (free == 0)
        {
            _ui.ShowMessage("Bordet är fullt (max " + MaxPlayers + " spelare).");
            return;
        }

        foreach (string name in _ui.AskPlayerNames(free))
        {
            Player? player = FindPlayer(name);

            if (player == null)
            {
                int type = _ui.AskPlayerType(name);
player = new Player(name, CreateStrategy(type, name));
            }
            else if (_tablePlayers.Contains(player))
            {
                _ui.ShowMessage(name + " sitter redan vid bordet.");
                continue;
            }
            else
            {
                _ui.ShowMessage("Välkommen tillbaka, " + player.Name + "!");
            }

            _tablePlayers.Add(player);
        }
    }

    // Letar upp en känd spelare med samma namn (stora/små bokstäver spelar ingen roll).
    private Player? FindPlayer(string name)
    {
        foreach (Player player in _allPlayers)
        {
            if (player.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
            {
                return player;
            }
        }
        return null;
    }

    // Spelar en runda. Byggs i nästa steg.
    // Spelar en runda med spelarna vid bordet, visar resultatet och sparar.

    // Väljer vem som fattar besluten åt spelaren.
private IPlayerStrategy CreateStrategy(int type, string name)
{
    if (type == 2) return new SimpleStrategy();
    if (type == 3) return new BasicStrategy();
    return new HumanStrategy(_ui, name);
}
    private void PlayRound()
    {
        if (_tablePlayers.Count == 0)
        {
            _ui.ShowMessage("Lägg till minst en spelare först (val 1).");
            return;
        }

        _ui.ShowMessage("");
        _ui.ShowMessage("--- Ny runda ---");

        Round round = new Round(_deck, _dealer, _tablePlayers, _counter);
        Dictionary<Player, Outcome> results = round.Play();

        _ui.ShowResults(_dealer, results);
        _ui.ShowCount(_counter.RunningCount, _counter.TrueCount(_deck.DecksRemaining));

        if (!_storage.Save(_allPlayers))
        {
            _ui.ShowMessage("Varning: kunde inte spara resultaten till fil.");
        }
    }
}