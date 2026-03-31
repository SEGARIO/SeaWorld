using UnityEngine;

[CreateAssetMenu(fileName = "SO_Characters", menuName = "Scriptable Objects/SO_Characters")]
public class SO_Characters : ScriptableObject
{
    public string _name;
    public string _description;

    [Header("Stats")]
    public float _speed;
    public float _shootForce;
    public float _shootDelay;
}
