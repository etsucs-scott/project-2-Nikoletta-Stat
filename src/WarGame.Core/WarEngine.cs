namespace WarGame.Core;

// WarEngine contains the actual game logic.
public class WarEngine
{
    private PlayerHands playerHands = new PlayerHands();
    private PlayedCards playedCards = new PlayedCards();
    private List<Card> pot = new List<Card>();
    private int roundLimit { get; set; }

    // WarEngine constructor
    public WarEngine(IEnumerable<string> playerNames)
    {
        roundLimit = 10000;
        foreach (string playerName in playerNames)
        {
            playerHands.Hands[playerName] = new Hand();
        }
    }
    
    // DealHands initializes a deck and distributes the cards between players hands in round robin order.
    public void DealHands()
    {
        Deck deck = new Deck();
        int numPlayers = playerHands.Hands.Count;
        int playerIndex = 0;
        
        while (deck.Count() > 0)        
        {
            var card = deck.Draw();
            List<string> keys = new List<string>(playerHands.Hands.Keys);
            string currentPlayer = keys[playerIndex % numPlayers];
            playerHands.Hands[currentPlayer].hand.Enqueue(card);
            playerIndex++;
        }
    }

    // ActivePlayers returns a list of strings of player names who still have cards and removes players from the dictionary who do not have cards.
    public List<string> ActivePlayers()
    {
        List<string> activePlayers = new List<string>();

        foreach (string playerName in playerHands.Hands.Keys)
        {
            if (playerHands.Hands[playerName].HasCardsLeft())
                activePlayers.Add(playerName);
            else
            {
                if (playerHands.Hands.Remove(playerName))
                {
                    playerHands.Hands.Remove(playerName);
                }
            }
        }

        return activePlayers;
    }

    // Game is the main game loop, it holds the game logic and keeps track of the rounds.
    public void Game()
    {
        DealHands();
        int round = 0;

        while (round < roundLimit)
        {
            List<string> activePlayers = ActivePlayers();

            if (activePlayers.Count < 2)
                break;

            round++;
            pot.Clear();
            Console.WriteLine("\nRound " + round);
            PlayRound(activePlayers, false);
        }
        DeclareFinalWinner();
    }

    // PlayRound takes in a string of activePlayers and loops through each player to play a card from their hand.
    // If the round is a tiebreaker, it writes it to the console.
    public void PlayRound(List<string> activePlayers, bool tiebreaker)
    {
        playedCards.Played.Clear();

        if (tiebreaker)
            Console.Write("Tiebreaker: ");

        foreach (string playerName in activePlayers)
        {
            Hand hand = playerHands.Hands[playerName];
            Card card = hand.PlayCard();
            playedCards.Played[playerName] = card;
            pot.Add(card);
            Console.WriteLine($"{playerName}: {card.RankAsString()}");
        }
        InterpretRound(activePlayers);
    }

    // InterpretRound determines if a tiebreaker round is necessary. If not, it awards the pot to the winner of the round.
    public void InterpretRound(List<string> players)
    {
        int maxRank = 0;
        foreach (Card card in playedCards.Played.Values)
        {
            if ((int)card.Rank > maxRank)
                maxRank = (int)card.Rank;
        }

        List<string> tiedPlayers = new List<string>();
        foreach (string playerName in playedCards.Played.Keys)
        {
            if ((int)playedCards.Played[playerName].Rank == maxRank)
                tiedPlayers.Add(playerName);
        }

        if (tiedPlayers.Count > 1)
        {
            Console.WriteLine($"Tie between {string.Join(" and ", tiedPlayers)}!");
            string potString = "";
            for (int i = 0; i < pot.Count; i++)
            {
                potString += pot[i].RankAsString();
                if (i < pot.Count - 1)
                    potString += ", ";
            }
            Console.WriteLine($"Pot includes: {potString}");
            for (int i = tiedPlayers.Count - 1; i >= 0; i--)
            {
                string player = tiedPlayers[i];
                if (!playerHands.Hands[player].HasCardsLeft())
                {
                    Console.WriteLine($"{player} has no cards left.");
                    tiedPlayers.Remove(player);
                }

            }
            if (tiedPlayers.Count > 1)
                PlayRound(tiedPlayers, true);
        }
        else
            TakePot(tiedPlayers[0]);
    }

    // TakePot adds the contents of the pot to the winner of the round and displays the winner and card counts.
    public void TakePot(string playerName)
    {
        playerHands.Hands[playerName].AddToHand(pot);
        Console.WriteLine($"Winner: {playerName} (Cards: {DisplayCardCounts()})");
    }

    // DeclareFinalWinner writes the end of game comments. It writes the final winner to the screen, or if there is a tie, it declares the tie.
    // It also handles the round limit and displays the card count.
    public void DeclareFinalWinner()
    {
        List<string> activePlayers = ActivePlayers();

        if (activePlayers.Count == 1)
        {
            Console.WriteLine($"Winner: {activePlayers[0]}");
            return;
        }

        Console.WriteLine("Round limit reached. Winner by card count: ");
        int highestCount = 0;
        List<string> tieList = new();

        foreach (string playerName in activePlayers)
        {
            int cardCount = playerHands.Hands[playerName].hand.Count;

            if (cardCount >  highestCount)
            {
                highestCount = cardCount;
                tieList.Clear();
                tieList.Add(playerName);
            }
            else if (cardCount == highestCount)
            {
                tieList.Add(playerName);
            }
        }

        if (tieList.Count == 1)
        {
            Console.WriteLine(tieList[0]);
        }
        else
        {
            Console.WriteLine($"Tie between {string.Join(" and ", tieList)}");
        }
        Console.WriteLine($"Final Card Counts: {DisplayCardCounts()}");

    }

    // DisplayCardCounts formats each player's card count as a string.
    public string DisplayCardCounts()
    {
        string countsString = "";
        bool firstIndex = true;
        foreach (string name in playerHands.Hands.Keys)
        {
            if (!firstIndex)
                countsString += ", ";
            countsString += $"{name} = {playerHands.Hands[name].hand.Count}";
            firstIndex = false;
        }
        return countsString;
    }


}
