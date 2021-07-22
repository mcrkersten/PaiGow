using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    public Suit _suit { private set; get; }
    public int _rank { private set; get; }
    public GameObject _card { private set; get; }

    public Card(Suit s, int r)
    {
        _suit = s;
        _rank = r;
    }
}

public enum Suit
{
    Clubs = 0,
    Diamonds = 1,
    Hearts = 2,
    Spades = 3,
    Joker = 4
}