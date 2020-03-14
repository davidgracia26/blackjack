using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpBlackjack
{
    public enum PlayerType
    {
        None = 0,
        Player = 1,
        Dealer = 2
    }

    public class Game
    {
        public Game()
        {
            this.Turn = PlayerType.Player;
        }

        public bool IsOver { get; set; }
        public PlayerType Turn { get; set; }
        public PlayerType Winner { get; set; }

        public void Run()
        {
            var dealerHand = new List<Card>() { };
            var playerHand = new List<Card>() { };
            var deck = new Deck().Create();

            deck.InitialDeal(deck.Cards, dealerHand, playerHand);
            Console.WriteLine();
            Console.WriteLine();

            this.PrintDealerInitialHand(dealerHand);
            this.PrintHand(playerHand, PlayerType.Player);

            this.CheckForBlackjack(dealerHand, PlayerType.Dealer);
            this.CheckForBlackjack(playerHand, PlayerType.Player);

            while (this.Turn == PlayerType.Player && this.IsOver == false)
            {
                Console.WriteLine("Hit or Stay: Type H or S");
                var response = Console.ReadLine().ToLower();
                if (response.Equals("s", StringComparison.InvariantCulture))
                {
                    this.Turn = PlayerType.Dealer;

                    Console.WriteLine();
                }
                else if (response.Equals("h", StringComparison.InvariantCulture))
                {
                    Console.WriteLine();

                    this.DrawCardAndCheckForWinner(deck.Cards, playerHand, PlayerType.Player);
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Invalid response. Please type H or S");
                    Console.WriteLine();
                }
            }

            this.PrintHand(dealerHand, PlayerType.Dealer);
            while (this.Turn == PlayerType.Dealer && this.IsOver == false)
            {
                this.CheckForBust(dealerHand, PlayerType.Dealer);

                if (((Card.Sum(dealerHand) > 17) &&
                    (Card.Sum(dealerHand) <= 21)) &&
                    (Card.Sum(dealerHand) > Card.Sum(playerHand)))
                {
                    this.Winner = PlayerType.Dealer;
                    this.IsOver = true;
                }

                this.DrawCardAndCheckForWinner(deck.Cards, dealerHand, PlayerType.Dealer);
            }

            this.PrintFinalOutcome(dealerHand, playerHand);

            Console.ReadLine();
        }

        public void PrintHand(IList<Card> hand, PlayerType playerType)
        {
            if (playerType == PlayerType.Player)
            {
                Console.WriteLine("Player's hand:");
            }
            else if (playerType == PlayerType.Dealer)
            {
                Console.WriteLine("Dealer's hand:");
            }

            foreach (var card in hand)
            {
                var sb = new StringBuilder()
                    .Append(Card.GetCardName(card))
                    .Append(" ")
                    .Append(card.Suit)
                    .Append(" ")
                    .AppendLine()
                    .AppendLine()
                    .ToString();

                Console.Write(sb);
            }
        }

        public void PrintDealerInitialHand(IList<Card> hand)
        {
            Console.WriteLine("Dealer's hand:");
            var cardToShow = hand.LastOrDefault();
            var sb = new StringBuilder()
                    .Append("?? ")
                    .Append(Card.GetCardName(cardToShow))
                    .Append(" ")
                    .Append(cardToShow.Suit)
                    .Append(" ")
                    .AppendLine()
                    .AppendLine()
                    .ToString();

            Console.Write(sb);
        }

        public void PrintFinalOutcome(IList<Card> dealerHand, IList<Card> playerHand)
        {
            Console.WriteLine("Dealer Score " + Card.Sum(dealerHand));
            Console.WriteLine("Player Score " + Card.Sum(playerHand));
            Console.WriteLine();

            if (this.Winner == PlayerType.Dealer)
            {
                Console.WriteLine("Dealer Wins");
            }
            if (this.Winner == PlayerType.Player)
            {
                Console.WriteLine("Player Wins");
            }
        }

        public void End(PlayerType winner)
        {
            if (winner == PlayerType.Player)
            {
                Console.WriteLine("Player Wins");
            }
            else if (winner == PlayerType.Dealer)
            {
                Console.WriteLine("Dealer Wins");
            }

            this.IsOver = true;
        }

        public void CheckForBlackjack(IList<Card> hand, PlayerType playerType)
        {
            if (Card.Sum(hand) == 21)
            {
                this.Winner = playerType;
                this.IsOver = true;
            }
        }

        public void CheckForBust(IList<Card> hand, PlayerType playerType)
        {
            if (Card.Sum(hand) > 21)
            {
                this.Winner = playerType == PlayerType.Player ? PlayerType.Dealer : PlayerType.Player;
                this.IsOver = true;
            }
        }

        public void DrawCardAndCheckForWinner(IList<Card> deckCards, IList<Card> hand, PlayerType playerType)
        {
            Card.Draw(deckCards, hand);

            this.CheckForBust(hand, playerType);
            this.CheckForBlackjack(hand, playerType);

            this.PrintHand(hand, playerType);
        }
    }
}
