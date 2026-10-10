using UnityEngine;
using static ChessCreatorGame;

[CreateAssetMenu(fileName = "SO_ChessPiece", menuName = "Scriptable Objects/SO_ChessPiece")]
public class SO_ChessPiece : ScriptableObject
{
    public string _name;
    public string _definition;

    [Header("Stats")]
    [Range(1, 5)] public int _attack;
    [Range(1, 5)]public int _hp;

    public string _startEffect;
    public string _constantEffect;
    public string _EndEffect;

}
