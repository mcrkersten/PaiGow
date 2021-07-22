using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seat
{
    public bool _occupied { private set; get; }
    public int _bet { private set; get; }
    public int _sideBet { private set; get; }

    private string _playerHandshake;

    public Seat(Table table)
    {
        _bet = 0;
        _sideBet = 0;
    }

    public string SetPlayer(string handshake)
    {
        if (_occupied) return "OCCUPIED";
        if (!GameManager._instance._openForBets) return "LOCKED";

        _playerHandshake = handshake;
        _occupied = true;
        return GameManager.CreateHandshake();
    }

    public string RemovePlayer(string handshake)
    {
        if (_playerHandshake != handshake) return "FALSE HANDSHAKE";
        if (!GameManager._instance._openForBets) return "LOCKED";

        _playerHandshake = null;
        _occupied = false;
        _bet = 0;
        _sideBet = 0;
        return "";
    }

    public void IncreaseBet(int amount, string handshake)
    {
        if (handshake != _playerHandshake) return;
        if (!GameManager._instance._openForBets) return;

        _bet += amount;
    }

    public int DecreaseBet(int amount, string handshake)
    {
        if (handshake != _playerHandshake) return 0;
        if (!GameManager._instance._openForBets) return 0;

        if (_bet >= amount)
        {
            _bet -= amount;
            return amount;
        }
        return 0;
    }

    public void IncreaseSideBet(int amount, string handshake)
    {
        if (handshake != _playerHandshake) return;
        if (!GameManager._instance._openForBets) return;

        _sideBet += amount;
    }

    public int DecreaseSideBet(int amount, string handshake)
    {
        if (handshake != _playerHandshake) return 0;
        if (!GameManager._instance._openForBets) return 0
                ;
        if (_bet >= amount)
        {
            _sideBet -= amount;
            return amount;
        }
        return 0;
    }
}
