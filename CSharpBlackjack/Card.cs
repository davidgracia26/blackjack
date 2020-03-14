using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CSharpBlackjack
{
    public enum CardType
    {
        None = 0,
        Ace = 1,
        Jack = 11,
        Queen = 12,
        King = 13
    }

    public class Card
    {
        public static RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

        public Card()
        {
            this.Values = new List<int>();
        }

        public string Suit { get; set; }
        public CardType Type { get; set; }
        public List<int> Values { get; set; }

        public static void Draw(IList<Card> deckCards, IList<Card> hand)
        {
            byte[] data = new byte[8];
            rng.GetBytes(data);
            decimal value = BitConverter.ToInt64(data, 0) & (long.MaxValue - 1);
            decimal random = value / long.MaxValue;

            var idx = (int)Math.Floor(random * deckCards.Count);
            hand.Add(deckCards[idx]);
            deckCards.RemoveAt(idx);
        }

        public static int Sum(IList<Card> hand)
        {
            if (hand.Any(card => card.Type == CardType.Ace))
            {
                var sumOfNonAces = hand.Where(card => card.Type != CardType.Ace).Sum(card => card.Values.FirstOrDefault());
                var aces = hand.Where(card => card.Type == CardType.Ace).ToList();
                if(aces.Count == 1)
                {
                    var sumUsingAceAsEleven = sumOfNonAces + aces.SelectMany(ace => ace.Values).Max();
                    var sumUsingAceAsOne = sumOfNonAces + aces.SelectMany(ace => ace.Values).Min();

                    return sumUsingAceAsEleven > 21 ? sumUsingAceAsOne : sumUsingAceAsEleven;
                }
                else
                {
                    var sumUsingOneAceAsEleven = sumOfNonAces + aces.SelectMany(ace => ace.Values).Max() + (aces.Count - 1) * aces.SelectMany(ace => ace.Values).Min();
                    var sumUsingAllAcesAsOne = sumOfNonAces + aces.Count * aces.SelectMany(ace => ace.Values).Min();

                    return sumUsingOneAceAsEleven > 21 ? sumUsingAllAcesAsOne : sumUsingOneAceAsEleven;
                }
            }

            return hand.Sum(card => card.Values.FirstOrDefault());
        }

        public static string GetCardName(Card card)
        {
            if (card.Type == CardType.Ace)
            {
                return "A";
            }
            if (card.Type == CardType.Jack)
            {
                return "J";
            }
            if (card.Type == CardType.Queen)
            {
                return "Q";
            }
            if (card.Type == CardType.King)
            {
                return "K";
            }

            return card.Values
                .FirstOrDefault()
                .ToString();
        }
    }
}
