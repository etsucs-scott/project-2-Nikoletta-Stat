namespace WarGame.Core;

// Hand holds a queue of cards.
public class Hand
{
    public Queue<Card> hand { get; } = new Queue<Card> ();

    // PlayCard returns the first card in the queue.
    public Card PlayCard()
    {
        return hand.Dequeue();
    }

    // AddToHand adds cards to a player's hand.
    public void AddToHand(IEnumerable<Card> cards)
    {
        foreach (Card card in cards)
        {
            hand.Enqueue (card);
        }
    }

    // HasCardsLeft returns true if there are cards left, false if no cards.
    public bool HasCardsLeft()
    {
        return hand.Count > 0;
    }
}
