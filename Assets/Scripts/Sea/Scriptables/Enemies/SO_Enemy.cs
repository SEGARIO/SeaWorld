using UnityEngine;

[CreateAssetMenu(fileName = "SO_Enemy", menuName = "Scriptable Objects/SO_Enemy")]
public class SO_Enemy : ScriptableObject
{

    [Header("For Bestiary")]
    public bool _isABoss;
    public string _name;
    public string _description;
    public int _size;
    public int _weight;
    public string _planetOfOrigin;
    public int _lifeSpan;
    public float _population;
    public enum Dangerosity
    {
        Pacific, 
        Neutral,
        Aggressive,
        VeryDangerous,
    }
    public Dangerosity _dangerosity;

    [System.Flags]
    public enum Alimentation
    {
        Vegan = 0,
        Vegetarian = 1 << 0,
        Omnivore = 1 << 1,
        Carnivore = 1 << 2,
        Insectivore = 1 << 3,
        NoAlimentation = 1 << 4,
        Pescivore = 1 << 5,
        Frugivore = 1 << 6,
        Algivore = 1 << 7,
        Sanguivore = 1 << 8,
        Charognard = 1 << 9,
        Mycophage = 1 << 10,
        Xylophage = 1 << 11,
        Lithophage = 1 << 12,
        Électrophage = 1 << 13,




    }
    public Alimentation _alimentation;
    public string _taste;



    [Header("Stats")]
    public bool _isAgressive;
    public bool _detectsWithSight;
    public float _detectionRange;
    [Range(1, 100)] public int _life;
    [Range(0, 10)] public int _attack;
    [Range(0, 10)] public int _speed;

    public bool _hasBeenMet;
    public bool _isNautilus;
}
