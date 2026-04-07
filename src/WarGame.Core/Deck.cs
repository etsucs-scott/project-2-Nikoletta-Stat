namespace WarGame.Core;

// Deck holds a stack of card objects.
public class Deck
{
    private Stack<Card> Cards = new Stack<Card>();
    private Random rand = new Random();

    // Default constructor.
    public Deck()
    {
        Initialize();
        Shuffle();
    }

    // Initialize creates a deck by creating cards of every rank and suit.
    private void Initialize()
    {
        foreach (Suit Suit in Enum.GetValues(typeof(Suit)))
        {
            foreach (Rank Rank in Enum.GetValues(typeof(Rank)))
                Cards.Push(new Card(Suit, Rank));
        }
    }

    // Shuffle randomizes the order of the cards in the deck.
    private void Shuffle()
    {
        List<Card> CardDeck = Cards.ToList();
        int n = CardDeck.Count;

        for (int i = n - 1; i > 0;  i--)
        {
            int j = rand.Next(i + 1);
            var temp = CardDeck[i];
            CardDeck[i] = CardDeck[j];
            CardDeck[j] = temp;
        }

        Cards.Clear();
        foreach (Card Card in CardDeck)
        {
            Cards.Push(Card);
        }
    }

    // Draw returns the top card in the stack.
    public Card Draw()
    {
        return Cards.Pop();
    }

    // Count returns count.
    public int Count() { return Cards.Count; }
}
