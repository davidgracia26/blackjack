using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpBlackjack
{
    public class Deck
    {
        public List<Card> Cards { get; set; }

        public Deck Create()
        {
            var suits = new List<string>() { '\u2660'.ToString(), '\u2665'.ToString(), '\u2666'.ToString(), '\u2663'.ToString() };
            this.Cards = new List<Card>(52);

            foreach (var suit in suits)
            {
                foreach (var value in Enumerable.Range(1, 13))
                {
                    var card = new Card();
                    card.Suit = suit;
                    
                    if (value == (int)CardType.Jack )
                    {
                        card.Type = CardType.Jack;
                        card.Values.Add(10);
                    }
                    else if (value == (int)CardType.Queen)
                    {
                        card.Type = CardType.Queen;
                        card.Values.Add(10);
                    }
                    else if (value == (int)CardType.King)
                    {
                        card.Type = CardType.King;
                        card.Values.Add(10);
                    }
                    else if (value == (int)CardType.Ace)
                    {
                        card.Type = CardType.Ace;
                        var aceValues = new List<int> { 1, 11 };
                        card.Values.AddRange(aceValues);
                    }
                    else
                    {
                        card.Values.Add(value);
                    }

                    this.Cards.Add(card);
                }
            }

            return this;
        }

        public void InitialDeal(IList<Card> deckCards, IList<Card> dealerHand, IList<Card> playerHand)
        {
            for (var cardCount = 0; cardCount < 4; cardCount++)
            {
                if (cardCount % 2 == 0)
                {
                    Card.Draw(deckCards, playerHand);
                }
                else
                {
                    Card.Draw(deckCards, dealerHand);
                }
            }
        }
    }
}
