using UnityEngine;

[CreateAssetMenu(fileName = "SO_Planets", menuName = "Scriptable Objects/SO_Planets")]
public class SO_Planets : ScriptableObject
{
    public string _name;
    public string _description;

    public int _position;
    public int _temperatureMoyenne;
    public int _nightDayCycle;
    public string _population;
    public string _gouvernement;
    public int _moons;
    public string _activities;
    public string _money;
    public Color _color;
}
