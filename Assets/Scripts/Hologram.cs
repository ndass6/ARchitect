using UnityEngine;
using System.Collections;

public class Hologram : SmoothBehaviour
{
    public float Cost = 0;

    public void OnDestroy()
    {
        CostDisplay.Instance.UpdateCost();
    }
}
