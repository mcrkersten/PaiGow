using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Hand
{
    public bool _openHand;
    public List<Card> _highHand { private set; get; }
    public List<Card> _lowHand { private set; get; }

    public List<Card> fullHand
    {
        get
        {
            if (_openHand)
                return _fullHand;
            else
                return null;
        }
    }

    private List<Card> _fullHand;

    public HandRank _highHandRank { private set; get; }
    public HandRank _lowHandRank { private set; get; }


    public Hand() 
    {
        _highHand = new List<Card>();
        _lowHand = new List<Card>();
        _fullHand = new List<Card>();
    }

    public bool RecieveHand(List<Card> cards)
    {
        if (fullHand == null || fullHand.Count == 0)
        {
            _fullHand = cards;

            for (int i = 0; i < 2; i++)
                _lowHand.Add(cards[i]);

            for (int i = 0; i < 5; i++)
                _highHand.Add(cards[i + 2]);

            ReadLowCards();
            ReadHighCards();
            return true;
        }
        Debug.LogError("Player already has active hand");
        return false;
    }

    public void DebugCards()
    {
        foreach (Card card in _fullHand)
        {
            Debug.Log(card._suit + " " + card._rank);
        }
    }

    public void ReadLowCards()
    {
        _lowHandRank = new HandRank(_lowHand);
    }

    public void ReadHighCards()
    {
        _highHandRank = new HandRank(_highHand);
    }
}

public class HandRank
{
    public string _handName { get { return GetHandName(); } }
    public PaiGowHand _type { private set; get; }
    public int _rank { private set; get; }

    public HandRank(List<Card> cards)
    {
        if (cards.Count == 2)
            CalculateLowHand(cards);
        else
            CalculateHighHand(cards);
    }

    private string GetHandName()
    {
        switch (_type)
        {
            case PaiGowHand.HighCard:
                if (_rank < 11)
                    return "High " + _rank.ToString();
                if (_rank == 11)
                    return "High Jack";
                if (_rank == 12)
                    return "High Queen";
                if (_rank == 13)
                    return "High King";
                if (_rank == 14)
                    return "High Ace";
                break;
            case PaiGowHand.OnePair:
                if (_rank < 11)
                    return "Pair of " + _rank.ToString();
                if (_rank == 11)
                    return "Pair of Jack";
                if (_rank == 12)
                    return "Pair of Queen";
                if (_rank == 13)
                    return "Pair of King";
                if (_rank == 14)
                    return "Pair of Ace";
                break;
            case PaiGowHand.TwoPair:
                return "Two pair";
            case PaiGowHand.ThreeOfAKind:
                return "Three of a kind";
            case PaiGowHand.Straight:
                return "Straight";
            case PaiGowHand.Flush:
                return "Flush";
            case PaiGowHand.FullHouse:
                return "Full House";
            case PaiGowHand.FourOfAKind:
                return "Four of a kind";
            case PaiGowHand.StraightFlush:
                return "Straight flush";
            case PaiGowHand.RoyalFlush:
                return "Royal flush";
            case PaiGowHand.FiveOfAKind:
                return "Fove of a kind";
        }
        return "NO MATCH";
    }

    private void CalculateLowHand(List<Card> cards)
    {
        if (cards[0]._rank == cards[1]._rank) //PAIR
        {
            _type = PaiGowHand.OnePair;
            _rank = cards[0]._rank;
        }
        else if(cards[0]._suit == Suit.Joker || cards[1]._suit == Suit.Joker) //HIGH ACE
        {
            _type = PaiGowHand.HighCard;
            _rank = 13;
        }
        else
        {
            _type = PaiGowHand.HighCard;
            if (cards[0]._rank > cards[1]._rank)
                _rank = cards[0]._rank;
            else
                _rank = cards[1]._rank;
        }
    }

    private void CalculateHighHand(List<Card> cards)
    {
        _rank = FiveOfAKind(cards);
        if (_rank != 0)
        {
            _type = PaiGowHand.FiveOfAKind;
            return;
        }

        _rank = RoyalFlush(cards);
        if (_rank != 0)
        {
            _type = PaiGowHand.RoyalFlush;
            return;
        }

        _rank = StraightFlush(cards);
        if (_rank != 0)
        {
            _type = PaiGowHand.StraightFlush;
            return;
        }

        _rank = FourOfAKind(cards);
        if (_rank != 0)
        {
            _type = PaiGowHand.FourOfAKind;
            return;
        }

        _rank = FullHouse(cards);
        if (_rank != 0)
        {
            _type = PaiGowHand.FullHouse;
            return;
        }

        _rank = Flush(cards);
        if (_rank != 0)
        {
            _type = PaiGowHand.Flush;
            return;
        }

        _rank = Straight(cards);
        if (_rank != 0)
        {
            _type = PaiGowHand.Straight;
            return;
        }

        _rank = ThreeOfAKind(cards);
        if (_rank != 0)
        {
            _type = PaiGowHand.ThreeOfAKind;
            return;
        }

        _rank = TwoPair(cards);
        if (_rank != 0)
        {
            _type = PaiGowHand.TwoPair;
            return;
        }

        _rank = OnePair(cards);
        if (_rank != 0)
        {
            _type = PaiGowHand.OnePair;
            return;
        }

        _rank = HighCard(cards);
        if (_rank != 0)
        {
            _type = PaiGowHand.HighCard;
            return;
        }
    }

