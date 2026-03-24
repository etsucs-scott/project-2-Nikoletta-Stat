using WarGame.Core;
{
    Console.WriteLine("Enter the number of players (2-4): ");
    int numPlayers = int.Parse(Console.ReadLine());
    List<string> players = new List<string>();
    for (int i = 1; i < numPlayers; i++)
    {
        players.Add($"Player {i}");
    }

    WarEngine game = new WarEngine(players);
    game.Game();

    Console.WriteLine("\nEnter any key to exit: ");
    Console.ReadLine();
}
