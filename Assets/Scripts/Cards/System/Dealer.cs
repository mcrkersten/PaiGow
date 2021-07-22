using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dealer
{
    private Table _table { set; get; }
    private Deck _deck { set; get; }
    public Hand _hand { private set; get; }

    public Dealer(Table table)
    {
        _deck = new Deck();
        _hand = new Hand();
        _table = table;
        _deck.ShuffleDeck();
    }

    public List<Card> DealCards(int amount)
    {
        List<Card> dealt = new List<Card>();
        for (int i = 0; i < amount; i++)
        {
            dealt.Add(_deck.TakeCard());
        }
        return dealt;
    }

    private void ReturnCards(List<Card> cards)
    {
        foreach (Card card in cards)
            _deck.ReturnCard(card);
    }

    public void DealHand(PlayerObject player, int handSize)
    {
        List<Card> cards = DealCards(handSize);

        if (!player._player._hand.RecieveHand(cards))
        {
            ReturnCards(cards);
        }
        else
        {
            player.CreateCards();
        }
            
    }
}
