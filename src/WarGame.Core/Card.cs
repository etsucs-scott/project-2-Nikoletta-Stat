namespace WarGame.Core;

// Suit contains every suit of card.
public enum Suit
{
    Spades,
    Hearts,
    Diamonds,
    Clubs
}

// Rank contains every possible rank.
public enum Rank
{
    Two,
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
    Ten,
    Jack,
    Queen,
    King,
    Ace
}

// A card has a suit and a rank.
public class Card
{
    public Suit Suit { get; }
    public Rank Rank { get; }

    public Card(Suit suit, Rank rank)
    {
        Suit = suit;
        Rank = rank;
    }

    // RankAsString returns the numeric value of each rank as a string.
    public string RankAsString()
    {
        if (Rank == Rank.Two)
            return "2";
        if (Rank == Rank.Three)
            return "3";
        if (Rank == Rank.Four)
            return "4";
        if (Rank == Rank.Five)
            return "5";
        if (Rank == Rank.Six)
            return "6";
        if (Rank == Rank.Seven)
            return "7";
        if (Rank == Rank.Eight)
            return "8";
        if (Rank == Rank.Nine)
            return "9";
        if (Rank == Rank.Ten)
            return "10";
        if (Rank == Rank.Jack)
            return "J";
        if (Rank == Rank.Queen)
            return "Q";
        if (Rank == Rank.King)
            return "K";
        if (Rank == Rank.Ace)
            return "A";

        return string.Empty;
    }
}
