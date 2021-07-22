using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PaiGowPlayer : PlayerObject
{
    public List<Transform> _lowHandCardsPosition = new List<Transform>();
    public List<Transform> _highHandCardsPosition = new List<Transform>();

    public override void CreateCards()
    {
        int index = 0;
        foreach (Card c in GameManager._instance._table._players[_playerIndex]._hand._lowHand)
        {
            GameObject card = null;
            if (c._suit == Suit.Joker) card = GameManager._instance.CreateCard(c._suit, 0);
            else card = GameManager._instance.CreateCard(c._suit, c._rank - 2);
            
            card.transform.DOMove(_lowHandCardsPosition[index].transform.position, 1f).SetEase(Ease.InCubic);
            card.transform.DORotate(_lowHandCardsPosition[index].transform.eulerAngles, 1f).SetEase(Ease.InCubic);
            card.transform.parent = _lowHandCardsPosition[index].transform;
            index++;
        }
        index = 0;

        foreach (Card c in GameManager._instance._table._players[_playerIndex]._hand._highHand)
        {
            GameObject card = null;
            if (c._suit == Suit.Joker) card = GameManager._instance.CreateCard(c._suit, 0);
            else card = GameManager._instance.CreateCard(c._suit, c._rank - 2);

            card.GetComponent<CardInteraction>()._card = c;
            card.transform.DOMove(_highHandCardsPosition[index].transform.position, 1f).SetEase(Ease.InCubic);
            card.transform.DORotate(_highHandCardsPosition[index].transform.eulerAngles, 1f).SetEase(Ease.InCubic);
            card.transform.parent = _highHandCardsPosition[index].transform;
            index++;
        }
    }

    protected override void UpdateCardPosition(Transform pos1, Transform pos2)
    {

    }
}
