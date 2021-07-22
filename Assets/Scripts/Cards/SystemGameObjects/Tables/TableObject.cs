using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableObject : MonoBehaviour
{
    public GameObject _playerPrefab;
    public List<PlayerObject> _players = new List<PlayerObject>();
    public Transform _deck;
    protected virtual void Awake()
    {

    }

    public virtual void PopulateTable()
    {
        for (int i = 0; i < GameManager._instance._playerAmount; i++)
        {
            GameObject p = Instantiate(_playerPrefab);
            PlayerObject po = p.GetComponent<PlayerObject>();
            _players.Add(po);
            po.InitializePlayer(GameManager._instance._startingBankroll);
        }
    }
}
