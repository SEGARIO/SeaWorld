using System.Collections.Generic;
using UnityEngine;

public class PiecesController : MonoBehaviour
{
    public SO_Characters _chosenKing;
    public SO_Characters[] _kings;
    public SO_Characters[] _queens;
    public SO_Characters[] _knights;
    public SO_Characters[] _bishops;
    public SO_Characters[] _towers;
    public SO_Characters[] _pawns;
    public List<SO_Characters> characters = new List<SO_Characters>();

    void Start()
    {
        characters.Add(_chosenKing);
        AddRandomQueen();
        AddRandomKnight();
        AddRandomKnight();
        AddRandomBishop();
        AddRandomBishop();
        AddRandomTower();
        AddRandomTower();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void AddRandomQueen()
    {
        if (_queens.Length == 0)
        {
            Debug.LogWarning("Le tableau _queens est vide !");
            return;
        }

        SO_Characters randomQueen = _queens[Random.Range(0, _queens.Length)];

        characters.Add(randomQueen);

        
    }
    public void AddRandomKnight()
    {
        if (_knights.Length == 0)
        {
            Debug.LogWarning("Le tableau _knight est vide !");
            return;
        }

        SO_Characters randomKnight = _knights[Random.Range(0, _queens.Length)];

        characters.Add(randomKnight);


    }
    public void AddRandomTower()
    {
        if (_towers.Length == 0)
        {
            Debug.LogWarning("Le tableau _towers est vide !");
            return;
        }

        SO_Characters randomTower = _towers[Random.Range(0, _queens.Length)];

        characters.Add(randomTower);


    }
    public void AddRandomBishop()
    {
        if (_bishops.Length == 0)
        {
            Debug.LogWarning("Le tableau _bishops est vide !");
            return;
        }

        SO_Characters randomBishop = _bishops[Random.Range(0, _queens.Length)];

        characters.Add(randomBishop);


    }
}
