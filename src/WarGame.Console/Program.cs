using WarGame.Core;

// User enters the number of players.

Console.WriteLine("Enter the number of players (2-4): ");
int numPlayers = int.Parse(Console.ReadLine());

// Create a new list of player names.

List<string> players = new();
for (int i = 0; i < numPlayers; i++)
{
    players.Add($"Player {i + 1}");
}

// Create a new War Game.

WarEngine game = new WarEngine(players);
game.Game();

Console.WriteLine("\nEnter any key to exit: ");
Console.ReadLine();

