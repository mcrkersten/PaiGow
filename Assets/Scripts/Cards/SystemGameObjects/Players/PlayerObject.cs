using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerObject : MonoBehaviour
{
    [SerializeField] protected int _playerIndex;
    public Player _player { protected set; get; }
    protected Seat _seat;

    [SerializeField] TextMeshProUGUI _lowHandName;
    [SerializeField] TextMeshProUGUI _highHandName;

    public CardInteraction _hoveringCard;
    public CardInteraction _selectedCard;


    public void InitializePlayer(int bankroll)
    {
        _seat = new Seat(GameManager._instance._table);
        _player = new Player(bankroll, _seat);
        GameManager._instance._table._players.Add(_player);
    }

    public virtual void CreateCards()
    {

    }

    protected virtual void UpdateCardPosition(Transform pos1, Transform pos2)
    {

    }

    public void SetHandText()
    {
        _lowHandName.text = _player._hand._lowHandRank._handName;
        _highHandName.text = _player._hand._highHandRank._handName;
    }
}
