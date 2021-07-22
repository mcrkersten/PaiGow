using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table
{
    public Dealer _dealer { private set; get; }
    public List<Player> _players {set; get; }
    public Table()
    {
        _dealer = new Dealer(this);
        _players = new List<Player>();
    }
}
