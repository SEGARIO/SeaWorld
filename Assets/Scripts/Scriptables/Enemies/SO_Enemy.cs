using UnityEngine;

[CreateAssetMenu(fileName = "SO_Enemy", menuName = "Scriptable Objects/SO_Enemy")]
public class SO_Enemy : ScriptableObject
{
    public bool _isABoss;
    public string _name;
    public string _description;

    [Header("Stats")]
    public bool _isAgressive;
    public float _detectionRange;
    [Range(1, 100)] public int _life;
    [Range(0, 10)] public int _attack;
    [Range(0, 10)] public int _speed;
}
