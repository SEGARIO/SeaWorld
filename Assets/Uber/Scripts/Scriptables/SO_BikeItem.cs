using UnityEngine;

[CreateAssetMenu(fileName = "SO_BikeItem", menuName = "Scriptable Objects/SO_BikeItem")]
public class SO_BikeItem : ScriptableObject
{
    public string _name;
    public Sprite _sprite;
    public GameObject _object;
    public string _description;
    public int _cost;

    [Header("Stats")]
    [Range(0, 3)]public int _speed;
    [Range(0, 3)] public int _acceleration;
    [Range(0, 3)] public int _frein;
    [Range(0, 3)] public int _turn;
    [Range(0, 10)] public int _time;
}
