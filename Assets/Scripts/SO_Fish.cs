using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_Fish", menuName = "Scriptable Objects/SO_Fish")]
public class SO_Fish : ScriptableObject
{
    public string _name;
    public string _description;
    public GameObject _visual;
    [Flags]
    public enum Apat
    {
        None = 0,
        Apple = 1 << 0,     // 1
        Worm = 1 << 1,      // 2
        Meat = 1 << 2,// 4
        Fish = 1 << 3      // 8
    }
    public Apat _apat;

    public enum Rarity
    {
        Common,
        Uncommon,
        Rare,
        VeryRare,
        Epic,
        Legendary,
        Unique,
    }
    public Rarity _rarity;
    [Flags]
    public enum Weathers
    {
        None = 0,
        Sunny = 1 << 0,     // 1
        Rainy = 1 << 1,      // 2
        Thunder = 1 << 2,// 4
        Snow = 1 << 3      // 8
    }
    public Weathers _weather;
    [Flags]
    public enum Times
    {
        None = 0,
        Day = 1 << 0,     // 1
        Morning = 1 << 1,      // 2
        Evening = 1 << 2,// 4
        Night = 1 << 3      // 8
    }
    public Times _times;

    public int _encounters;
}
