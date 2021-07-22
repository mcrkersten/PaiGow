using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class CardInteraction : MonoBehaviour
{
    public Card _card;
    public void StartHover()
    {
        this.transform.DOKill();
        this.transform.DOScale(1.2f, .5f).SetEase(Ease.OutQuad);
        this.transform.DOLocalMoveY(.5f, .5f).SetEase(Ease.OutQuad);
        GameManager._instance._tableObject._currentTable._players[GameManager._instance._currentplayer]._hoveringCard = this;
    }
    public void EndHover()
    {
        this.transform.DOKill();
        this.transform.DOScale(1f, .25f).SetEase(Ease.OutQuad);
        this.transform.DOLocalMoveY(0f, .25f).SetEase(Ease.OutQuad);
        GameManager._instance._tableObject._currentTable._players[GameManager._instance._currentplayer]._hoveringCard = null;
    }

    private void OnSelection()
    {
        GameManager._instance._tableObject._currentTable._players[GameManager._instance._currentplayer]._selectedCard = this;
    }
}
