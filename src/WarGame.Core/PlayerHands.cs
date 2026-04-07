namespace WarGame.Core;

// PlayerHands contains the dictionary for all players and hands.
public class PlayerHands
{
    public Dictionary<string, Hand> Hands { get; } = new Dictionary<string, Hand>();
}
