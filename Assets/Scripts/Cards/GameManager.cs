using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public int _currentplayer;
    public GameType _gameType;
    public bool _deckHasJokers;
    public int _playerAmount;
    public int _startingBankroll;

    public TableCreator _tableObject;

    public static GameManager _instance;
    public bool _openForBets { private set; get; }
    public Table _table { private set; get; }

    public List<CardTypes> _cardTypes = new List<CardTypes>();
    public GameObject _cardPrefab;

    const string glyphs = "abcdefghijklmnopqrstuvwxyz0123456789";

    private void Awake()
    {
        _openForBets = true;
        if (GameManager._instance != null) Destroy(this);
        else { _instance = this; }
    }

    [ContextMenu("Initialize")]
    public void InitializeGame()
    {
        _table = new Table();
        _tableObject._currentTable.PopulateTable();
    }

    [ContextMenu("Start Game")]
    public void StartGame()
    {
        foreach (PlayerObject p in _tableObject._currentTable._players)
        {
            _table._dealer.DealHand(p, 7);
            p.SetHandText();
        }
    }

    [ContextMenu("Print Hands")]
    public void PrintHands()
    {
        int x = 1;
        foreach (PlayerObject p in _tableObject._currentTable._players)
        {
            Debug.Log("Player: " + x + "| Low hand " + p._player._hand._lowHandRank._type + " " + p._player._hand._lowHandRank._rank);
            Debug.Log("Player: " + x + "| High hand " + p._player._hand._highHandRank._type +" "+ p._player._hand._highHandRank._rank);
            x++;
        }
            
    }

    public static string CreateHandshake()
    {
        string temp = "";
        for (int i = 0; i < 10; i++)
        {
            temp += glyphs[Random.Range(0, glyphs.Length)];
        }
        return temp;
    }

    [ContextMenu("Open")]
    private void OpenBetting()
    {
        _openForBets = true;
    }

    [ContextMenu("Close")]
    private void CloseBetting()
    {
        _openForBets = false;
    }

    [ContextMenu("Debug cards")]
    private void DebugCards()
    {
        _table._players[0]._hand.DebugCards();
    }

    public GameObject CreateCard(Suit suit, int index)
    {
        GameObject card = Instantiate(_cardPrefab, _tableObject._currentTable._deck.position, _tableObject._currentTable._deck.rotation, null);
        card.GetComponent<SpriteRenderer>().sprite = GetSuit(suit)[index];
        return card;
    }

    public List<Sprite> GetSuit(Suit suit)
    {
        return _cardTypes.SingleOrDefault(x => x._suit == suit)._cards;
    }
}
