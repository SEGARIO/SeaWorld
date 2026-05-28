using UnityEngine;

[CreateAssetMenu(fileName = "SO_Characters", menuName = "Scriptable Objects/SO_Characters")]
public class SO_Characters : ScriptableObject
{
    public string _name;
    public string _description;
    public GameObject _attackPrefab;
    public enum Type
    {
        Fire,
        Water,
        Air,
        Earth,
        Dark,
        Ice,
        Sound,
        Electric,
        Plant,
    }
    public Type _type;
    public enum PlayType
    {
       Support,
       Recognition,
       Control,
       Assault,
       Furtive,
    }
    public PlayType _playType;

    [Header("Stats")]
    public int _invocationCost;
    public int _life;
    public int _attack;
    public int _attackCost;
    public int _moveCost;

    
   
}
