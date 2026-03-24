public class Hand
{
    public Queue<Card> hand { get; } = new Queue<Card> ();

    public Card PlayCard()
    {
        return hand.Dequeue();
    }

    public void AddToHand(IEnumerable<Card> cards)
    {
        foreach (Card card in cards)
        {
            hand.Enqueue (card);
        }
    }

    public bool HasCardsLeft()
    {
        return hand.Count > 0;
    }
}