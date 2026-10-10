
using UnityEngine;

[CreateAssetMenu(
    fileName = "HealSelf",
    menuName = "Chess/Effects/Heal Self"
)]
public class HealSelfEffect : PieceEffect
{
    public int healingAmount = 5;

    public override void Execute(PieceContext context)
    {
        Debug.Log(
            context.piece.name +
            " déclenche un soin de " +
            healingAmount
        );

        // Appeler ici le système de santé du pion
        // pour lui rendre healingAmount points de vie.
    }
}
