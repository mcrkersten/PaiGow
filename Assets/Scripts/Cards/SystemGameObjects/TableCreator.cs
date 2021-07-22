using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableCreator : MonoBehaviour
{
    [SerializeField] private GameObject _paiGowTablePrefab;
    [SerializeField] private GameObject _pokerTablePrefab;

    public TableObject _currentTable { private set; get; }

    private void Start()
    {
        switch (GameManager._instance._gameType)
        {
            case GameType.PaiGow:
                InstantiatePaiGowTable();
                break;
            case GameType.Poker:
                InstantiatePokerTable();
                break;
        }
    }

    private void InstantiatePaiGowTable()
    {
        GameObject table = Instantiate(_paiGowTablePrefab, Vector3.zero, Quaternion.identity, null);
        _currentTable = table.GetComponent<PaiGowTable>();
    }

    private void InstantiatePokerTable()
    {

    }
}

public enum GameType
{
    PaiGow = 0,
    Poker = 1,
}
