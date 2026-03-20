using System;

public enum Suit
{
	Spades,
	Hearts,
	Diamonds,
	Clubs
}

pulic enum Rank
{
	Two = 2,
	Three = 3,
	Four = 4,
	Five = 5,
	Six = 6,
	Seven = 7,
	Eight = 8,
	Nine = 9,
	Ten = 10,
	Jack = 11,
	Queen = 12,
	King = 13,
	Ace = 14
}
public class Card
{
	public Suit Suit { get; }
	public Rank Rank { get; }

	public Card(Suit suit, Rank rank)
	{
		Suit = suit;
		Rank = rank;
	}
}
