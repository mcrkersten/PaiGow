using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public int _bankroll { private set; get; }
    public Hand _hand { private set; get; }
    private Seat _seat;
    private string _seatHandshake;

    public Player(int bankroll, Seat seat)
    {
        _bankroll = bankroll;
        _seat = seat;
        _hand = new Hand();
        CreateHandshake();
    }

    public void IncreaseBet(int amount)
    {
        if (_bankroll >= amount)
        {
            _bankroll -= amount;
            _seat.IncreaseBet(amount, _seatHandshake);
        }
        else
            Debug.Log("Out of money");
    }

    public void DecreaseBet(int amount)
    {
        _bankroll += _seat.DecreaseBet(amount, _seatHandshake);
    }

        public void IncreaseSideBet(int amount)
    {
        if (_bankroll >= amount)
        {
            _bankroll -= amount;
            _seat.IncreaseSideBet(amount, _seatHandshake);
        }
        else
            Debug.Log("Out of money");
    }

    public void DecreaseSideBet(int amount)
    {
        _bankroll += _seat.DecreaseSideBet(amount, _seatHandshake);
    }

    private void CreateHandshake()
    {
        _seatHandshake = _seat.SetPlayer(GameManager.CreateHandshake());
        switch (_seatHandshake)
        {
            case "OCCUPIED":
                Debug.LogError(">!ERROR!< SEAT TAKEN");
                break;
            case "LOCKED":
                Debug.LogError(">!ERROR!< SEAT LOCKED");
                break;
        }
    }
}
