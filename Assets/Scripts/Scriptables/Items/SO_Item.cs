using UnityEngine;

[CreateAssetMenu(fileName = "SO_Item", menuName = "Scriptable Objects/SO_Item")]
public class SO_Item : ScriptableObject
{
    public string _name;
    public string _description;
    public Sprite _sprite;
    public int _quantity;
}
