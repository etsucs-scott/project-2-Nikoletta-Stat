namespace WarGame.Core;

// PlayedCards holds the dictionary that contains each card played and the player who played it.
public class PlayedCards
{
    public Dictionary<string, Card> Played { get; } = new Dictionary<string, Card>();
}
