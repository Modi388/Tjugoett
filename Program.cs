using Tjugoett;

// Här skapas allt som ska finnas EN gång i programmet.
Random random = new Random();
Deck deck = new Deck(random);
Dealer dealer = new Dealer();
ConsoleUi ui = new ConsoleUi();
GameStorage storage = new GameStorage("players.txt");

Game game = new Game(ui, deck, dealer, storage);
game.Run();