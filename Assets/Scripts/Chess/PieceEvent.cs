
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PieceEvent
{
    public string eventName;
    public List<PieceEffect> effects = new List<PieceEffect>();

    public void Trigger(PieceContext context)
    {
        foreach (PieceEffect effect in effects)
        {
            if (effect != null)
                effect.Execute(context);
        }
    }
}
