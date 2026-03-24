namespace WarGame.Core;
{
public class Deck
{
    private Stack<Card> Cards = new Stack<Card>;
    private Random rand = new Random();

    public Deck()
    {
        Initialize();
        Shuffle();
    }

    private void Initialize()
    {
        foreach (Suit Suit in Enum.GetValues(typeof(Suit)))
        {
            foreach (Rank Rank in Enum.GetValues(typeof(Rank)))
                Cards.Push(new Card(Suit, Rank));
        }
    }

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

    private Card Draw()
    {
        return Cards.Pop();
    }
}
}