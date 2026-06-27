using UnityEngine;

[CreateAssetMenu(fileName = "SO_PlayableCgaracters", menuName = "Scriptable Objects/SO_PlayableCgaracters")]
public class SO_PlayableCgaracters : ScriptableObject
{
    public string _name;
    public string _description;

    [Header("Capactities")]
    public string _passifDescription;
    public string _tacticDescription;
    public string _ultimeDescription;
    public string _interactableDescription;

    [Header("Stats")]
    [Range(1, 10)] public int _life;
    [Range(1, 5)] public int _shield;
    [Range(1, 10)] public int _speed;
    [Range(1, 10)] public int _attack;
}
