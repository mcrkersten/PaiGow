using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Security.Cryptography;

public class Deck
{
    private LinkedList<Card> _cards = new LinkedList<Card>();
    private List<Card> _dealtCards = new List<Card>();

    public Deck()
    {
        PopulateDeck(GameManager._instance._deckHasJokers);
    }

    public Card TakeCard()
    {
        if (_cards.Count == 0)
            return null; //Deck is empty

        Card card = _cards.First();
        _cards.RemoveFirst();
        return card;
    }

    public void ReturnCard(Card card)
    {
        if (!_dealtCards.Contains(card))
            _cards.AddFirst(card);
        else
            Debug.LogError("ERROR, CARD ALREADY IN DECK");
    }

    public void PopulateDeck(bool hasJokers)
    {
        //Clubs
        for (int i = 0; i < 12; i++)
            _cards.AddLast(new Card(Suit.Clubs, i + 2));

        //Diamonds
        for (int i = 0; i < 12; i++)
            _cards.AddLast(new Card(Suit.Diamonds, i + 2));

        //Hearts
        for (int i = 0; i < 12; i++)
            _cards.AddLast(new Card(Suit.Hearts, i + 2));

        //Spades
        for (int i = 0; i < 12; i++)
            _cards.AddLast(new Card(Suit.Spades, i + 2));

        if (hasJokers)
        {
            _cards.AddLast(new Card(Suit.Joker, 15));
            _cards.AddLast(new Card(Suit.Joker, 15));
        }
    }

    public void ShuffleDeck()
    {
        List<Card> shuffle = _cards.ToList();
        shuffle.ShuffleCards();
        shuffle.ShuffleCards();
        shuffle.ShuffleCards();
        _cards = new LinkedList<Card>();
        foreach (Card card in shuffle)
            _cards.AddFirst(card);
    }
}

public static class Shuffle
{
    public static void ShuffleCards<T>(this IList<T> list)
    {
        RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
        int n = list.Count;
        while (n > 1)
        {
            byte[] box = new byte[1];
            do provider.GetBytes(box);
            while (!(box[0] < n * (byte.MaxValue / n)));
            int k = (box[0] % n);
            n--;
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
        provider.Dispose();
    }
}