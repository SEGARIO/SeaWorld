using UnityEngine;

[CreateAssetMenu(fileName = "SO_Items", menuName = "Scriptable Objects/SO_Items")]
public class SO_Items : ScriptableObject
{
    public string _name;
    public string _description;
    public GameObject _visual;
    public int _price;

    [Header("Stats to give")]
    public int _life;
    public int _attack;
    public int _speed;
    public int _shootSpeed;
    public int _range;
    public int _circularRange;
}