    //Four ACE cards + Joker
    private int FiveOfAKind(List<Card> cards)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            if ( cards[i]._rank != 14)
                return 0;
        }
        return cards[0]._rank;
    }

    //A, K, Q, J, 10, all the same suit.
    private int RoyalFlush(List<Card> cards)
    {
        cards = cards.OrderBy(e => e._rank).ToList(); //Order from low to high

        int flush = Flush(cards);
        if(flush != 0)
        {
            for (int i = 0; i < cards.Count; i++)
                if (cards[i]._rank != 10 + i)
                    return 0;

            return 99; //Royal flush
        }
        return 0;
    }

    //Five cards in a sequence, all in the same suit.
    private int StraightFlush(List<Card> cards)
    {
        int flushRank = Flush(cards);
        if(flushRank != 0)
            return Straight(cards);
        return 0;
    }

    //All four cards of the same rank.
    private int FourOfAKind(List<Card> cards)
    {
        foreach (Card c in cards)
        {
            int count = 0;
            foreach (Card cc in cards)
            {
                if (cc == c) continue;
                if (c._rank == cc._rank)
                    count++;
            }
            if (count == 4)
                return c._rank;
        }
        return 0;
    }

    //Three of a kind with a pair.
    private int FullHouse(List<Card> cards)
    {
        int three = ThreeOfAKind(cards);
        if(three != 0)
        {
            List<Card> x = new List<Card>();
            for (int i = 0; i < cards.Count; i++)
                if (cards[i]._rank != three)
                    x.Add(cards[i]);
            int two = OnePair(x);

            if (two != 0)
                return three + two;
        }
        return 0;
    }

    //Any five cards of the same suit, but not in a sequence. | Joker is all Suits
    private int Flush(List<Card> cards)
    {
        int rank = 0;
        foreach (Card c in cards)
        {
            if (c._suit == Suit.Joker) continue;
            foreach (Card cc in cards)
            {
                if (cc._suit != c._suit && cc._suit != Suit.Joker)
                    return 0;
                else
                    rank += c._rank;
            }
        }
        return rank;
    }

    //Five cards in a sequence, but not of the same suit.
    private int Straight(List<Card> cards)
    {
        cards = cards.OrderBy(e => e._rank).ToList(); //Order from low to high
        for (int i = 0; i < cards.Count - 1; i++)
        {
            if (cards[i]._rank - cards[i + 1]._rank != 0)
                return 0;
        }
        return cards[cards.Count - 1]._rank;
    }

    private int ThreeOfAKind(List<Card> cards)
    {
        foreach (Card c in cards)
        {
            int count = 0;
            foreach (Card cc in cards)
            {
                if (cc._rank == c._rank)
                    count++;
            }
            if (count == 3)
                return c._rank;
        }
        return 0;
    }

    private int TwoPair(List<Card> cards)
    {
        int one = OnePair(cards);
        if(one != 0)
        {
            List<Card> x = new List<Card>();
            for (int i = 0; i < cards.Count; i++)
                if (cards[i]._rank != one)
                    x.Add(cards[i]);
            return OnePair(x);      
        }
        return 0;
    }

    private int OnePair(List<Card> cards)
    {
        foreach (Card c in cards)
        {
            int count = 0;
            foreach (Card cc in cards)
            {
                if (cc._rank == c._rank)
                    count++;
            }
            if (count == 2)
                return c._rank;
        }
        return 0;
    }

    private int HighCard(List<Card> cards)
    {
        cards = cards.OrderBy(e => e._rank).ToList(); //Order from low to high
        return cards[cards.Count - 1]._rank;
    }
}


public enum PaiGowHand
{
    HighCard = 0,
    OnePair,
    TwoPair,
    ThreeOfAKind,
    Straight,
    Flush,
    FullHouse,
    FourOfAKind,
    StraightFlush,
    RoyalFlush,
    FiveOfAKind,
}
