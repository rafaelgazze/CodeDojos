using NUnit.Framework;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CreditCardValidation.Test
{
    [TestFixture]
    public class CreditCardValidation
    {
        [TestCase("4999999999999")]
        [TestCase("4999999999999999")]
        public void DiscoverVisaCard(string cardNumber)
        {
            CreditCard card = new CreditCard(cardNumber);
            Assert.That(CardType.Visa, Is.EqualTo(card.Type));
        }

        [TestCase("5199999999999999")]
        [TestCase("5299999999999999")]
        [TestCase("5399999999999999")]
        [TestCase("5499999999999999")]
        [TestCase("5599999999999999")]
        public void DiscoverMasterCard(string cardNumber)
        {
            CreditCard card = new CreditCard(cardNumber);
            Assert.That(CardType.MasterCard, Is.EqualTo(card.Type));
        }

        [TestCase("349999999999999")]
        [TestCase("379999999999999")]
        public void DiscoverAmexCard(string cardNumber)
        {
            CreditCard card = new CreditCard(cardNumber);
            Assert.That(CardType.Amex, Is.EqualTo(card.Type));
        }

        [TestCase("6011999999999999")]
        public void DiscoverDiscoverCard(string cardNumber)
        {
            CreditCard card = new CreditCard(cardNumber);
            Assert.That(CardType.Discover, Is.EqualTo(card.Type));
        }

        [Test]
        public void InvalidCard()
        {
            string cardNumber = "99999999999999";
            CreditCard card = null;
            Assert.Throws<ArgumentException>(() => card = new CreditCard(cardNumber));
        }
    }



    public class ValidateCardType
    {
        private readonly Dictionary<CardType, Regex> _cardsTypes = new Dictionary<CardType, Regex>
        {
            {CardType.Visa, new Regex("^4[0-9]{12}(?:[0-9]{3})?$")},
            {CardType.MasterCard, new Regex("^5[1-5][0-9]{14}$")},
            {CardType.Amex, new Regex("^3[47][0-9]{13}$")},
            {CardType.Discover, new Regex("^6011[0-9]{12}$")}
            
        };

        public CardType DiscoverCardType(string cardNumber)
        {
            var validateCardType = new ValidateCardType();

            foreach (var card in validateCardType._cardsTypes)
            {
                if (card.Value.Match(cardNumber).Success)
                {
                    return card.Key;
                }
            }
            throw new ArgumentException("Cartão Invalido");

        }
    }


    public class CreditCard
    {
        private string CardNumber { get; set; }
        public CardType Type { get; private set; }

        public CreditCard(string cardNumber)
        {
            CardNumber = cardNumber;
            Type = new ValidateCardType().DiscoverCardType(CardNumber);
        }
    }

    public enum CardType
    {
        Visa,
        Discover,
        MasterCard,
        Amex
    }
}
