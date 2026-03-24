namespace WarGame.Core;
{
public class WarEngine
{
    private PlayerHands playerHands = new PlayerHands();
    private PlayedCards playedCards = new PlayedCards();
    private List<Card> pot = new List<Card>();
    private int roundLimit { get; set; }

    public WarEngine(IEnumerable<string> playerNames)
    {
        roundLimit = 10000;
        foreach (string playerName in playerNames)
        {
            playerHands.Hands[playerName] = new Hand();
        }
    }

    public void DealHands()
    {
        Deck deck = new Deck();
        int numPlayers = playerHands.Hands.Count;
        int playerIndex = 0;
        
        while (deck.Count > 0)        
        {
            var card = deck.Draw();
            List<string> keys = new List<string>(playerHands.Hands.Keys);
            string currentPlayer = keys[playerIndex % numPlayers];
            playerHands.Hands[currentPlayer].hand.Enqueue(card);
            playerIndex++;
        }
    }

    public List<string> ActivePlayers()
    {
        List<string> activePlayers = new List<string>();

        foreach (string playerName in playerHands.Hands.Keys)
        {
            if (playerHands.Hands[playerName].HasCards)
                activePlayers.Add(playerName);
        }

        return activePlayers;
    }

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

    public void PlayRound(List<string> activePlayers, bool tiebreaker)
    {
        playedCards.Clear();

        if (tiebreaker)
            Console.Write("Tiebreaker: ");

        foreach (string playerName in activePlayers)
        {
            Hand hand = playerHands.Hands[playerName];
            Card card = hand.PlayCard();
            playedCards.Cards[playerName] = card;
            pot.Add(card);
            Console.WriteLine($"{playerName}: {card.RankAsString()}");
        }
        InterpretRound(activePlayers);
    }

    public void InterpretRound(List<string> players)
    {
        int maxRank = 0;
        foreach (Card card in playedCards.Cards.Values)
        {
            if (card.Rank > maxRank)
                maxRank = card.Rank;
        }

        List<string> tiedPlayers = new List<string>();
        foreach (string playerName in playedCards.Cards.Keys)
        {
            if (playedCards.Cards[playerName].Rank == maxRank)
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
            PlayRound(tiedPlayers, true);
        }
        else
            TakePot(tiedPlayers[0]);
    }

    public void TakePot(string playerName)
    {
        playerHands.Hands[playerName].AddToHand(pot);
        Console.WriteLine($"Winner: {playerName} (Cards: {DisplayCardCounts()})");
    }

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
            int cardCount = playerHands.Hands[playerName].Count;

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

    public string DisplayCardCounts()
    {
        string countsString = "";
        bool firstIndex = true;
        foreach (string name in playerHands.Hands.Keys)
        {
            if (!firstIndex)
                countsString += ", ";
            countsString += $"{name} = {playerHands.Hands[name].Count}";
            firstIndex = false;
        }
        return countsString;
    }


}
}