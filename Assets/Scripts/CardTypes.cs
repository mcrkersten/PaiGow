using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardSuite", menuName = "ScriptableObjects/CardSuite", order = 1)]

public class CardTypes : ScriptableObject
{
    public Suit _suit;
    public List<Sprite> _cards = new List<Sprite>();
}
