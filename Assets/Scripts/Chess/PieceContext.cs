using UnityEngine;

public class PieceContext : MonoBehaviour
{
    public GameObject piece;
    public Vector2Int position;

    public PieceContext(GameObject piece, Vector2Int position)
    {
        this.piece = piece;
        this.position = position;
    }
}
