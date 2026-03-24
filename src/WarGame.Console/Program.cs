Console.WriteLine("Enter the number of players (1-4): ");
int numPlayers = int.Parse(Console.ReadLine());
List<string> players = new List<string>();
for (int i = 0; i < numPlayers; i++)
{
    players.Add($"Player {i}");
}

WarGame game = new WarGame(players);
game.Game();

Console.WriteLine("\nEnter any key to exit: ");
Console.ReadLine();
